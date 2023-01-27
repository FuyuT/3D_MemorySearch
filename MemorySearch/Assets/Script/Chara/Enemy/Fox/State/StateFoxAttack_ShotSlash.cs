using UnityEditor;
using UnityEngine;

using State = MyUtil.ActorState<EnemyFox>;

//斬撃を飛ばす攻撃
public class StateFoxAttack_ShotSlash : State
{
    private void UpdateParameter()
    {
        //飛ばす斬撃のパラメータを設定
        Owner.projectileSlash.SetDamage(Owner.projectileDamage);
        Owner.projectileSlash.SetSpeed(Owner.projectileSpeed);
        Owner.projectileSlash.SetMoveVec(Owner.transform.forward);
    }

    protected override void OnEnter(State prevState)
    {
        UpdateParameter();
    }

    protected override void OnUpdate()
    {
        //アニメーションが変更されていなければ、変更して終了
        if (!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Attack_Shot_Slash"))
        {
            return;
        }

        //ターゲットの方向に回転
        Vector3 vec = Vector3.Normalize(Player.readPlayer.GetPos() - Actor.IPosition().GetPosition());
        vec.y = 0;
        Actor.Transform.RotateUpdateToVec(vec, Owner.rotateSpeed);

        //斬撃が飛ぶ方向を更新
        Owner.projectileSlash.SetMoveVec(vec);

        //当たり判定の位置を修正
        Owner.boxCollider.center = new Vector3(Owner.animTransform.localPosition.x, Owner.boxCollider.center.y, Owner.animTransform.localPosition.z);

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //モーションが終了していたら待機へ
        if(BehaviorAnimation.IsPlayEnd(ref Owner.animator, "Attack_Shot_Slash"))
        {
            stateMachine.Dispatch((int)EnemyFox.State.Idle);
            return;
        }
    }

    protected override void OnExit(State prevState)
    {
        Owner.animator.ResetTrigger("Attack_Shot_Slash");
        Owner.projectileDelay = 0;
    }
}
