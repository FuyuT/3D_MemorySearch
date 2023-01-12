using UnityEditor;
using UnityEngine;

using State = MyUtil.ActorState<EnemyCrab>;

public class StateCrabMove : State
{
    protected override void OnEnter(State prevState)
    {
        Owner.animator.SetTrigger("Move");
    }

    protected override void OnUpdate()
    {
        //移動
        if (Owner.searchRange.InTarget)
        {
            //目標に向かって追従移動
            Vector3 moveAdd = Vector3.Normalize(Player.readPlayer.GetPos() - Actor.IPosition().GetPosition()) * Owner.moveSpeed;
            moveAdd.y = 0;
            Actor.IVelocity().AddVelocity(moveAdd);
            SoundManager.instance.PlaySe(Owner.WalkSE);
        }

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //アニメーションが変更されていなければ処理終了
        if (!Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Move")) return;

        //攻撃
        if (Owner.attackRange.InTarget)
        {
            stateMachine.Dispatch((int)EnemyCrab.State.Attack);
            return;
        }

        //防御
        if(Owner.delayGuard >= Owner.delayGuardMax)
        {
            stateMachine.Dispatch((int)EnemyCrab.State.Defense);
        }

        //待機
        if (!Owner.searchRange.InTarget)
        {
            stateMachine.Dispatch((int)EnemyCrab.State.Idle);
        }

    }

    protected override void OnExit(State prevState)
    {
        Owner.animator.ResetTrigger("Move");
    }
}
