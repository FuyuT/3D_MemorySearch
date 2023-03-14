using UnityEngine;

using State = MyUtil.ActorState<Player>;

public class StatePlayerDead : State
{
    protected override void OnEnter(State prevState)
    {
        //当たり判定を消す
        Owner.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
        Owner.gameObject.GetComponent<Rigidbody>().useGravity = false;
        Owner.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        SoundManager.instance.PlaySe(Owner.DownSE, Owner.transform.position);
    }

    protected override void OnUpdate()
    {
        if (!Owner.renderer.enabled) return;
        Owner.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Damage_Dead");

        if (Owner.renderer.enabled)
        {
            if (BehaviorAnimation.IsPlayEnd(ref Owner.animator, "Damage_Dead"))
            {
                //SE関連
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
