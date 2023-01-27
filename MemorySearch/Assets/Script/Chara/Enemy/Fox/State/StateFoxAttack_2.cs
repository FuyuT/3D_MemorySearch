using UnityEditor;
using UnityEngine;

using State = MyUtil.ActorState<EnemyFox>;

public class StateFoxAttack_2 : State
{
    protected override void OnEnter(State prevState)
    {
        //攻撃力設定
        Owner.SetAttackPower(1);

        //ターゲットの方向に回転
        Vector3 vec = Vector3.Normalize(Player.readPlayer.GetPos() - Actor.IPosition().GetPosition());
        vec.y = 0;
        Actor.Transform.RotateUpdateToVec(vec, Owner.rotateSpeed * 5);
    }

    protected override void OnUpdate()
    {
        //アニメーションが変更されていなければ変更
        if (!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Attack_2"))
        {
            return;
        }

        //当たり判定の位置を修正
        Owner.boxCollider.center = new Vector3(Owner.animTransform.localPosition.x, Owner.boxCollider.center.y, Owner.animTransform.localPosition.z);

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //モーションが終了していたら待機へ
        if(BehaviorAnimation.IsPlayEnd(ref Owner.animator, "Attack_2"))
        {
            stateMachine.Dispatch((int)EnemyFox.State.Idle);
            return;
        }
    }

    protected override void OnExit(State prevState)
    {
        Owner.animator.ResetTrigger("Attack_2");
        Owner.closeAttackDelay = 0;
    }
}
