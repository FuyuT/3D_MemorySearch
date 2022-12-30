using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;


/// <summary>
/// 移動
/// </summary>
public class StateMove : State
{
    /*******************************
    * protected
    *******************************/
    protected override void OnEnter(State prevState)
    {
    }

    protected override void OnUpdate()
    {
        Vector3 moveAdd = BehaviorMoveToInput.GetInputVec();

        //カメラの方向と移動ベクトルを合わせる
        moveAdd = Camera.main.transform.forward * moveAdd.z + Camera.main.transform.right * moveAdd.x;
        moveAdd.y = 0;

        //移動スピードを掛ける
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveAdd *= Owner.RunSpeed;
        }
        else
        {
            moveAdd *= Owner.MoveSpeed;
        }

        Actor.Transform.IVelocity().AddVelocity(moveAdd);

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //ダッシュ系
        if (Owner.nowDushDelayTime < 0 && Input.GetKey(KeyCode.Z))
        {
            if(Owner.isGround)
            {
                //タックル
                stateMachine.Dispatch((int)Player.Event.Attack_Tackle);
            }
            else
            {
                //空中ダッシュ
                stateMachine.Dispatch((int)Player.Event.Air_Dush);
            }
        }

        //ジャンプ
        if (Owner.isGround) //浮遊していない時
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (Owner.CheckPossesionMemory((int)Player.Event.Jump) || Owner.CheckPossesionMemory((int)Player.Event.Double_Jump)) //メモリを持っているか確認
                {
                    stateMachine.Dispatch((int)Player.Event.Jump);
                    return;
                }
            }
        }

        //パンチ
        if (Input.GetMouseButtonDown(0) && !Owner.ChapterCamera.activeSelf)
        {
            stateMachine.Dispatch((int)Player.Event.Attack_Punch);
            return;
        }

        //待機状態
        if (Actor.Transform.IVelocity().GetVelocity() == Vector3.zero)
        {
            stateMachine.Dispatch((int)Player.Event.Idle);
            return;
        }
    }
}
