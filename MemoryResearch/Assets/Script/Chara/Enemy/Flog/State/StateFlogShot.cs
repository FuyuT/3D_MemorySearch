using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyFlog>;

public class StateFlogShot : State
{
    /*******************************
    * private
    *******************************/
    float delayShotTime;

    /*******************************
    * protected
    *******************************/
    protected override void OnEnter(State prevState)
    {
        Owner.animator.SetTrigger("Attack_Shot");
        Debug.Log("射撃へ");

        delayShotTime = 0;
        //攻撃力設定
    }

    protected override void OnUpdate()
    {
        //アニメーションが変更されていなければ処理終了
        if (!Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Shot"))
        {
            Owner.animator.SetTrigger("Attack_Shot");
            return;
        }

        RotateToTarget();

        Shot();

        SelectNextState();
    }

    void RotateToTarget()
    {
        //ターゲットへのベクトルを計算
        Vector3 vec = Vector3.Normalize(Player.readPlayer.GetPos() - Actor.IPosition().GetPosition()) * Owner.moveSpeed;

        Actor.Transform.RotateUpdateToVec(vec, Owner.rotateSpeed);
    }

    void Shot()
    {
        //インターバルを更新    
        if (delayShotTime < Owner.ShotInterval)
        {
            delayShotTime += Time.deltaTime;
        }
        else
        {
            CreateBullet();
        }
    }

    void CreateBullet()
    {
        //弾丸を生成
        Vector3 pos = Owner.transform.position + Owner.transform.forward * 4;
        var bullet = Object.Instantiate(Owner.bullet);
        bullet.GetComponent<Bullet>().Init(pos, Owner.transform.forward, Owner.ShotSpeed, Owner.ShotDamage);
        delayShotTime = 0;
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
        Owner.delayShotTime = 0;
    }
}
