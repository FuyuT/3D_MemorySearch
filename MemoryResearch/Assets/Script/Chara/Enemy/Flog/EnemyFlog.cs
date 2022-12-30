using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyFlog>;

public class EnemyFlog : CharaBase
{
    /*******************************
    * private
    *******************************/
    //アクター
    MyUtil.Actor<EnemyFlog> actor;
    //ステートマシン
    MyUtil.ActorStateMachine<EnemyFlog> stateMachine;

    [Header("地面との当たり判定で使用するレイの長さ")]
    [SerializeField] float DirectionCheckHitGround;

    private void Awake()
    {
        Init();
        StateMachineInit();
        actor.Transform.Init();
    }

    private void Init()
    {
        CharaBaseInit();

        delayJumpTime = 0;
        delayShotTime = 0;
        charaParam.hp = HpMax;
    }
    // ステートマシンの初期化
    void StateMachineInit()
    {
        stateMachine.AddAnyTransition<StateFlogIdle>((int)State.Idle);
        stateMachine.AddAnyTransition<StateFlogJump>((int)State.Jump);
        stateMachine.AddAnyTransition<StateFlogShot>((int)State.Attack_Shot);
        stateMachine.AddAnyTransition<StateFlogAttackTongue>((int)State.Attack_Tongue);

        //ステートマシンの開始　初期ステートは引数で指定
        stateMachine.Start(stateMachine.GetOrAddState<StateFlogIdle>());
    }

    void Update()
    {
        if (IsDead())
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Damage_Dead"))
            {
                Debug.Log("死亡");
                animator.SetTrigger("Damage_Dead");
                //当たり判定を消す
                this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                this.gameObject.GetComponent<Rigidbody>().useGravity = false;
                rigidbody.velocity = Vector3.zero;
            }
            return;
        }

        //ステートマシン更新
        stateMachine.Update();


        //角度更新
        RotateUpdate();

        //位置更新
        PositionUpdate();

        //Delayの更新
        DelayUpdate();

        //地面に着地しているか確認する
        CheckCollisionGround();
    }

    //角度の更新
    void RotateUpdate()
    {
        Vector3 temp = actor.IVelocity().GetVelocity();
        temp.y = 0;
        if (temp != Vector3.zero)
        {
            actor.Transform.RotateUpdateToVec(temp, rotateSpeed);
        }
    }

    ///位置の更新
    void PositionUpdate()
    {
        //重力の変更
        switch (stateMachine.currentStateKey)
        {
            //ジャンプ中は重力を使用しない
            case (int)State.Jump:
                actor.IVelocity().SetUseGravity(false);
                break;
            default:
                actor.IVelocity().SetUseGravity(true);
                break;
        }

        //攻撃の時は移動を止める
        if (stateMachine.currentStateKey == (int)State.Attack_Shot)
        {
            actor.IVelocity().InitVelocity();
        }

        //移動
        actor.Transform.PositionUpdate(MyUtil.MoveType.Rigidbody);
    }

    void DelayUpdate()
    {
        //ジャンプ待機時間の更新
        if (delayJumpTime < delayJumpTimeMax)
        {
            delayJumpTime += Time.deltaTime;
        }

        //射撃待機時間の更新
        if (delayShotTime < delayShotTimeMax)
        {
            delayShotTime += Time.deltaTime;
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
        //ジャンプ
        Jump,
        Floating,
        //ガード
        Defense,
        //攻撃
        Attack_Punch,
        Attack_Tongue,
        Attack_Shot,
    }
    [Header("アニメーター")]
    [SerializeField] public Animator animator;

    [Header("範囲")]
    [SerializeField] public MyUtil.TargetCollider searchRange;
    [SerializeField] public MyUtil.TargetCollider attackRange;

    [Header("速度")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float rotateSpeed;

    [Space]
    [Header("HP")]
    [SerializeField] public int HpMax;

    [Header("タックル")]
    [SerializeField] public float delayTackleMax;
    [HideInInspector] public float delayTackle;
    [SerializeField] public float tackleTimeMax;
    [HideInInspector] public float tackleTime;
    [SerializeField] public float tackleSpeed;


    [Header("射撃")]
    [SerializeField] public float delayShotTimeMax;
    public float delayShotTime;

    [SerializeField] public float ShotSpeed;
    [SerializeField] public int ShotDamage;
    [SerializeField] public GameObject bullet;
    [SerializeField] public float ShotInterval;

    [Header("ジャンプ")]
    [Header("ジャンプに入るまでの時間")]
    [SerializeField] public float delayJumpTimeMax;
    [Header("進む力")]
    [SerializeField] public float JumpToTargetPower;

    [Header("初速")]
    [SerializeField] public float JumpStartSpeed;
    [Header("加速値")]
    [SerializeField] public float JumpAcceleration;

    [Space]
    [Header("重力")]
    [SerializeField] public float Gravity;

    public bool isGround;

    //ジャンプ関係
    public float delayJumpTime;

    public float nowJumpSpeed;


    //コンストラクタ
    public EnemyFlog()
    {
        actor = new MyUtil.Actor<EnemyFlog>(this);
        stateMachine = new MyUtil.ActorStateMachine<EnemyFlog>(this, ref actor);
    }

    /*******************************
    * 衝突判定
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
