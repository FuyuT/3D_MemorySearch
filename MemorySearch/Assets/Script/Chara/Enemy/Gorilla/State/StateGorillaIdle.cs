using UnityEditor;
using UnityEngine;

using State = MyUtil.ActorState<EnemyGorilla>;

public class StateGorillaIdle : State
{
    protected override void OnEnter(State prevState)
    {
        //アニメーションが変更されていなければ処理終了
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Idle_1");
    }

    protected override void OnUpdate()
    {

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //探知範囲にいなければ処理終了
        if (!Owner.searchRange.InTarget) return;

        //攻撃へ遷移
        if (Owner.attackRange.InTarget)
        {
            stateMachine.Dispatch((int)EnemyGorilla.State.Attack_Punch);
            return;
        }
        //移動へ遷移
        else
        {
            stateMachine.Dispatch((int)EnemyGorilla.State.Move);
            return;
        }
    }

    protected override void OnExit(State prevState)
    {
        Owner.animator.ResetTrigger("Idle_1");
    }
}
