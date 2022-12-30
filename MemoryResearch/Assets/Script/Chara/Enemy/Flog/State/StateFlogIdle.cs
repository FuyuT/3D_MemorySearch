using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyFlog>;

public class StateFlogIdle : State
{ 
    /*******************************
    * protected
    *******************************/
    protected override void OnEnter(State prevState)
    {
        Owner.animator.SetTrigger("Idle_1");
    }

    protected override void OnUpdate()
    {
        //アニメーションが変更されていなければ処理終了
        if (!Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle_1"))
        {
            Owner.animator.SetTrigger("Idle_1");
            return;
        }

        //探知範囲内にターゲットがいれば、そちらの方向を向く
        if(Owner.searchRange.InTarget)
        {
            RotateToTarget();
        }

        SelectNextState();
    }

    void RotateToTarget()
    {
        //ターゲットへのベクトルを計算
        Vector3 vec = Vector3.Normalize(Player.readPlayer.GetPos() - Actor.IPosition().GetPosition()) * Owner.moveSpeed;

        Actor.Transform.RotateUpdateToVec(vec, Owner.rotateSpeed * 3);
    }

    protected override void SelectNextState()
    {
        if (SelectAttack()) return;

        if (SelectJump()) return;
    }

    bool SelectAttack()
    {
        //射撃のディレイが終わっていれば射撃へ
        if (Owner.delayShotTime > Owner.delayShotTimeMax)
        {
            stateMachine.Dispatch((int)EnemyFlog.State.Attack_Shot);
            return true;
        }

        //攻撃範囲に敵がいれば舌攻撃へ
        if (Owner.attackRange.InTarget)
        {
            stateMachine.Dispatch((int)EnemyFlog.State.Attack_Tongue);
            return true;
        }

        return false;
    }

    bool SelectJump()
    {
        //探知範囲内にターゲットがいないなら終了
        if (!Owner.searchRange.InTarget) return false;

        //ジャンプのディレイが終わっていればジャンプへ
        if (Owner.delayJumpTime >= Owner.delayJumpTimeMax)
        {
            stateMachine.Dispatch((int)EnemyFlog.State.Jump);
            return true;
        }

        return false;
    }

    protected override void OnExit(State prevState)
    {
    }

}
