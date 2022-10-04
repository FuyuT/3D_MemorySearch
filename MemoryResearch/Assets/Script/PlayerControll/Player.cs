using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;

public class Player : MonoBehaviour
{
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
    [Header("重さ")]
    [SerializeField] public float Weight;

    [Space]
    [Header("重力")]
    [SerializeField] public float Gravity;

    [Space]
    [Header("ダッシュ")]
    [Header("初速")]
    [SerializeField] public float DushStartSpeed;
    [Header("加速値")]
    [SerializeField] public float DushAcceleration;
    [Header("移動時間")]
    [SerializeField] public float DushTime;
    /// <summary>
    /// ステートenum
    /// </summary>
    public enum Event
    {
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

    public float nowJumpSpeed;
    public float jumpAcceleration;
    public bool isFloating;
    public bool isGravity;

    public Vector3 moveVec { get; set; }

    public Vector3 dushVec;
    public float nowDushTime;

    StateMachine<Player> stateMachine;

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
        isFloating = false;
        isGravity = false;
        nowJumpSpeed = 0.0f;
        dushVec = Vector3.zero;
        nowDushTime = 0;

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

        //タックルキーが押されているならタックル@
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

        //位置更新
        //浮遊していたら重力を計算に追加
        if(isFloating && isGravity)
        {
            moveVec -= new Vector3(0, Weight + Gravity, 0);
        }
        transform.position += moveVec * Time.deltaTime;
        moveVec = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (nowJumpSpeed < 0)
            {
                Debug.Log("着地した");
                isFloating = false;
                isGravity = false;
                nowJumpSpeed = 0.0f;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (nowJumpSpeed < 0)
            {
                Debug.Log("地面と接している");
                isFloating = false;
                isGravity = false;
                nowJumpSpeed = 0.0f;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("落下している");
            //浮遊状態へ
            isFloating = true;
        }
    }
}

