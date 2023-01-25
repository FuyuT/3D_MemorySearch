using UnityEngine;

using State = MyUtil.ActorState<EnemyFlog>;

public class StateFlogDead : State
{
    protected override void OnEnter(State prevState)
    {
        //当たり判定を消す
        Owner.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        Owner.gameObject.GetComponent<Rigidbody>().useGravity = false;
        Owner.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        SoundManager.instance.PlaySe(Owner.DownSE, Owner.transform.position);
    }

    protected override void OnUpdate()
    {
        if (!Owner.renderer.enabled) return;

        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Damage_Dead");

        if (Owner.renderer.enabled)
        {
            if (BehaviorAnimation.IsPlayEnd(ref Owner.animator, "Damage_Dead"))
            {
                //SE関連
                SoundManager.instance.StopSe(Owner.DownSE);
                SoundManager.instance.PlaySe(Owner.ExplosionSE, Owner.transform.position);
                Owner.renderer.enabled = false;
            }
        }
    }

    protected override void SelectNextState()
    {
    }

    protected override void OnExit(State prevState)
    {
        Owner.animator.ResetTrigger("Damage_Dead");
    }
}
