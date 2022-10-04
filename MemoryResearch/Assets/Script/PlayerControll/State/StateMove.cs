using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;


/// <summary>
/// 移動
/// </summary>
public class StateMove : State
{
    protected override void OnEnter(State prevState)
    {
        Debug.Log("移動状態へ移行");
    }

    protected override void OnUpdate()
    {
        //キー入力での移動
        if (Input.GetKey("up"))
        {
            Owner.moveVec += Owner.transform.forward;
        }
        if (Input.GetKey("down"))
        {
            Owner.moveVec -= Owner.transform.forward;
        }
        if (Input.GetKey("right"))
        {
            Owner.moveVec += Owner.transform.right;
        }
        if (Input.GetKey("left"))
        {
            Owner.moveVec -= Owner.transform.right;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Owner.moveVec *= Owner.RunSpeed;
        }
        else
        {
            Owner.moveVec *= Owner.MoveSpeed;
        }

        if (Owner.moveVec != Vector3.zero)
        {
            stateMachine.Dispatch((int)Player.Event.Idle);
        }

        NextStateUpdate();
    }

    protected override void NextStateUpdate()
    {
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
