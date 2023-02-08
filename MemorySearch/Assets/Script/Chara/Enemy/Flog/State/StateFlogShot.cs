using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyFlog>;

public class StateFlogShot : State
{
    /*******************************
    * private
    *******************************/
    private void UpdateParameter()
    {
        //飛ばす斬撃のパラメータを設定
        Owner.projectileBullet.SetDamage(Owner.projectileDamage);
        Owner.projectileBullet.SetSpeed(Owner.projectileSpeed);
        Owner.projectileBullet.SetMoveVec(Owner.transform.forward);
    }

    void RotateToTarget()
    {
        //ターゲットへのベクトルを計算
        Vector3 vec = Vector3.Normalize(Player.readPlayer.GetPos() - Actor.IPosition().GetPosition()) * Owner.moveSpeed;
        vec.y = 0;
        Actor.Transform.RotateUpdateToVec(vec, Owner.rotateSpeed);

        //弾丸が飛ぶ方向を更新
        Owner.projectileBullet.SetMoveVec(vec);
    }

    /*******************************
    * protected
    *******************************/
    protected override void OnEnter(State prevState)
    {
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Attack_Shot");

        SoundManager.instance.PlaySe(Owner.ShotSE, Owner.transform.position);

        UpdateParameter();
    }

    protected override void OnUpdate()
    {
        //アニメーションが変更されていなければ変更
        if (!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Attack_Shot"))
        {
            return;
        }

        RotateToTarget();

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //アニメーションが終了していたら待機へ
        if (Owner.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            stateMachine.Dispatch((int)EnemyFlog.State.Idle);
        }
    }

    protected override void OnExit(State nextState)
    {
        Owner.projectileDelay = 0;
        Owner.animator.ResetTrigger("Attack_Shot");
        SoundManager.instance.StopSe(Owner.ShotSE);
    }
}
