using UnityEditor;
using UnityEngine;

using State = MyUtil.ActorState<EnemyGorilla>;

public class StateGorillaPunch : State
{
    bool isTackleReady;
    Vector3 moveVec;

    protected override void OnEnter(State prevState)
    {
        Owner.SetAttackPower(3);
    }

    protected override void OnUpdate()
    {
        //アニメーションが変更されていなければ変更
        if (!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Attack_Punch"))
        {
            return;
        }
        SelectNextState();
    }


    protected override void SelectNextState()
    {
        //攻撃モーションが終了したら待機へ
        if (Owner.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            stateMachine.Dispatch((int)EnemyGorilla.State.Idle);
            return;
        }
    }

    protected override void OnExit(State prevState)
    {
        Owner.InitAttackPower();

        Owner.animator.ResetTrigger("Attack_Punch");
        SoundManager.instance.PlaySe(Owner.AttackSE,Owner.transform.position);
    }
}
