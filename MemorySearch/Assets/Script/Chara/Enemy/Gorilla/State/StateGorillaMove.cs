using UnityEditor;
using UnityEngine;

using State = MyUtil.ActorState<EnemyGorilla>;

public class StateGorillaMove : State
{
    protected override void OnEnter(State prevState)
    {
    }

    protected override void OnUpdate()
    {
        //アニメーションが変更されていなければ変更
        if (!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Move_Walk"))
        {
            return;
        }

        //移動
        if (Owner.searchRange.InTarget)
        {
            //目標に向かって追従移動
            Vector3 moveAdd = Vector3.Normalize(Player.readPlayer.GetPos() - Actor.IPosition().GetPosition()) * Owner.moveSpeed;
            moveAdd.y = 0;
            Actor.IVelocity().AddVelocity(moveAdd);
        }

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //攻撃へ遷移
        if (Owner.attackRange.InTarget)
        {
            stateMachine.Dispatch((int)EnemyGorilla.State.Attack_Punch);
            return;
        }

        //待機
        if (!Owner.searchRange.InTarget)
        {
            stateMachine.Dispatch((int)EnemyGorilla.State.Idle);
            return;
        }
    }

    protected override void OnExit(State prevState)
    {
        Owner.animator.ResetTrigger("Move_Walk");
    }
}
