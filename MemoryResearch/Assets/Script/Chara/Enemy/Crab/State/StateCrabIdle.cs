using UnityEditor;
using UnityEngine;

using State = MyUtil.ActorState<EnemyCrab>;

public class StateCrabIdle : State
{
    protected override void OnEnter(State prevState)
    {
        Owner.animator.SetTrigger("Idle");
    }

    protected override void OnUpdate()
    {
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //アニメーションが変更されていなければ処理終了
        if (!Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) return;

        //移動への遷移
        if (Owner.searchRange.InTarget)
        {
            stateMachine.Dispatch((int)EnemyCrab.State.Move);
            return;
        }
    }

    protected override void OnExit(State prevState)
    {
    }
}
