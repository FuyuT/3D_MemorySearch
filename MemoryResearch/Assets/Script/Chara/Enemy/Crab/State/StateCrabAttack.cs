using UnityEditor;
using UnityEngine;

using State = MyUtil.ActorState<EnemyCrab>;

public class StateCrabAttack : State
{
    protected override void OnEnter(State prevState)
    {
        //攻撃力設定
        Owner.SetAttackPower(2);

        Owner.SetSubMemory(MemoryType.Punch);

        //SE
        SoundManager.instance.PlaySe(Owner.AttackSE, Owner.transform.position);
    }

    protected override void OnUpdate()
    {
        //移動
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //アニメーションが変更されていなければ変更
        if (!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Attack"))
        {
            return;
        }

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
        Owner.InitSubMemory();

        Owner.animator.ResetTrigger("Attack");

        SoundManager.instance.StopSe(Owner.AttackSE);
    }
}
