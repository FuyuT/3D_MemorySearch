using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Enemy>;

public class Enemy : MonoBehaviour
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
    }

    StateMachine<Enemy> stateMachine;

    //スコープ小さくする
    [SerializeField] public Transform PlayerTransform;
    public AnyParameterMap parameter;

    [SerializeField] public Vector3 moveVec;

    //ジャンプ関係
    public float nowJumpDelayTime;

    public float nowJumpSpeed;
    public float jumpAcceleration;
    public bool isJump;
    public bool isFloating;

    [SerializeField] IReaderActor playerReadActor = new Actor();

    [Space]
    [Header("ジャンプ")]
    [Header("ジャンプに入るまでの時間")]
    [SerializeField] public float JumpDelayTime;

    [Header("初速")]
    [SerializeField] public float JumpStartSpeed;
    [Header("加速値")]
    [SerializeField] public float JumpAcceleration;
    [Header("重さ")]
    [SerializeField] public float Weight;

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
        parameter = new AnyParameterMap();

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
        isJump = false;
        isFloating = false;
        nowJumpDelayTime = JumpDelayTime;
        nowDushDelayTime = DushDelayTime;

        parameter = new AnyParameterMap();
        parameter.Add("攻撃可能判定", false);
        parameter.Add("所持メモリ", (int)Player.Event.Idle);

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

        stateMachine.AddAnyTransition<StateEnemyAttack>((int)Event.Attack_Punch);

        stateMachine.AddAnyTransition<StateEnemyTackle>((int)Event.Attack_Tackle);

        //ステートマシンの開始　初期ステートは引数で指定
        stateMachine.Start(stateMachine.GetOrAddState<StateEnemyMove>());
    }


    // Update is called once per frame
    void Update()
    {
        //ステートマシン更新
        stateMachine.Update();

        //ジャンプ待機時間の更新
        if(nowJumpDelayTime > 0)
        {
            nowJumpDelayTime -= Time.deltaTime;
        }
        
        //ダッシュ待機時間の更新
        if(nowDushDelayTime > 0)
        {
            nowDushDelayTime -= Time.deltaTime;
        }

        //位置更新
        //浮遊していたら重力を計算に追加
        if (isFloating)
        {
            if (!isJump)
            {
                moveVec -= new Vector3(0, Weight + Gravity, 0);
            }
        }

        if(moveVec.y > 0)
        {
            Debug.Log("上昇中" + moveVec.y);
        }

        transform.position += moveVec * Time.deltaTime;
        moveVec = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //地面
        if (collision.gameObject.tag == "Ground")
        {
            if (isFloating)
            {
                Debug.Log("着地した");
                isFloating = false;
                isJump = false;
                nowJumpSpeed = 0.0f;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //地面
        if (collision.gameObject.tag == "Ground")
        {
            if (isFloating)
            {
                isFloating = false;
                isJump = false;
                nowJumpSpeed = 0.0f;
            }
        }

        //攻撃範囲
        if (collision.gameObject.tag == "Player")
        {
            parameter.Set("攻撃可能判定", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //地面
        if (collision.gameObject.tag == "Ground")
        {
            //浮遊状態へ
            isFloating = true;
        }

        //攻撃範囲
        if (collision.gameObject.tag == "Player")
        {
            parameter.Set("攻撃可能判定", false);
        }
    }
}
