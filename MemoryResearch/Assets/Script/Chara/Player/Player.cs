using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;

public class Player : CharaBase,IPlayer
{
    [Header("チャプターカメラ")]
    [SerializeField] public GameObject ChapterCamera;

    [Header("アニメーター")]
    [SerializeField] public Animator animator;

    [Space]
    [Header("攻撃範囲")]
    [SerializeField] public AttackRange Attack_Punch;
    [SerializeField] public AttackRange Attack_Tackle;

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
    [Header("HPバー")]
    [SerializeField] public GameObject[] HPBar;

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

    [Header("カメラマネージャー")]
    [SerializeField] CameraManager camemana;

    [Header("プレイヤーのRenderer")]
    [SerializeField] Renderer playerRenderer;

    ChangeCamera changeCame;

    public bool isGround;

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

    //////////////////////////////
    /// getter
    //////////////////////////////

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
    /// メモリを設定できる配列番号を取得する
    /// </summary>
    /// <returns>同じメモリがあるのなら</returns>
    public int GetMemoryArrayNullValue(int memory)
    {
        for (int n = 0; n < MemoryMax; n++)
        {
            if (possessionMemory[n] == memory)
            {
                return n;
            }

            if (possessionMemory[n] == 0)
            {
                return n;
            }
        }

        return -1;
    }

    //////////////////////////////
    /// setter
    //////////////////////////////

    /// <summary>
    /// メモリを設定する
    /// </summary>
    public void SetPossesionMemory(int memory, int arrayValue)
    {
        //todo:デバッグ:設定した行動メモリ表示用
        switch(memory)
        {
            case (int)Event.Jump:
                Debug.Log("ジャンプ登録");
                break;
            case (int)Event.Double_Jump:
                Debug.Log("ダブルジャンプ登録");
                break;
            default:
                break;
        }

        possessionMemory[arrayValue] = memory;
    }


    //////////////////////////////
    /// 初期化
    //////////////////////////////

    Player()
    {
    }

    void Start()
    {
        Init();
      //  HPBar[] = HpMax;
    }

    private void Init()
    {
        situation = (int)Situation.None;
        nowJumpSpeed = 0.0f;
        dushVec = Vector3.zero;
        nowDushTime = 0;
        nowDushDelayTime = DushDelayTime;
        isGround = false;
        possessionMemory = new int[MemoryMax];
        for(int n = 0; n < MemoryMax; n++)
        {
            possessionMemory[n] = 0;
        }

        possessionMemory[0] = (int)Event.Double_Jump;
        CharaBaseInit();
        param.Add((int)ParamKey.AttackPower, 0);
        param.Add((int)ParamKey.Attack_Info, (int)AttackInfo.Attack_Not_Possible);
        param.Add((int)Enemy.ParamKey.Hp, HpMax);

        StateMachineInit();
    }

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

        //パンチ
        stateMachine.AddAnyTransition<StateAttack_Punch>((int)Event.Attack_Punch);

        //何も押されていないなら待機状態へ
        stateMachine.AddAnyTransition<StateIdle>((int)Event.Idle);

        //ステートマシンの開始　初期ステートは引数で指定
        stateMachine.Start(stateMachine.GetOrAddState<StateIdle>());
    }


    //////////////////////////////
    /// 更新
    //////////////////////////////

    void Update()
    {
        if (param.Get<int>((int)Enemy.ParamKey.Hp) <= 0)
        {
            animator.SetBool("isDead", true);
            return;
        }

        //FPSカメラの時は、プレイヤーを非表示にする
        if (ChapterCamera.activeSelf)
        {
            playerRenderer.enabled = false;
        }
        else
        {
            playerRenderer.enabled = true;
        }

        //ステートマシン更新
        stateMachine.Update();
        currentState = stateMachine.currentStateKey;

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

    //角度更新
    void RotateUpdate()
    {
        //一人称時の角度変更
        if (ChapterCamera.activeSelf)
        {
            Vector3 a = Vector3.zero;
            a.y = ChapterCamera.transform.eulerAngles.y;
            transform.eulerAngles = a;
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

    //位置更新
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

        SetAnimatarComponent();
    }

    //アニメーターに要素を設定
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

    //ディレイ時間の更新
    void DelayTimeUpdate()
    {
        if(nowDushDelayTime > 0)
        {
            nowDushDelayTime -= Time.deltaTime;
        }
    }

    //////////////////////////////
    /// 当たり判定
    //////////////////////////////
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
            isGround = true;
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
            isGround = false;
        }
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }

    public bool IsDead()
    {
        if (param.Get<int>((int)Enemy.ParamKey.Hp) <= 0)
        {
            return true;
        }
        return false;
    }
}

