using UnityEditor;
using UnityEngine;

using State = MyUtil.ActorState<EnemyCow>;

public class StateCowIdle : State
{
    protected override void OnEnter(State prevState)
    {
        SoundManager.instance.PlaySe(Owner.AttackSE, Owner.transform.position);
    }

    protected override void OnUpdate()
    {
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //アニメーションが変更されていなければ変更
        if (!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Idle"))
        {
            return;
        }

        //探知範囲にいなければ処理終了
        if (!Owner.searchRange.InTarget) return;

        //タックルへ遷移
        if (Owner.delayTackle >= Owner.delayTackleMax)
        {
            stateMachine.Dispatch((int)EnemyCow.State.Attack_Tackle);
            SoundManager.instance.PlaySe(Owner.AttackSE,Owner.transform.position);
            return;
        }
        //移動へ遷移
        else
        {
            stateMachine.Dispatch((int)EnemyCow.State.Move);
            return;
        }
    }

    protected override void OnExit(State prevState)
    {
        Owner.animator.ResetTrigger("Idle");
        SoundManager.instance.StopSe(Owner.AttackSE);
    }
}
