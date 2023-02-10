using UnityEditor;
using UnityEngine;

using State = MyUtil.ActorState<EnemyCrab>;

public class StateCrabIdle : State
{
    protected override void OnEnter(State prevState)
    {
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Idle");
    }

    protected override void OnUpdate()
    {
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //移動への遷移
        if (Owner.searchRange.InTarget)
        {
            stateMachine.Dispatch((int)EnemyCrab.State.Move);
            return;
        }
    }

    protected override void OnExit(State prevState)
    {
        Owner.animator.ResetTrigger("Idle");
    }
}
