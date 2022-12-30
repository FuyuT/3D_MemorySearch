using UnityEditor;
using UnityEngine;

using State = MyUtil.ActorState<EnemyGorilla>;

public class StateGorillaIdle : State
{
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
        }

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
    }
}
