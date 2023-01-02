using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;

/// <summary>
/// ダブルジャンプ
/// </summary>
public class StateDoubleJump : State
{
    bool IsAcceleration;

    protected override void OnEnter(State prevState)
    {
        //アニメーションの更新
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Jump_DowbleJump");

        Owner.isGround = false;

        IsAcceleration = true;

        //y軸の速度を0にする
        Actor.IVelocity().SetVelocityY(0);
        //初速を設定
        Owner.nowJumpSpeed = Owner.JumpStartSpeed;
    }
    protected override void OnUpdate()
    {
        Move();

        Jump();

        SelectNextState();
    }

    //移動
    void Move()
    {
        //方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveAdd = BehaviorMoveToInput.GetInputVec();
        BehaviorMoveToInput.ParseToCameraVec(ref moveAdd);

        Actor.IVelocity().AddVelocity(moveAdd * Owner.MoveSpeed);
    }

    //ジャンプ
    void Jump()
    {
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
        Actor.IVelocity().AddVelocityY(Owner.nowJumpSpeed);
    }

    protected override void SelectNextState()
    {
        //空中ダッシュ
        if (Owner.nowDushDelayTime < 0 && Input.GetKey(KeyCode.Z))
        {
            stateMachine.Dispatch((int)Player.State.Move_Dush);
        }

        //ジャンプ中でない、かつ着地していたら待機状態へ
        if (!IsAcceleration && Owner.isGround)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
        }
    }

    protected override void OnExit(State nextState)
    {
        Actor.IVelocity().SetVelocityY(0);
        Owner.nowJumpSpeed = 0;
        Actor.IVelocity().InitRigidBodyVelocity();
    }
}
