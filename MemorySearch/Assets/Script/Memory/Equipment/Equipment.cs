using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentMemory
{
    /*******************************
    * private
    *******************************/

    //割り振られている操作キー
    private KeyCode keyCode;

    //できることの種類
    List<Player.State> statesList;

    private void Update(Player owner)
    {
        //装備のディレイ更新を行う
        switch ((Player.State)owner.GetCurrentStateKey())
        {
            case Player.State.Jump:
                EquipmentJump.Instance().Update(owner);
                break;
        }

    }

    private void AddState(Player.State state)
    {
        statesList.Add(state);
    }

    private void ClearState()
    {
        statesList.Clear();
    }

    /*******************************
    * public
    *******************************/

    public EquipmentMemory(KeyCode keyCode)
    {
        statesList = new List<Player.State>();
        this.keyCode = keyCode;
    }

    public KeyCode GetKeyCode()
    {
        return keyCode;
    }

    public List<Player.State> GetStatesList()
    {
        return statesList;
    }


    //以前までのデータを消し、この装備が遷移することのできる状態に初期化する
    public void InitState(Player.State memoryType)
    {
        ClearState();

        var memoryData = DataManager.instance.IMemoryData().GetData((MemoryType)(int)memoryType);

        //装備自体のStateを登録
        AddState(memoryType);
        statesList.Add(memoryType);
        //装備の素材となったStateを登録
        for (int n = 0; n < memoryData.materialType.Length; n++)
        {
            Player.State state = (Player.State)(int)memoryData.materialType[n];
            if (state == Player.State.None) continue;
           AddState(state);
        }
    }

    //装備を使用することが可能か返す
    public bool IsPossible(Player.State nextState, Player owner)
    {
        bool isPossible = true;
        switch(nextState)
        {
            case Player.State.Jump:
                isPossible = EquipmentJump.Instance().IsPossible(owner);
                break;
        }

        return isPossible;
    }
}
