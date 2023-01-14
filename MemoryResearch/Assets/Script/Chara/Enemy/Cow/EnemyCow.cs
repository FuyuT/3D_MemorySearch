using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyCow>;

public class EnemyCow : EnemyBase
{
    /*******************************
    * private
    *******************************/
    //アクター
    MyUtil.Actor<EnemyCow> actor;
    //ステートマシン
    MyUtil.ActorStateMachine<EnemyCow> stateMachine;

    private void Awake()
    {
        Init();
        StateMachineInit();
        actor.Transform.Init();
    }
    private void Init()
    {
        CharaBaseInit();
        charaParam.hp = hpMax;

        mainMemory = MemoryType.Dush;
    }
    void StateMachineInit()
    {
        stateMachine.AddAnyTransition<StateCowIdle>((int)State.Idle);
        stateMachine.AddAnyTransition<StateCowMove>((int)State.Move);
        stateMachine.AddAnyTransition<StateCowTackle>((int)State.Attack_Tackle);

        stateMachine.Start(stateMachine.GetOrAddState<StateCowIdle>());
    }

    private void Update()
    {
        //プレイヤーの実体がなければ終了
        if (Player.readPlayer == null) return;

        if (IsDead())
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Damage_Dead"))
            {
                BehaviorAnimation.UpdateTrigger(ref animator, "Damage_Dead");
                //バラバラSE
                SoundManager.instance.PlaySe(DownSE, transform.position);

                //当たり判定を消す
                this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                this.gameObject.GetComponent<Rigidbody>().useGravity = false;
                rigidbody.velocity = Vector3.zero;
            }
            else if(BehaviorAnimation.IsPlayEnd(ref animator, "Damage_Dead"))
            {
                if(renderer.enabled)
                {
                    effectExplosion.Play();
                    //爆発SE
                    SoundManager.instance.PlaySe(ExplosionSE, transform.position);
                    renderer.enabled = false;
                }
            }
            return;
        }

        stateMachine.Update();

        UpdateRotate();

        UpdatePosition();

        UpdateDelay();

        CharaUpdate();
    }
    //角度更新
    void UpdateRotate()
    {
        Vector3 temp = actor.IVelocity().GetVelocity();
        temp.y = 0;
        if (temp != Vector3.zero)
        {
            actor.Transform.RotateUpdateToVec(temp, rotateSpeed);
        }
    }
    //位置更新
    void UpdatePosition()
    {
        //位置を更新
        actor.Transform.PositionUpdate(MyUtil.MoveType.Rigidbody);

        //velocityを初期化
        actor.IVelocity().InitVelocity();
    }
    void UpdateDelay()
    {
        //探知範囲にターゲットが入っていなければ終了
        if (!searchRange.InTarget) return;
        if (delayTackle < delayTackleMax)
        {
            delayTackle += Time.deltaTime;
        }
    }
    

    /*******************************
    * public
    *******************************/
    public enum State
    {
        Idle,
        Move,
        Attack_Tackle,
    }

    [Header("範囲")]
    [SerializeField] public MyUtil.TargetCollider searchRange;
    [SerializeField] public MyUtil.TargetCollider attackRange;

    [Header("速度")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float rotateSpeed;

    [Header("HP")]
    [SerializeField] public int hpMax;

    [Header("タックル")]
    [SerializeField]  public float delayTackleMax;
    [HideInInspector] public float delayTackle;
    [SerializeField]  public float tackleTimeMax;
    [HideInInspector] public float tackleTime;
    [SerializeField]  public float tackleSpeed;

    [Header("モデルのRenderer")]
    [SerializeField] private Renderer renderer;

    [Header("エフェクト")]
    [SerializeField] public Effekseer.EffekseerEmitter effectTackle;
    [SerializeField] public Effekseer.EffekseerEmitter effectExplosion;

    [Space]
    [Header("SE関連")]
    [SerializeField] public AudioClip WalkSE;
    [SerializeField] public AudioClip AttackSE;
    [SerializeField] public AudioClip DownSE;
    [SerializeField] public AudioClip ExplosionSE;

    public EnemyCow()
    {
        actor = new MyUtil.Actor<EnemyCow>(this);
        stateMachine = new MyUtil.ActorStateMachine<EnemyCow>(this, ref actor);
    }

}