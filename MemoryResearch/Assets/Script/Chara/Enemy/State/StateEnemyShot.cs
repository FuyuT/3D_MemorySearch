using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<EnemyFlog>;

public class StateEnemyShot : State
{
    bool  isAttack;
    float shotDelayTime;
    
    protected override void OnEnter(State prevState)
    {
        //攻撃力設定
        Owner.param.Set((int)Player.ParamKey.AttackPower, 5);

        Owner.objectParam.InitMoveVec();

        isAttack = false;

        shotDelayTime = Owner.ShotInterval;
    }

    protected override void OnUpdate()
    {
        Vector3 moveAdd = Owner.TargetTransform.position - Owner.transform.position;
        Owner.objectParam.AddMoveVec(Vector3.Normalize(moveAdd) * Owner.MoveSpeed);
        Shot();
        SelectNextState();
    }

    void Shot()
    {
        if (!Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Shot"))
        {
            return;
        }
        
        //インターバルを更新    
        if (shotDelayTime > 0)
        {
            shotDelayTime -= Time.deltaTime;
        }
        else
        {
            //弾丸を生成
            Vector3 pos = Owner.transform.position + Owner.transform.forward * 4;
            var bullet = Object.Instantiate(Owner.bullet);
            bullet.GetComponent<Bullet>().Init(pos, Owner.transform.forward, Owner.ShotSpeed, Owner.ShotDamage);
            isAttack = true;
            shotDelayTime = Owner.ShotInterval;
        }
    }

    protected override void SelectNextState()
    {
        if(!isAttack) return;

        //アニメーションが待機になっていたら待機状態へ
        if (Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            stateMachine.Dispatch((int)Player.Event.Move);
        }
    }

    protected override void OnExit(State nextState)
    {
    }
}
