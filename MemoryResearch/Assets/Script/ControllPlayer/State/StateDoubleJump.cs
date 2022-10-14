using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;

/// <summary>
/// ダブルジャンプ
/// </summary>
public class StateDoubleJump : State
{
    bool IsAcceleration;

    protected override void OnEnter(State prevState)
    {
        Debug.Log("ダブルジャンプ");
        Owner.situation = (int)Player.Situation.Jump;
        var rb = Owner.GetComponent<Rigidbody>();
        rb.useGravity = false;

        IsAcceleration = true;

        //y軸の速度を0にする
        Owner.moveVec = new Vector3(Owner.moveVec.x, 0, Owner.moveVec.z);
        //初速を設定
        Owner.nowJumpSpeed = Owner.JumpStartSpeed;
    }
    protected override void OnUpdate()
    {
        //方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 inputVector = Vector3.zero;
        if (Input.GetKey("up"))
        {
            inputVector += new Vector3(0, 0, 1);
        }
        if (Input.GetKey("down"))
        {
            inputVector += new Vector3(0, 0, -1);
        }
        if (Input.GetKey("right"))
        {
            inputVector += new Vector3(1, 0, 0);
        }
        if (Input.GetKey("left"))
        {
            inputVector += new Vector3(-1, 0, 0);
        }

        //単位ベクトルを作成
        Vector3 moveForward = Camera.main.transform.forward * inputVector.z + Camera.main.transform.right * inputVector.x;
        moveForward.y = 0;

        //移動ベクトルを格納
        Owner.moveVec += moveForward * Owner.MoveSpeed;


        //ジャンプ処理
        //キー入力されていたら、ジャンプ速度を加速させる（飛距離を延ばす）
        if (Input.GetKey(KeyCode.C) && IsAcceleration)
        {
            Owner.nowJumpSpeed += Owner.JumpAcceleration;
        }
        else
        {
            IsAcceleration = false;
        }

        //ジャンプの速度を減少させる
        Owner.nowJumpSpeed -= Owner.JumpDecreaseValue;

        //ジャンプベクトルを格納
        Owner.moveVec.y += Owner.nowJumpSpeed;

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //空中ダッシュ
        if (Owner.nowDushDelayTime < 0 && Input.GetKey(KeyCode.Z))
        {
            stateMachine.Dispatch((int)Player.Event.Air_Dush);
        }

        //着地したら待機状態へ
        if (Owner.situation == (int)Player.Situation.Jump_End)
        {
            stateMachine.Dispatch((int)Player.Event.Idle);
        }
    }

    protected override void OnExit(State nextState)
    {
        Owner.moveVec.y = 0;
        var rb = Owner.GetComponent<Rigidbody>();
        rb.useGravity = true;
    }
}
