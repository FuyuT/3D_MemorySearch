using UnityEngine;

using State = MyUtil.ActorState<EnemyFox>;

public class StateFoxDead : State
{
    bool isEnd;

    protected override void OnEnter(State prevState)
    {
        //当たり判定を消す
        Owner.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
        Owner.gameObject.GetComponent<Rigidbody>().useGravity = false;
        Owner.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //SoundManager.instance.PlaySe(Owner.DownSE, Owner.transform.position);

        isEnd = false;
    }

    protected override void OnUpdate()
    {
        if (isEnd) return;
        if (!Owner.renderer.enabled) return;

        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Damage_Dead");

        if (Owner.renderer.enabled)
        {
            if (BehaviorAnimation.IsPlayEnd(ref Owner.animator, "Damage_Dead"))
            {
                SceneManager.ToClear();

                ////SE関連
                //SoundManager.instance.StopSe(Owner.DownSE);
                //SoundManager.instance.PlaySe(Owner.ExplosionSE, Owner.transform.position);
                //Owner.renderer.enabled = false;
                isEnd = true;
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
