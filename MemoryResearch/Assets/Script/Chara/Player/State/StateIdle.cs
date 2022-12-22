using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;

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
        //移動
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            stateMachine.Dispatch((int)Player.Event.Move);
        }

        //ジャンプ
        if (Owner.isGround) //浮遊していない時
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (Owner.CheckPossesionMemory((int)Player.Event.Jump) || Owner.CheckPossesionMemory((int)Player.Event.Double_Jump)) //メモリを持っているか確認
                {
                    stateMachine.Dispatch((int)Player.Event.Jump);
                }
            }
        }

        //ダッシュ系
        if (Owner.nowDushDelayTime < 0 && Input.GetKey(KeyCode.Z))
        {
            //空中ダッシュ
            if (Owner.situation == (int)Player.Situation.Floating)
            {
                stateMachine.Dispatch((int)Player.Event.Air_Dush);
            }
            //タックル
            else
            {
                stateMachine.Dispatch((int)Player.Event.Attack_Tackle);
            }

        }

        //パンチ
        if(Input.GetMouseButtonDown(0) && !Owner.ChapterCamera.activeSelf)
        {
            stateMachine.Dispatch((int)Player.Event.Attack_Punch);
        }
    }

}
