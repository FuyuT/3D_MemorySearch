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
        nowDushDelayTime = DushDelayTime;
        isGround = false;
        actor.IVelocity().SetUseGravity(true);
        possessionMemory = new int[MemoryMax];
        for (int n = 0; n < MemoryMax; n++)
        {
            possessionMemory[n] = 0;
        }

        possessionMemory[0] = (int)State.Double_Jump;

        CharaBaseInit();

        charaParam.hp = HpMax;

        StateMachineInit();
        actor.Transform.Init();

        //速度の上限値を設定
        actor.IVelocity().SetMaxVelocityY(120);
    }

    void StateMachineInit()
    {
        //歩く
        stateMachine.AddTransition<StateIdle, StateMoveWalk>((int)State.Move_Walk);
        //走る
        stateMachine.AddAnyTransition<StateMoveRun>((int)State.Move_Run);
        //ガード
        stateMachine.AddAnyTransition<StateGuard>((int)State.Guard);

        //ダッシュ
        stateMachine.AddAnyTransition<StateDush>((int)State.Move_Dush);

        //ジャンプ
        stateMachine.AddAnyTransition<StateJump>((int)State.Jump);
        stateMachine.AddAnyTransition<StateDoubleJump>((int)State.Double_Jump);

        //タックル
        stateMachine.AddAnyTransition<StateAttackTackle>((int)State.Attack_Tackle);

        //パンチ
        stateMachine.AddAnyTransition<StateAttack_Punch>((int)State.Attack_Punch);

        //叩きつけ
        stateMachine.AddAnyTransition<StateAttack_Slam>((int)State.Attack_Slam);

        //何も押されていないなら待機状態へ
        stateMachine.AddAnyTransition<StateIdle>((int)State.Idle);

        //ステートマシンの開始　初期ステートは引数で指定
        stateMachine.Start(stateMachine.GetOrAddState<StateIdle>());
    }

    void Update()
    {
        if (IsDead())
        {
            BehaviorAnimation.UpdateTrigger(ref animator, "Damage_Dead");
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

        CharaUpdate();
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
        actor.Transform.PositionUpdate(MyUtil.MoveType.Rigidbody);

        //velocityを初期化
        actor.IVelocity().InitVelocity();

    }


    //ディレイ時間の更新
    void DelayTimeUpdate()
    {
        if (nowDushDelayTime > 0)
        {
            nowDushDelayTime -= Time.deltaTime;
        }

        if (nowTackleDelayTime > 0)
        {
            nowTackleDelayTime -= Time.deltaTime;
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
    [Header("「ジャンプステート」")]
    [Header("初速")]
    [SerializeField] public float JumpStartSpeed;
    [Header("加速値")]
    [SerializeField] public float JumpAcceleration;

    [Space]
    [Header("重力")]
    [SerializeField] public float JumpDecreaseValue;

    [Space]
    [Header("「ダッシュステート」")]
    [Header("ディレイ秒数")]
    [SerializeField] public float DushDelayTime;
    public float nowDushDelayTime;

    [Header("初速")]
    [SerializeField] public float DushStartSpeed;
    [Header("加速値")]
    [SerializeField] public float DushAcceleration;
    [Header("移動時間")]
    [SerializeField] public float DushTime;

    [Space]
    [Header("「タックルステート」")]
    [Header("ディレイ秒数")]
    [SerializeField] public float TackleDelayTime;

    public float nowTackleDelayTime;

    [Header("初速")]
    [SerializeField] public float TackleStartSpeed;
    [Header("加速値")]
    [SerializeField] public float TackleAcceleration;
    [Header("移動時間")]
    [SerializeField] public float TackleTime;

    [Header("プレイヤーのRenderer")]
    [SerializeField] Renderer playerRenderer;

    ChangeCamera changeCame;

    public bool isGround;

    //アクター
    /// <summary>
    /// ステートenum
    /// </summary>
    public enum State
    {
        None,
        Idle,
        //移動
        Move_Walk,
        Move_Run,
        Move_Dush,
        //ジャンプ
        Jump,
        Double_Jump,
        Floating,
        //攻撃
        Attack_Punch,
        Attack_Slam,
        Attack_Tackle,
        //防御
        Guard,
    }

    public float nowJumpSpeed;
    public float jumpAcceleration;


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
            case (int)State.Jump:
                Debug.Log("ジャンプ登録");
                break;
            case (int)State.Double_Jump:
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

