using UnityEditor;
using UnityEngine;

using State = MyUtil.ActorState<EnemyFox>;

public class StateFoxAttack_Counter : State
{
    protected override void OnEnter(State prevState)
    {
    }

    protected override void OnUpdate()
    {
        //アニメーションが変更されていなければ変更
        if (!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Attack_Counter"))
        {
            return;
        }

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //モーションが終了していたら待機へ
        if(BehaviorAnimation.IsPlayEnd(ref Owner.animator, "Attack_Counter"))
        {
            stateMachine.Dispatch((int)EnemyFox.State.Idle);
            return;
        }
    }

    protected override void OnExit(State prevState)
    {
        Owner.animator.ResetTrigger("Attack_Counter");
    }
}
