using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<EnemyFlog>;

public class EnemyFlog : CharaBase
{
    //////////////////////////////
    /// private

    StateMachine<EnemyFlog> stateMachine;

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

        CharaBaseInit();
        //todo:param消す dataManagerからの参照に変更
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
        stateMachine = new StateMachine<EnemyFlog>(this);

        stateMachine.AddAnyTransition<StateFlogMove>((int)Event.Move);

        //ステートマシンの開始　初期ステートは引数で指定
        stateMachine.Start(stateMachine.GetOrAddState<StateFlogMove>());
    }


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
        RotateUpdateToMoveVec();
    }

    /// <summary>
    /// 位置の更新
    /// </summary>
    void PositionUpdate()
    {
        switch (situation)
        {
            //ジャンプ中は重力を使用しない
            case (int)Situation.Jump:
                objectParam.SetUseGravity(false);
                break;
            default:
                objectParam.SetUseGravity(true);
                break;
        }

        //攻撃の時は移動を止める
        if (stateMachine.currentStateKey == (int)Event.Attack_Shot)
        {
            objectParam.InitMoveVec();
        }

        //移動
        ObjectPositionUpdate(ObjectBase.MoveType.Rigidbody);

        SetAnimatarComponent();
    }

    //Animatar要素を設定
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


    //////////////////////////////
    /// public

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

    [Header("アニメーター")]
    [SerializeField] public Animator animator;

    //スコープ小さくする
    [SerializeField] public Transform TargetTransform;

    //todo:ビヘイビアに持たせる
    //ジャンプ関係
    public float nowJumpDelayTime;

    public float nowJumpSpeed;

    public int situation;


    [Space]
    [Header("Hp")]
    [SerializeField] public int HpMax;

    [Space]
    [Header("移動")]
    [SerializeField] public float MoveSpeed;

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
    [Header("索敵距離")]
    [SerializeField] public float SearchDistance;


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
