using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyGorilla>;

public class EnemyGorilla : EnemyBase
{
    /*******************************
    * private
    *******************************/
    //アクター
    MyUtil.Actor<EnemyGorilla> actor;
    //ステートマシン
    MyUtil.ActorStateMachine<EnemyGorilla> stateMachine;

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

        mainMemory = MemoryType.Slam;
    }
    void StateMachineInit()
    {
        stateMachine.AddAnyTransition<StateGorillaIdle>((int)State.Idle);
        stateMachine.AddAnyTransition<StateGorillaMove>((int)State.Move);
        stateMachine.AddAnyTransition<StateGorillaPunch>((int)State.Attack_Punch);
        stateMachine.AddAnyTransition<StateGorillaDead>((int)State.Dead);

        stateMachine.Start(stateMachine.GetOrAddState<StateGorillaIdle>());
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
    }

    /*******************************
    * public
    *******************************/
    [Header("モデルのRenderer")]
    [SerializeField] public Renderer renderer;

    [Header("エフェクト")]
    [SerializeField] public Effekseer.EffekseerEmitter effectExplosion;

    public enum State
    {
        Idle,
        Move,
        Attack_Punch,
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

    [Space]
    [Header("SE関連")]
    [SerializeField] public AudioClip AttackSE;
    [SerializeField] public AudioClip DownSE;
    [SerializeField] public AudioClip ExplosionSE;

    public EnemyGorilla()
    {
        actor = new MyUtil.Actor<EnemyGorilla>(this);
        stateMachine = new MyUtil.ActorStateMachine<EnemyGorilla>(this, ref actor);
    }

}