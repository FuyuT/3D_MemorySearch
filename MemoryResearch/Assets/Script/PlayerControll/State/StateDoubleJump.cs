using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;

/// <summary>
/// ダブルジャンプ
/// </summary>
public class StateDoubleJump : State
{
    protected override void OnEnter(State prevState)
    {
        Debug.Log("ダブルジャンプ状態へ移行");

        Owner.isGravity = false;

        Owner.isFloating = true;
        //y軸の速度を0にする
        Owner.moveVec = new Vector3(Owner.moveVec.x, 0, Owner.moveVec.z);
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
