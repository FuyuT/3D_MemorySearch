using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;

/// <summary>
/// ジャンプ
/// </summary>
public class StateJump : State
{
    bool IsAcceleration;

    protected override void OnEnter(State prevState)
    {
        IsAcceleration = true;

        Owner.isJump = true;
        Debug.Log("ジャンプ状態へ移行");

        Owner.isFloating = true;
        //初速を設定
        Owner.nowJumpSpeed = Owner.JumpStartSpeed;
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

        //ジャンプ処理
        Vector3 moveAdd = Owner.moveVec;

        Owner.nowJumpSpeed -= Owner.JumpDecreaseValue;
        Owner.moveVec += Owner.transform.forward;

        //キー入力されていたら、ジャンプ速度を加速させる（飛距離を延ばす）
        if (Input.GetKey("up") && IsAcceleration)
        {
            Owner.nowJumpSpeed += Owner.JumpAcceleration;
        }
        else
        {
            IsAcceleration = false;
        }

        moveAdd.y += Owner.nowJumpSpeed;

        Owner.moveVec += moveAdd;

        SelectNextState();
    }

    protected override void SelectNextState()
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

        //着地したら待機状態へ
        if (!Owner.isJump)
        {
            stateMachine.Dispatch((int)Player.Event.Idle);
        }

    }

    protected override void OnExit(State nextState)
    {
        Owner.moveVec.y = 0;
    }
}
