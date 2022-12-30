using UnityEditor;
using UnityEngine;

using State = MyUtil.ActorState<EnemyGorilla>;

public class StateGorillaPunch : State
{
    bool isTackleReady;
    Vector3 moveVec;

    protected override void OnEnter(State prevState)
    {
        Owner.animator.SetTrigger("Attack_Punch");
    }

    protected override void OnUpdate()
    {
        //アニメーションが変更されていなければ処理終了
        if (!Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Punch"))
        {
            Owner.animator.SetTrigger("Attack_Punch");
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
    }
}
