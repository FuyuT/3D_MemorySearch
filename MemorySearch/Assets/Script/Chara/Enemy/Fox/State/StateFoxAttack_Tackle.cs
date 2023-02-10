using UnityEditor;
using UnityEngine;

using State = MyUtil.ActorState<EnemyFox>;

public class StateFoxAttack_Tackle : State
{
    bool isTackleReady;
    bool isTackleEnd;
    Vector3 moveVec;

    protected override void OnEnter(State prevState)
    {
        //攻撃力設定
        Owner.SetAttackPower(3);

        isTackleReady = false;
        isTackleEnd   = false;
        Owner.SetAttackPower(Owner.tackleDamage);
        Owner.animator.SetTrigger("Attack_Tackle_1");

        Owner.SetSubMemory(MemoryType.Tackle);
    }

    protected override void OnUpdate()
    {
        //タックルの準備ができていなければ、準備をして、処理終了
        if (!TackleReady()) return;

        //タックル中なら処理終了
        if (!Tackle()) return;

        SelectNextState();
    }

    bool TackleReady()
    {
        if (isTackleReady) return true;
        //アニメーションが変更されていなければ処理終了
        if (!Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Tackle_1")) return false;

        //ターゲットの方向に回転
        Vector3 vec = Vector3.Normalize(Player.readPlayer.GetPos() - Actor.IPosition().GetPosition());
        vec.y = 0;
        Actor.Transform.RotateUpdateToVec(vec, Owner.rotateSpeed);

        //待機モーションが1週したら、タックルの準備完了
        if (BehaviorAnimation.IsPlayEnd(ref Owner.animator, "Attack_Tackle_1"))
        {
            isTackleReady = true;
            Owner.animator.SetTrigger("Attack_Tackle_2");
            moveVec = Owner.transform.forward;
        }

        return false;
    }

    bool Tackle()
    {
        if (isTackleEnd) return true;

        //目標に向かって移動
        Actor.IVelocity().AddVelocity(moveVec * Owner.tackleSpeed);
        //待機モーションが1週したら、タックルの準備完了
        if (BehaviorAnimation.IsPlayEnd(ref Owner.animator, "Attack_Tackle_2"))
        {
            isTackleEnd = true;
            Owner.animator.SetTrigger("Attack_Tackle_3");
        }

        return false;
    }

    protected override void SelectNextState()
    {
        if (!BehaviorAnimation.IsPlayEnd(ref Owner.animator, "Attack_Tackle_3")) return;

        stateMachine.Dispatch((int)EnemyCow.State.Idle);
        return;
    }

    protected override void OnExit(State prevState)
    {
        Owner.tackleDelay = 0;

        Owner.InitAttackPower();
        Owner.animator.ResetTrigger("Attack_Tackle_1");
        Owner.animator.ResetTrigger("Attack_Tackle_2");
        Owner.animator.ResetTrigger("Attack_Tackle_3");

        Owner.InitSubMemory();
    }
}