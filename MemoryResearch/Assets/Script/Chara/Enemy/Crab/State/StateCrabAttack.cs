using UnityEditor;
using UnityEngine;

using State = MyUtil.ActorState<EnemyCrab>;

public class StateCrabAttack : State
{
    protected override void OnEnter(State prevState)
    {
        Owner.animator.SetTrigger("Attack");
        //攻撃力設定
        Owner.SetAttackPower(5);
    }

    protected override void OnUpdate()
    {
        //移動
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //アニメーションが変更されていなければ処理終了
        if (!Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) return;

        //アニメーションが終了していたら
        if (Owner.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            stateMachine.Dispatch((int)EnemyCrab.State.Move);
        }

        //攻撃
        if (Owner.attackRange.InTarget)
        {
            return;
        }

        //待機

    }

    protected override void OnExit(State prevState)
    {
        //攻撃力初期化
        Owner.InitAttackPower();
    }

}
