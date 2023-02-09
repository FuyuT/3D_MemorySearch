using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;

/// <summary>
/// ダブルジャンプ
/// </summary>
public class StateDoubleJump : State
{
    /*******************************
    * private
    *******************************/
    bool IsAcceleration;
    Vector3 moveAdd;
    Vector3 jumpStartPos;

    void MoveInput()
    {
        //方向キーの入力値とカメラの向きから、移動方向を決定
        moveAdd = BehaviorMoveToInput.GetInputVec();
        BehaviorMoveToInput.ParseToCameraVec(ref moveAdd);
    }
    void Move()
    {
        Actor.Transform.IVelocity().AddVelocity(moveAdd * Owner.MoveSpeed);
    }

    void JumpInput()
    {
        //キー入力されていたら、ジャンプ速度を加速させる（飛距離を延ばす）
        if (Input.GetKey(Owner.equipmentMemories[Owner.currentEquipmentNo].GetKeyCode())
            && IsAcceleration && Owner.nowJumpSpeed > 0)
        {
        }
        else
        {
            IsAcceleration = false;
        }
    }
    void Jump()
    {
        if (IsAcceleration)
        {
            Owner.nowJumpSpeed += Owner.JumpAcceleration * Time.timeScale;
        }
        //ジャンプの速度を減少させる
        Owner.nowJumpSpeed -= Owner.JumpDecreaseValue * Time.timeScale;
        //ジャンプベクトルを格納
        Actor.Transform.IVelocity().AddVelocityY(Owner.nowJumpSpeed);
    }
    /*******************************
    * protected
    *******************************/
    protected override void OnEnter(MyUtil.ActorState<Player> prevState)
    {
        //アニメーションの更新
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Jump_DowbleJump");
        SoundManager.instance.PlaySe(Owner.DonbleJunpSE, Owner.transform.position);

        Owner.isGround = false;

        IsAcceleration = true;

        //y軸の速度を0にする
        Actor.IVelocity().SetVelocityY(0);
        //初速を設定
        Owner.nowJumpSpeed = Owner.JumpStartSpeed;

        jumpStartPos = Owner.transform.position;

        Owner.effectJump.transform.position = jumpStartPos;
        Owner.effectJump.Play();
    }
    protected override void OnUpdate()
    {
        MoveInput();

        JumpInput();

        SelectNextState();
    }
    protected override void OnFiexdUpdate()
    {
        Move();

        Jump();
    }
    protected override void SelectNextState()
    {
        //ジャンプ中でない、かつ着地していたら待機状態へ
        if (!IsAcceleration && Owner.isGround)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }

        //装備から状態を選択
        EquipmentSelectNextState();
    }
    protected override void OnExit(MyUtil.ActorState<Player> nextState)
    {
        Actor.IVelocity().SetVelocityY(0);
        Owner.nowJumpSpeed = 0;
        Actor.IVelocity().InitRigidBodyVelocity();

        //アニメーションのトリガーを解除
        Owner.animator.ResetTrigger("Jump_DowbleJump");
        SoundManager.instance.StopSe(Owner.DonbleJunpSE);
    }
}
