using UnityEditor;
using UnityEngine;

using State = MyUtil.ActorState<EnemyCrab>;

public class StateCrabDefense : State
{
    protected override void OnEnter(State prevState)
    {
        Owner.animator.SetTrigger("Defense1");
    }

    protected override void OnUpdate()
    {
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //アニメーションが変更されていなければ処理終了
        if (!Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Defense1")) return;

        //アニメーションが終了していたら
        if (Owner.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            stateMachine.Dispatch((int)EnemyCrab.State.Move);
        }
    }

    protected override void OnExit(State prevState)
    {
        Owner.delayGuard = 0;
    }
}
