using UnityEngine;

using State = MyUtil.ActorState<EnemyFox>;

public class StateFoxMove : State
{
    protected override void OnEnter(State prevState)
    {
    }

    protected override void OnUpdate()
    {
        //アニメーションが変更されていなければ変更
        if (!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Move_Walk"))
        {
            Owner.animator.ResetTrigger("Idle");

            return;
        }

        //目標に向かって追従移動
        Vector3 moveAdd = Vector3.Normalize(Player.readPlayer.GetPos() - Actor.IPosition().GetPosition()) * Owner.moveSpeed;
        moveAdd.y = 0;
        Actor.IVelocity().AddVelocity(moveAdd);

        //進行方向に回転
        Actor.Transform.RotateUpdateToVec(moveAdd, Owner.rotateSpeed);

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //if (Owner.attackRange.InTarget)
        //{
        //    stateMachine.Dispatch((int)EnemyFox.State.Attack_Punch);
        //    return;
        //}

        //移動モーションが一度終了していたらもう一度再生
        if (BehaviorAnimation.IsPlayEnd(ref Owner.animator, "Move_Walk"))
        {
            stateMachine.Dispatch((int)EnemyFox.State.Idle);
            //BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Idle");
        }


    }

    protected override void OnExit(State prevState)
    {
        Owner.animator.ResetTrigger("Move_Walk");
    }
}
