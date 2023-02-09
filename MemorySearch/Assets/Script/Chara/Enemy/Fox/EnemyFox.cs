using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyFox>;

public class EnemyFox : EnemyBase
{
    /*******************************
    * private
    *******************************/
    //アクター
    MyUtil.Actor<EnemyFox> actor;
    //ステートマシン
    MyUtil.ActorStateMachine<EnemyFox> stateMachine;

    [Header("HPのUI")]
    [SerializeField] HpUI hpUI;

    [Header("地面との当たり判定で使用するレイの長さ")]
    [SerializeField] float DirectionCheckHitGround;

    bool isEngagement;
    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        StateMachineInit();
        actor.Transform.Init();
        Init();
        isEngagement = false;

        mainMemory = MemoryType.Rush_Three;
    }

    private void Init()
    {
        CharaBaseInit();

        charaParam.hp = HpMax;

        mainMemory = MemoryType.Jump;

        actor.IVelocity().SetUseGravity(true);
    }

    // ステートマシンの初期化
    void StateMachineInit()
    {
        stateMachine.AddAnyTransition<StateFoxIdle>((int)State.Idle);
        stateMachine.AddAnyTransition<StateFoxMove>((int)State.Move);
        stateMachine.AddAnyTransition<StateFoxAttack_1>((int)State.Attack_1);
        stateMachine.AddAnyTransition<StateFoxAttack_2>((int)State.Attack_2);
        stateMachine.AddAnyTransition<StateFoxAttack_ShotSlash>((int)State.Attack_Shot_Slash);
        stateMachine.AddAnyTransition<StateFoxAttack_Counter>((int)State.Attack_Counter);
        stateMachine.AddAnyTransition<StateFoxAttack_Tackle>((int)State.Attack_Tackle);
        stateMachine.AddAnyTransition<StateFoxDead>((int)State.Dead);

        //ステートマシンの開始　初期ステートは引数で指定
        stateMachine.Start(stateMachine.GetOrAddState<StateFoxIdle>());
    }

    void Update()
    {
        //プレイヤーの実体がなければ終了
        if (Player.readPlayer == null) return;

        if (!isEngagement)
        {
            if (searchRange.InTarget)
            {
                isEngagement = true;
                hpUI.gameObject.SetActive(true);
                hpUI.SetCurrentHP(0);
                hpUI.Heal(HpMax);
            }
            return;
        }

        if (IsDead())
        {
            if (stateMachine.currentStateKey != (int)State.Dead)
            {
                stateMachine.Dispatch((int)State.Dead);
            }
            stateMachine.Update();
            return;
        }

        if (!UpdateCharaBase()) return;

        stateMachine.Update();

        PositionUpdate();

        DelayUpdate();

        CheckCollisionGround();
    }


    ///位置の更新
    void PositionUpdate()
    {
        //位置を更新
        actor.Transform.PositionUpdate(MyUtil.MoveType.Rigidbody);
        //velocityを初期化
        actor.IVelocity().InitVelocity();
    }

    void DelayUpdate()
    {
        if(tackleDelay < tackleDelayMax)
        {
            tackleDelay += Time.deltaTime;
        }

        if (projectileDelay < projectileDelayMax)
        {
            projectileDelay += Time.deltaTime;
        }

        if (closeAttackDelay < closeAttackDelayMax)
        {
            closeAttackDelay += Time.deltaTime;
        }
    }

    /*******************************
    * public
    *******************************/

    /// <summary>
    /// ステートenum
    /// </summary>
    public enum State
    {
        Idle,
        //移動
        Move,
        //攻撃
        Attack_1,
        Attack_2,
        Attack_Counter,
        Attack_Shot_Slash,
        Attack_Tackle,

        Dead,
    }

    [Header("モデルのRenderer")]
    [SerializeField] public Renderer renderer;

    [Header("範囲")]
    [SerializeField] public MyUtil.TargetCollider searchRange;
    [SerializeField] public MyUtil.TargetCollider attackRange;

    [Header("速度")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float rotateSpeed;

    [Space]
    [Header("HP")]
    [SerializeField] public int HpMax;

    public bool isGround;

    [Header("近距離斬撃")]
    [SerializeField] public int closeAttackDamage;
    [SerializeField] public float closeAttackDelay;
    [SerializeField] public float closeAttackDelayMax;

    [Header("飛ばす斬撃")]
    [SerializeField] public ProjectileSlash projectileSlash;
    [SerializeField] public float projectileSpeed;
    [SerializeField] public int   projectileDamage;
    [SerializeField] public float projectileDelay;
    [SerializeField] public float projectileDelayMax;

    [Header("タックル")]
    [SerializeField] public int   tackleDamage;
    [SerializeField] public float tackleSpeed; 
    [SerializeField] public float tackleDelay;
    [SerializeField] public float tackleDelayMax;

    [HideInInspector]
    public CapsuleCollider capsuleCollider;
    //コンストラクタ
    public EnemyFox()
    {
        actor = new MyUtil.Actor<EnemyFox>(this);
        stateMachine = new MyUtil.ActorStateMachine<EnemyFox>(this, ref actor);
    }

    /*******************************
    * override
    *******************************/
    override protected void AddDamageProcess(int damage)
    {
        //hpUIの再生
        hpUI.Damage(damage);
    }

    /*******************************
    * collision
    *******************************/

    private void CheckCollisionGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * DirectionCheckHitGround, Color.red);
        if (Physics.Raycast(ray, out hit, DirectionCheckHitGround)) //速度が0の時に例が0にならないように+RayAdjustしている
        {

            //地面にレイが当たっていて、プレイヤーのVelocityが上昇していない時
            if (hit.collider.gameObject.CompareTag("Ground")
                && actor.IVelocity().GetState() != MyUtil.VelocityState.isUp)
            {
                isGround = true;
            }
        }
        //何にも当たっていないなら
        else
        {
            isGround = false;
        }
    }
}
