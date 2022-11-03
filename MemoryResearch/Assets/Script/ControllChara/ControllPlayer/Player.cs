using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;

public class Player : CharaBase
{
    [Header("チャプターカメラ")]
    [SerializeField] GameObject ChapterCamera;

    [Header("アニメーター")]
    [SerializeField] Animator animator;

    [Space]
    [Header("ステート所持可能メモリ数")]
    [SerializeField] int MemoryMax;

    [Space]
    [Header("回転")]
    [Header("回転速度(一秒で変わる量)")]
    [SerializeField] float RotateSpeed;

    [Space]
    [Header("Hp")]
    [SerializeField] public int HpMax;

    [Space]
    [Header("移動")]
    [Header("歩く時の速度")]
    [SerializeField] public float MoveSpeed;
    [Header("走る時の速度")]
    [SerializeField] public float RunSpeed;

    [Space]
    [Header("ジャンプ")]
    [Header("初速")]
    [SerializeField] public float JumpStartSpeed;
    [Header("加速値")]
    [SerializeField] public float JumpAcceleration;

    [Space]
    [Header("重力")]
    [SerializeField] public float JumpDecreaseValue;

    [Space]
    [Header("ダッシュ")]
    [Header("ディレイ秒数")]
    [SerializeField] public float DushDelayTime;
    public float nowDushDelayTime;

    [Header("初速")]
    [SerializeField] public float DushStartSpeed;
    [Header("加速値")]
    [SerializeField] public float DushAcceleration;
    [Header("移動時間")]
    [SerializeField] public float DushTime;

    //アクター
    /// <summary>
    /// ステートenum
    /// </summary>
    public enum Event
    {
        None,
        Idle,
        //移動
        Move,
        Air_Dush,
        //ジャンプ
        Jump,
        Double_Jump,
        Floating,
        //攻撃
        Attack_Punch,
        Attack_Tackle,
    }

    public enum Situation
    {
        None,
        Jump,
        Jump_End,
        Floating,
        Dush,
    }

    public enum AttackInfo
    {
        Attack_Not_Possible,
        Attack_Possible,
        Attack_End,
    }

    public float nowJumpSpeed;
    public float jumpAcceleration;

    public int situation;

    public Vector3 moveVec;

    public Vector3 dushVec;
    public float nowDushTime;

    StateMachine<Player> stateMachine;


    public int[] possessionMemory { get; private set; }

    /// <summary>
    /// メモリを所持しているか確認する
    /// 所持していればtrue、いなければfalse
    /// </summary>
    public bool CheckPossesionMemory(int memory)
    {
        for (int n = 0; n < MemoryMax; n++)
        {
            if(memory == possessionMemory[n])
            {
                return true;
            }
        }

        return false;
    }


    //todo:重複している強化メモリを先に検索する
    /// <summary>
    /// 空いている配列番号を確認する
    /// 空いている配列番号を返す
    /// 無い場合、すでにある場合は-1を返す
    /// </summary>
    public int GetMemoryArrayNullValue()
    {
        for (int n = 0; n < MemoryMax; n++)
        {
            if (possessionMemory[n] == 0)
            {
                return n;
            }
        }

        return -1;
    }


    /// <summary>
    /// メモリを設定する
    /// </summary>
    public void SetPossesionMemory(int memory, int arrayValue)
    {
        if (memory == (int)Event.Jump)
        {
            Debug.Log("ジャンプ登録");
        }
        possessionMemory[arrayValue] = memory;
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    Player()
    {
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
        nowJumpSpeed = 0.0f;
        dushVec = Vector3.zero;
        nowDushTime = 0;
        nowDushDelayTime = DushDelayTime;

        possessionMemory = new int[MemoryMax];
        for(int n = 0; n < MemoryMax; n++)
        {
            possessionMemory[n] = 0;
        }

        CharaBaseInit();
        Debug.Log(param);
        param.Add((int)ParamKey.AttackPower, 0);
        param.Add((int)ParamKey.Attack_Info, (int)AttackInfo.Attack_Not_Possible);
        param.Add((int)Enemy.ParamKey.Hp, HpMax);

        StateMachineInit();
    }

    /// <summary>
    /// ステートマシンの初期化
    /// </summary>
    void StateMachineInit()
    {
        stateMachine = new StateMachine<Player>(this);

        //移動キーが押されているなら移動
        stateMachine.AddTransition<StateIdle, StateMove>((int)Event.Move);

        //タックルキーが押されているならタックル
        stateMachine.AddAnyTransition<StateTackle>((int)Event.Attack_Tackle);

        //ジャンプボタンでジャンプ
        stateMachine.AddAnyTransition<StateJump>((int)Event.Jump);
        stateMachine.AddAnyTransition<StateDoubleJump>((int)Event.Double_Jump);

        //エアダッシュ
        stateMachine.AddAnyTransition<StateAirDush>((int)Event.Air_Dush);

        //何も押されていないなら待機状態へ
        stateMachine.AddAnyTransition<StateIdle>((int)Event.Idle);

        //ステートマシンの開始　初期ステートは引数で指定
        stateMachine.Start(stateMachine.GetOrAddState<StateIdle>());
    }

    /// <summary>
    /// MainUpdate
    /// </summary>
    void Update()
    {
        //ステートマシン更新
        stateMachine.Update();

        //角度更新
        RotateUpdate();

        //位置更新
        PositionUpdate();

        //Delayの更新
        DelayTimeUpdate();

        //現在のステートを表示
        //Debug.Log(stateMachine.currentStateKey);
       // Debug.Log("situation:" + situation);

    }

    /// <summary>
    /// 角度の更新
    /// </summary>
    void RotateUpdate()
    {
        //一人称時の角度変更
        if (ChapterCamera.activeSelf)
        {
            transform.rotation = ChapterCamera.transform.rotation;
        }
        else
        {
            Vector3 temp = moveVec;
            temp.y = 0;
            if (temp != Vector3.zero)
            {
                var quaternion = Quaternion.LookRotation(temp);
                transform.rotation = Quaternion.Slerp(this.transform.rotation, quaternion, RotateSpeed * Time.deltaTime);
            }
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
                //重力を使用しない
                rb.velocity = moveVec;
                break;
            //ダッシュ中
            case (int)Situation.Dush:
                //落下しないようにする
                moveVec.y = 0;
                rb.velocity = moveVec;
                break;
            default:
                //ベクトルを設定（重力も足しておく）
                rb.velocity = moveVec + new Vector3(0, rb.velocity.y, 0);
                break;
        }
        moveVec = Vector3.zero;

        float speed = 0;

        if (Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z) > 0)
        {
            speed = 1;
        }

        animator.SetFloat("Speed", speed);
        animator.SetFloat("Speed_Y", rb.velocity.y);

    }

    /// <summary>
    /// ディレイ時間の更新
    /// </summary>
    void DelayTimeUpdate()
    {
        if(nowDushDelayTime > 0)
        {
            nowDushDelayTime -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //地面
        if (collision.gameObject.tag == "Ground")
        {
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
    }

    private void OnCollisionExit(Collision collision)
    {
        //地面
        if (collision.gameObject.tag == "Ground")
        {
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
    }
}

