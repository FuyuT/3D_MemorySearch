using System.Collections.Generic;
using UnityEngine;

namespace MyUtil
{
    /// <summary>
    /// ステートを表すクラス
    /// </summary>
    public abstract class PlayerState : ActorState<Player>
    {
        //ステートから遷移できるステート
        protected Player.State[] dispatchStates;

        //遷移できるステートをセットする
        public void SetDispatchStates(Player.State[] dispatchStates)
        {
            this.dispatchStates = dispatchStates;
        }

        //装備のキーを押されているか確認する　押されていれば装備の番号を、押されていなければ-1を返す
        protected int IsInputEquipmentKey()
        {
            //入力の判断
            if (!Input.anyKey) return -1;

            for (int n = 0; n < Global.EquipmentMemoryMax; n++)
            {
                if (Input.GetKeyDown(Owner.equipmentMemories[n].GetKeyCode()))
                {
                    return n;
                }
            }

            return -1;
        }

        //現在の状態から遷移できるものを取得する
        protected Player.State GetDispachableState(List<Player.State> equipmentStates)
        {
            //装備が遷移することのできる状態の数分ループ
            for (int n = 0; n < equipmentStates.Count; n++)
            {
                if (IsCombineDispatchStates(equipmentStates[n]))
                {
                    return equipmentStates[n];
                }
            }
            return Player.State.None;
        }

        protected bool IsCombineDispatchStates(Player.State state)
        {
            //遷移できる状態の数分ループ
            for (int n = 0; n < dispatchStates.Length; n++)
            {
                if (state == dispatchStates[n])
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 装備のステート選択処理
        /// 遷移先が見つかれば遷移してtrue、見つからなければfalseを返す
        /// </summary>
        /// <returns></returns>
        protected bool EquipmentSelectNextState()
        {
            //装備の入力されているキーを確認する 該当するキーが入力されていなければ終了
            int equipmentNo = IsInputEquipmentKey();
            if (equipmentNo == -1) return false;

            //装備が現在の状態から遷移できるものに該当するか確かめる
            Player.State nextState = GetDispachableState(Owner.equipmentMemories[equipmentNo].GetStatesList());

            //装備が使用可能か確かめる
            if (!Owner.equipmentMemories[equipmentNo].IsPossible(nextState, Owner)) return false;

            //入力された状態へ遷移
            stateMachine.Dispatch((int)nextState);
            Owner.currentEquipmentNo = equipmentNo;
            return true;
        }
    }
}