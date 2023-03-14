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
        stateMachine.AddAnyTransition<StateCowDead>((int)State.Dead);

        stateMachine.Start(stateMachine.GetOrAddState<StateCowIdle>());
    }

    private void Update()
    {
        //プレイヤーの実体がなければ終了
        if (Player.readPlayer == null) return;

        if (IsDead())
        {
            if (stateMachine.currentStateKey != (int)State.Dead)
            {
                stateMachine.Dispatch((int)State.Dead);
            }
            stateMachine.Update();
            return;
        }

        if (!UpdateCharaBase())
        {
            if (stateMachine.currentStateKey != (int)State.Idle)
            {
                stateMachine.Dispatch((int)State.Idle);
            }
            return;
        }

        stateMachine.Update();

        UpdateRotate();

        UpdatePosition();

        UpdateDelay();
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
        Dead,
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
    [SerializeField] public Renderer renderer;

    [Header("エフェクト")]
    [SerializeField] public Effekseer.EffekseerEmitter effectTackle;

    [Space]
    [Header("SE関連")]
    [SerializeField] public AudioClip AttackSE;
    [SerializeField] public AudioClip DownSE;
    [SerializeField] public AudioClip ExplosionSE;

    public EnemyCow()
    {
        actor = new MyUtil.Actor<EnemyCow>(this);
        stateMachine = new MyUtil.ActorStateMachine<EnemyCow>(this, ref actor);
    }

}