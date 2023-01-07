using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyFlog>;

public class StateFlogJump : State
{
    Vector3 moveVec;
    protected override void OnEnter(State prevState)
    {
        //初速を設定
        Owner.nowJumpSpeed = Owner.JumpStartSpeed;

        //目標に向かうベクトル
        moveVec = Vector3.zero;
        moveVec = Vector3.Normalize(Player.readPlayer.GetPos() - Owner.transform.position) * Owner.JumpToTargetPower;
        moveVec.y = 0;

        //速度の初期化
        Actor.IVelocity().InitVelocity();

        //重力の変更
        Actor.IVelocity().SetUseGravity(false);

    }

    protected override void OnUpdate()
    {
        Jump();

        SelectNextState();
    }

    void Jump()
    {
        //ジャンプ速度を減速
        Owner.nowJumpSpeed -= Owner.JumpDecreaseValue * Time.timeScale;
        //移動ベクトルに進行方向のベクトルとジャンプのベクトルを足す
        Actor.IVelocity().AddVelocity(moveVec + new Vector3(0, Owner.nowJumpSpeed, 0));
    }


    protected override void SelectNextState()
    {
        //ジャンプを終了して待機へ
        if (Owner.nowJumpSpeed < 0 && Owner.isGround)
        {
            stateMachine.Dispatch((int)EnemyFlog.State.Idle);
        }
    }

    protected override void OnExit(State nextState)
    {
        Owner.delayJumpTime = 0;
        Actor.IVelocity().InitVelocity();

        //重力の変更
        Actor.IVelocity().SetUseGravity(true);
    }
}
