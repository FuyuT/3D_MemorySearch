using UnityEngine;

using State = MyUtil.ActorState<EnemyFox>;

public class StateFoxIdle : State
{
    protected override void OnEnter(State prevState)
    {
    }

    protected override void OnUpdate()
    {
        //アニメーションが変更されていなければ変更
        if (!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Idle"))
        {
            return;
        }

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //タックル
        if(Owner.tackleDelay >= Owner.tackleDelayMax)
        {
            stateMachine.Dispatch((int)EnemyFox.State.Attack_Tackle);
            return;
        }

        //通常攻撃

        if (Owner.attackRange.InTarget && Owner.closeAttackDelay >= Owner.closeAttackDelayMax)
        {
            if(Random.Range(0,10) < 5)
            {
                stateMachine.Dispatch((int)EnemyFox.State.Attack_1);
            }
            else
            {
                stateMachine.Dispatch((int)EnemyFox.State.Attack_2);
            }
            return;
        }
        
        //遠距離攻撃
        if (Owner.projectileDelay >= Owner.projectileDelayMax)
        {
            stateMachine.Dispatch((int)EnemyFox.State.Attack_Shot_Slash);
            return;
        }

        //移動
        stateMachine.Dispatch((int)EnemyFox.State.Move);
        return;
    }

    protected override void OnExit(State prevState)
    {
        Owner.animator.ResetTrigger("Idle");
    }
}
