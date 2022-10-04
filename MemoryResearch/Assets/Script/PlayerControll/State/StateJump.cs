using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;

/// <summary>
/// ジャンプ
/// </summary>
public class StateJump : State
{
    protected override void OnEnter(State prevState)
    {
        Owner.isGravity = false;
        Debug.Log("ジャンプ状態へ移行");

        Owner.isFloating = true;
        //初速を設定
        Owner.nowJumpSpeed = Owner.JumpStartSpeed;
    }
    protected override void OnUpdate()
    {
        //着地したら待機状態へ
        if (!Owner.isFloating)
        {
            stateMachine.Dispatch((int)Player.Event.Idle);
            return;
        }

        

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

        //ジャンプ処理
        Vector3 moveAdd = Owner.moveVec;

        Owner.nowJumpSpeed -= Owner.Gravity;
        moveAdd.y += Owner.nowJumpSpeed + Owner.JumpAcceleration;

        Owner.moveVec += moveAdd;


        NextStateUpdate();
    }

    protected override void NextStateUpdate()
    {
        //ダブルジャンプ
        if (Input.GetKeyDown(KeyCode.C))
        {
            stateMachine.Dispatch((int)Player.Event.Double_Jump);
        }
        
        //空中ダッシュ
        if (Input.GetKey(KeyCode.Z))
        {
            stateMachine.Dispatch((int)Player.Event.Air_Dush);
        }
    }

    protected override void OnExit(State nextState)
    {
        Owner.isGravity = true;
    }
}
