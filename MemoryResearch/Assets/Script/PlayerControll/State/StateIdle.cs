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
        Debug.Log("待機状態へ移行");
    }
    protected override void OnUpdate()
    {
        NextStateUpdate();
    }

    protected override void NextStateUpdate()
    {
        //移動
        if (Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("right") || Input.GetKey("left"))
        {
            stateMachine.Dispatch((int)Player.Event.Move);
        }

        //ジャンプ
        if (!Owner.isFloating) //浮遊していない時
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                stateMachine.Dispatch((int)Player.Event.Jump);
            }
        }

        //ダッシュ系
        if (Input.GetKey(KeyCode.Z))
        {
            //空中ダッシュ
            if (Owner.isFloating)
            {
                stateMachine.Dispatch((int)Player.Event.Air_Dush);
            }
            //タックル
            else
            {
                stateMachine.Dispatch((int)Player.Event.Attack_Tackle);
            }

        }
    }

}
