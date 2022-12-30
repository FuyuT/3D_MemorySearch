using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;
public class Player : CharaBase, IReadPlayer
{
    /*******************************
    * private
    *******************************/
    //アクター
    MyUtil.Actor<Player> actor;
    //ステートマシン
    MyUtil.ActorStateMachine<Player> stateMachine;

    MyUtil.MoveType moveType;

    [Header("地面との当たり判定で使用するレイの長さ")]
    [SerializeField] float DirectionCheckHitGround;
    void Start()
    {
        Init();
        if (readPlayer == null)
        {
            readPlayer = this;
        }
    }

    private void Init()
    {
        nowJumpSpeed = 0.0f;
        dushSpeed = Vector3.zero;
        nowDushTime = 0;
        nowDushDelayTime = DushDelayTime;
        isGround = true;
        possessionMemory = new int[MemoryMax];
        for (int n = 0; n < MemoryMax; n++)
        {
            possessionMemory[n] = 0;
        }

        possessionMemory[0] = (int)Event.Double_Jump;

        CharaBaseInit();

        charaParam.hp = HpMax;

        StateMachineInit();
        actor.Transform.Init();
    }

    void StateMachineInit()
    {
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

    void Update()
    {
        if (IsDead())
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

        //角度更新
        RotateUpdate();

        //位置更新
        PositionUpdate();

        //Delayの更新
        DelayTimeUpdate();

        //地面に着地しているか確認する
        CheckCollisionGround();
    }

    //角度更新
    void RotateUpdate()
    {
        //一人称時の角度変更
        if (ChapterCamera.activeSelf)
        {
            transform.eulerAngles = new Vector3(0, ChapterCamera.transform.eulerAngles.y, 0);
        }
        else
        {
            Vector3 temp = actor.IVelocity().GetVelocity();
            temp.y = 0;
            if (temp != Vector3.zero)
            {
                actor.Transform.RotateUpdateToVec(temp, RotateSpeed);
            }
        }
    }

    //位置更新
    void PositionUpdate()
    {
        moveType = MyUtil.MoveType.Rigidbody;
        switch (stateMachine.currentStateKey)
        {
            //ジャンプ中
            case (int)Event.Jump:
            case (int)Event.Double_Jump:
                //重力を使用しない
                actor.IVelocity().SetUseGravity(false);
                break;
            default:
                //ベクトルを設定（重力も足しておく）
                actor.IVelocity().SetUseGravity(true);
                break;
        }
        actor.Transform.PositionUpdate(moveType);

        //velocityを初期化
        actor.IVelocity().InitVelocity();

        SetAnimatarComponent();
    }

    //アニメーターに要素を設定
    void SetAnimatarComponent()
    {
        float speed = 0;
        if (Mathf.Abs(rigidbody.velocity.x) + Mathf.Abs(rigidbody.velocity.z) > 0)
        {
            speed = 1;
        }

        animator.SetFloat("Speed", speed);
        animator.SetFloat("Speed_Y", rigidbody.velocity.y);
        animator.SetInteger("StateNo", (int)stateMachine.currentStateKey);
    }

    //ディレイ時間の更新
    void DelayTimeUpdate()
    {
        if (nowDushDelayTime > 0)
        {
            nowDushDelayTime -= Time.deltaTime;
        }
    }

    /*******************************
    * public
    *******************************/

    static public IReadPlayer readPlayer; 

    public Player()
    {
        actor = new MyUtil.Actor<Player>(this);
        stateMachine = new MyUtil.ActorStateMachine<Player>(this, ref actor);
    }
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

    public enum AttackInfo
    {
        Attack_Not_Possible,
        Attack_Possible,
        Attack_End,
    }

    public float nowJumpSpeed;
    public float jumpAcceleration;

    public Vector3 dushSpeed;
    public float nowDushTime;

    public int[] possessionMemory { get; private set; }

    // getter
    public Vector3 GetPos()
    {
        return transform.position;
    }

    //メモリを所持しているか確認する
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
    //メモリを設定できる配列番号を取得する
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

    // setter
    //メモリを設定する
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

    /*******************************
    * 衝突判定
    *******************************/

    private void CheckCollisionGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * DirectionCheckHitGround, Color.red);
        if (Physics.Raycast(ray,out hit, DirectionCheckHitGround)) //速度が0の時に例が0にならないように+RayAdjustしている
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

