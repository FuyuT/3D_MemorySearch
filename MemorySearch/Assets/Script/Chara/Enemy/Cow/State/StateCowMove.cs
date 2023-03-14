using UnityEditor;
using UnityEngine;

using State = MyUtil.ActorState<EnemyCow>;

public class StateCowMove : State
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
        //アニメーションが変更されていなければ処理終了
        if (!Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Move_Walk")) return;

        //タックルへ遷移
        if (Owner.delayTackle >= Owner.delayTackleMax)
        {
            stateMachine.Dispatch((int)EnemyCow.State.Attack_Tackle);
            return;
        }

        //待機
        if (!Owner.searchRange.InTarget)
        {
            stateMachine.Dispatch((int)EnemyCow.State.Idle);
            return;
        }

    }

    protected override void OnExit(State prevState)
    {
        Owner.animator.ResetTrigger("Move_Walk");
    }
}
