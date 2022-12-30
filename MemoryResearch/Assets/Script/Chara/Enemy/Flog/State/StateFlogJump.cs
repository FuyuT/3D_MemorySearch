using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyFlog>;

public class StateFlogJump : State
{
    Vector3 JumpVec;
    protected override void OnEnter(State prevState)
    {
        Owner.animator.SetTrigger("Jump_Start");

        //初速を設定
        Owner.nowJumpSpeed = Owner.JumpStartSpeed;

        //目標に向かうベクトル
        JumpVec = Vector3.zero;
        JumpVec = Vector3.Normalize(Player.readPlayer.GetPos() - Owner.transform.position) * Owner.JumpToTargetPower;
        JumpVec.y = 0;

        //攻撃力設定
        //todo:ダメージ変更

    }

    protected override void OnUpdate()
    {
        //アニメーションが変更されていなければ処理終了
        if (!Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Jump_Start"))
        {
            Owner.animator.SetTrigger("Jump_Start");
            return;
        }

        //ジャンプ処理
        //ジャンプ速度を減速
        Owner.nowJumpSpeed -= (Owner.Gravity + Owner.JumpAcceleration) * Time.deltaTime;
        //移動ベクトルに進行方向のベクトルとジャンプのベクトルを足す
        Actor.IVelocity().AddVelocity(JumpVec + new Vector3(0, Owner.nowJumpSpeed , 0));

        SelectNextState();
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

        //攻撃力設定
    }
}
