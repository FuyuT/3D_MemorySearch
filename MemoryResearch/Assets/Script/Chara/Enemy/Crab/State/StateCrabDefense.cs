using UnityEditor;
using UnityEngine;

using State = MyUtil.ActorState<EnemyCrab>;

public class StateCrabDefense : State
{
    protected override void OnEnter(State prevState)
    {
        Owner.SetDefencePower(30);
    }

    protected override void OnUpdate()
    {
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //アニメーションが変更されていなければ変更
        if (!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Defense1"))
        {
            SoundManager.instance.PlaySe(Owner.GuardSE);
            return;
        }

        //アニメーションが終了していたら
        if (Owner.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            stateMachine.Dispatch((int)EnemyCrab.State.Move);
        }
    }

    protected override void OnExit(State prevState)
    {
        Owner.delayGuard = 0;
        Owner.InitDefencePower();
        Owner.InitSubMemory();
    }
}
