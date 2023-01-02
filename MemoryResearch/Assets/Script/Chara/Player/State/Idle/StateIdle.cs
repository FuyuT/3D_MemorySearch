using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;

/// <summary>
/// 待機
/// </summary>
public class StateIdle : State
{
    protected override void OnEnter(State prevState)
    {
    }

    protected override void OnUpdate()
    {
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //アニメーションが変更されていなければ処理終了
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Idle");

        //移動
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            stateMachine.Dispatch((int)Player.State.Move_Walk);
        }

        //ジャンプ
        if (Owner.isGround) //浮遊していない時
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (Owner.CheckPossesionMemory((int)Player.State.Jump) || Owner.CheckPossesionMemory((int)Player.State.Double_Jump)) //メモリを持っているか確認
                {
                    stateMachine.Dispatch((int)Player.State.Jump);
                }
            }
        }

        //ダッシュ系
        if (Owner.nowDushDelayTime < 0 && Input.GetKey(KeyCode.Z))
        {
            if(Owner.isGround)
            {
                stateMachine.Dispatch((int)Player.State.Move_Dush);
            }
            else
            {
                stateMachine.Dispatch((int)Player.State.Move_Dush);
            }
        }

        //パンチ
        if (Input.GetMouseButtonDown(0) && !Owner.ChapterCamera.activeSelf)
        {
            stateMachine.Dispatch((int)Player.State.Attack_Punch);
        }

        //ガード
        if (Input.GetKey(KeyCode.H))
        {
            stateMachine.Dispatch((int)Player.State.Guard);
            return;
        }

        //叩きつけ
        if (Input.GetKey(KeyCode.J))
        {
            stateMachine.Dispatch((int)Player.State.Attack_Slam);
            return;
        }

        //タックル
        if (Input.GetKey(KeyCode.K))
        {
            stateMachine.Dispatch((int)Player.State.Attack_Tackle);
            return;
        } 
    }

    protected override void OnExit(State nextState)
    {
        //アニメーションのトリガーを解除
        Owner.animator.ResetTrigger("Idle");
    }

}
