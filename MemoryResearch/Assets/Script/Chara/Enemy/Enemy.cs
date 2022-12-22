using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Enemy>;

public class Enemy : CharaBase
{
    /// <summary>
    /// ステートenum
    /// </summary>
    public enum Event
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
        Attack_Tackle,
        Attack_Shot,
    }

    public enum Situation
    {
        None,
        Jump,
        Jump_End,
        Floating,
        Dush,
    }

    StateMachine<Enemy> stateMachine;

    [Header("アニメーター")]
    [SerializeField] public Animator animator;

    [Header("当たり判定")]
    [Header("パンチ")]
    [SerializeField] public AttackRange attackRangePunch;

    //スコープ小さくする
    [SerializeField] public Transform PlayerTransform;

    [SerializeField] public Vector3 moveVec;

    //ジャンプ関係
    public float nowJumpDelayTime;

    public float nowJumpSpeed;
    public float jumpAcceleration;

    public int   situation;

    [SerializeField] IReaderActor playerReadActor = new Actor();

    [Header("回転速度(一秒で変わる量)")]
    [SerializeField] float RotateSpeed;

    [Space]
    [Header("Hp")]
    [SerializeField] public int HpMax;

    [Space]
    [Header("移動")]
    [SerializeField] public float MoveSpeed;
    [Header("射撃")]
    [SerializeField] public float ShotDelayTime;
    public float nowShotDelaytime;

    [SerializeField] public float ShotSpeed;
    [SerializeField] public int   ShotDamage;
    [SerializeField] public GameObject bullet;
    [SerializeField] public float ShotInterval;

    [Header("ジャンプ")]
    [Header("ジャンプに入るまでの時間")]
    [SerializeField] public float JumpDelayTime;
    [Header("進む力")]
    [SerializeField] public float JumpToTargetPower;

    [Header("初速")]
    [SerializeField] public float JumpStartSpeed;
    [Header("加速値")]
    [SerializeField] public float JumpAcceleration;

    [Space]
    [Header("重力")]
    [SerializeField] public float Gravity;

    [Space]
    [Header("ダッシュ")]
    [Header("ダッシュに入るまでの時間")]
    [SerializeField] public float DushDelayTime;

    [Header("初速")]
    [SerializeField] public float DushStartSpeed;
    [Header("加速値")]
    [SerializeField] public float DushAcceleration;
    [Header("移動時間")]
    [SerializeField] public float DushTime;

    [Space]
    [Header("索敵距離")]
    [SerializeField] public float SearchDistance;

    public Vector3 dushVec;
    public float nowDushTime;
    public float nowDushDelayTime;



    //todo:unity上で設置されてるオブジェクトはコンストラクタ通らない？
    /// <summary>
    /// コンストラクタ
    /// </summary>
    Enemy()
    {
        Init();
    }

    /// <summary>
    /// MainStart
    /// </summary>
    void Start()
    {
        Init();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Init()
    {
        situation = (int)Situation.None;

        nowJumpDelayTime = JumpDelayTime;
        nowShotDelaytime = ShotDelayTime;
        nowDushDelayTime = DushDelayTime;

        CharaBaseInit();
        //todo:param消す　もしくは修正する　Addの初期値設定できてない
        param.Add((int)Enemy.ParamKey.PossesionMemory, Player.Event.None);
        param.Add((int)ParamKey.AttackPower, 0);

        param.Add((int)Enemy.ParamKey.Hp, HpMax);
        param.Set((int)Enemy.ParamKey.Hp, HpMax);

        StateMachineInit();
    }

    /// <summary>
    /// ステートマシンの初期化
    /// </summary>
    void StateMachineInit()
    {
        stateMachine = new StateMachine<Enemy>(this);

        stateMachine.AddAnyTransition<StateEnemyMove>((int)Event.Move);

        stateMachine.AddAnyTransition<StateEnemyJump>((int)Event.Jump);

        stateMachine.AddAnyTransition<StateEnemyShot>((int)Event.Attack_Shot);

        //stateMachine.AddAnyTransition<StateEnemyAttack>((int)Event.Attack_Punch);

        //stateMachine.AddAnyTransition<StateEnemyTackle>((int)Event.Attack_Tackle);

        //ステートマシンの開始　初期ステートは引数で指定
        stateMachine.Start(stateMachine.GetOrAddState<StateEnemyMove>());
    }


    // Update is called once per frame
    void Update()
    {
        if (param.Get<int>((int)Enemy.ParamKey.Hp) <= 0)
        {
            animator.SetBool("isDead", true);
            return;
        }

        //ステートマシン更新
        stateMachine.Update();
        currentState = stateMachine.currentStateKey;

        //ジャンプ待機時間の更新
        if (nowJumpDelayTime > 0)
        {
            nowJumpDelayTime -= Time.deltaTime;
        }

        //ジャンプ待機時間の更新
        if (nowShotDelaytime > 0)
        {
            nowShotDelaytime -= Time.deltaTime;
        }

        //ダッシュ待機時間の更新
        if (nowDushDelayTime > 0)
        {
            nowDushDelayTime -= Time.deltaTime;
        }


        //角度更新
        RotateUpdate();

        //位置更新
        PositionUpdate();
    }

    /// <summary>
    /// 角度の更新
    /// </summary>
    void RotateUpdate()
    {
        //todo:現在のステートkeyを取得する関数を作成する
        if (moveVec != Vector3.zero)
        {
            //yは回転の要素に加えない
            Vector3 temp = moveVec;
            temp.y = 0;
            var quaternion = Quaternion.LookRotation(temp);
            transform.rotation = Quaternion.Slerp(this.transform.rotation, quaternion, RotateSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 位置の更新
    /// </summary>
    void PositionUpdate()
    {

        var rb = GetComponent<Rigidbody>();
        switch (situation)
        {
            //ジャンプ中
            case (int)Situation.Jump:
                //ジャンプ中は重力を使用しない
                rb.velocity = moveVec;
                break;
            //それ以外
            default:
                //ベクトルを設定（重力も足しておく）
                rb.velocity = moveVec + new Vector3(0, rb.velocity.y, 0);
                break;
        }


        moveVec = Vector3.zero;

        if(stateMachine.currentStateKey == (int)Event.Attack_Shot)
        {
            moveVec = Vector3.zero;
            rb.velocity = new Vector3(0, 0, 0);
        }
        SetAnimatarComponent();
    }

    void SetAnimatarComponent()
    {
        var rb = GetComponent<Rigidbody>();
        float speed = 0;
        if (Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z) > 0)
        {
            speed = 1;
        }

        animator.SetFloat("Speed", speed);
        animator.SetFloat("Speed_Y", rb.velocity.y);
        animator.SetInteger("StateNo", (int)stateMachine.currentStateKey);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //地面
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("IsLanding", true);
            switch (situation)
            {
                case (int)Situation.Jump:
                    situation = (int)Situation.Jump_End;

                    nowJumpSpeed = 0.0f;
                    break;
                case (int)Situation.Floating:
                    break;
                default:
                    break;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //地面
        if (collision.gameObject.tag == "Ground")
        {
            switch (situation)
            {
                case (int)Situation.Jump:
                    if (nowJumpSpeed < 0)
                    {
                        situation = (int)Situation.Jump_End;
                        nowJumpSpeed = 0.0f;
                    }
                    break;
                case (int)Situation.Floating:
                    break;
                default:
                    break;
            }
        }

        //攻撃範囲
        if (collision.gameObject.tag == "Player")
        {
            param.Set((int)Enemy.ParamKey.PossesionMemory, true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //地面
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("IsLanding", false);

            switch (situation)
            {
                case (int)Situation.None:
                    //浮遊状態へ
                    situation = (int)Situation.Floating;
                    break;

                case (int)Situation.Jump:
                    break;
                case (int)Situation.Jump_End:
                    break;

                case (int)Situation.Floating:
                    break;
                default:
                    break;
            }
        }

        //攻撃範囲
        if (collision.gameObject.tag == "Player")
        {
            param.Set((int)Enemy.ParamKey.PossesionMemory, false);
        }
    }
}
