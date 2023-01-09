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
    void Awake()
    {
        EquipmentInit();
        Init();
        readPlayer = this;
    }

    void EquipmentInit()
    {
        equipmentMemories = new EquipmentMemory[Global.EquipmentMemoryMax];
        //キーの設定
        equipmentMemories[0] = new EquipmentMemory(KeyCode.I);
        equipmentMemories[1] = new EquipmentMemory(KeyCode.J);
        equipmentMemories[2] = new EquipmentMemory(KeyCode.K);
        equipmentMemories[3] = new EquipmentMemory(KeyCode.L);

        //装備の初期設定
        IPlayerData plyaerData = DataManager.instance.IPlayerData();
        for (int n = 0; n < equipmentMemories.Length; n++)
        {
            equipmentMemories[n].InitState((Player.State)plyaerData.GetEquipmentMemory(n));
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
        //待機
        stateMachine.AddAnyTransition<StateIdle>((int)State.Idle);
        stateMachine.GetOrAddState<StateIdle>().SetDispatchStates(new State[7]
            { State.Move_Walk,State.Jump,State.Move_Dush,State.Attack_Punch,
              State.Guard,State.Attack_Slam,State.Attack_Tackle});

        //歩く
        stateMachine.AddTransition<StateIdle, StateMoveWalk>((int)State.Move_Walk);
        stateMachine.GetOrAddState<StateMoveWalk>().SetDispatchStates(new State[7]
            { State.Move_Walk,State.Jump,State.Move_Dush,State.Attack_Punch,
              State.Guard,State.Attack_Slam,State.Attack_Tackle});

        //走る
        stateMachine.AddAnyTransition<StateMoveRun>((int)State.Move_Run);
        stateMachine.GetOrAddState<StateMoveRun>().SetDispatchStates(new State[7]
            { State.Move_Walk,State.Jump,State.Move_Dush,State.Attack_Punch,
              State.Guard,State.Attack_Slam,State.Attack_Tackle});

        //ガード
        stateMachine.AddAnyTransition<StateGuard>((int)State.Guard);
        stateMachine.GetOrAddState<StateGuard>().SetDispatchStates(new State[1]
        {
            State.None
        });

        //ダッシュ
        stateMachine.AddAnyTransition<StateDush>((int)State.Move_Dush);
        stateMachine.GetOrAddState<StateDush>().SetDispatchStates(new State[1]
        {
            State.None
        });

        //ジャンプ
        stateMachine.AddAnyTransition<StateJump>((int)State.Jump);
        stateMachine.GetOrAddState<StateJump>().SetDispatchStates(new State[2]
            { State.Double_Jump,State.Move_Dush});

        stateMachine.AddAnyTransition<StateDoubleJump>((int)State.Double_Jump);
        stateMachine.GetOrAddState<StateDoubleJump>().SetDispatchStates(new State[1]
        {
            State.Move_Dush
        });

        //タックル
        stateMachine.AddAnyTransition<StateAttackTackle>((int)State.Attack_Tackle);
        stateMachine.GetOrAddState<StateAttackTackle>().SetDispatchStates(new State[1]
        {
            State.None
        });

        //パンチ
        stateMachine.AddAnyTransition<StateAttack_Punch>((int)State.Attack_Punch);
        stateMachine.GetOrAddState<StateAttack_Punch>().SetDispatchStates(new State[1]
        {
            State.None
        });

        //叩きつけ
        stateMachine.AddAnyTransition<StateAttack_Slam>((int)State.Attack_Slam);
        stateMachine.GetOrAddState<StateAttack_Slam>().SetDispatchStates(new State[1]
        {
            State.None
        });

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

        //TimeScaleが0以下の時は処理を終了
        if (Time.timeScale <= 0) return;

        //FPSカメラの時は、プレイヤーを非表示にする
        if (FPSCamera.activeSelf)
        {
            renderer.enabled = false;
        }
        else
        {
            renderer.enabled = true;
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
        if (FPSCamera.activeSelf)
        {
            transform.eulerAngles = new Vector3(0, FPSCamera.transform.eulerAngles.y, 0);
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
    [SerializeField] public GameObject FPSCamera;

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
    [SerializeField] Renderer renderer;

    [Header("エフェクト")]
    [SerializeField] public Effekseer.EffekseerEmitter effectWind;
    [SerializeField] public Effekseer.EffekseerEmitter effectJump;

    ChangeCamera changeCame;

    public bool isGround;

    public enum State
    {
        //メモリの種類
        None = MemoryType.None,
        Move_Dush = MemoryType.Dush,
        //ジャンプ
        Jump = MemoryType.Jump,
        Double_Jump = MemoryType.DowbleJump,
        //攻撃
        Attack_Punch = MemoryType.Punch,
        Attack_Slam = MemoryType.Slam,
        Attack_Tackle = MemoryType.Tackle,
        //防御
        Guard = MemoryType.Guard,
        //メモリの種類以外
        Idle = MemoryType.Count,
        //移動
        Move_Walk,
        Move_Run,
        Floating,
    }

    public float nowJumpSpeed;
    public float jumpAcceleration;

    public int[] possessionMemory { get; private set; }

    public EquipmentMemory[] equipmentMemories;
    public int currentEquipmentNo;

    // getter
    public Vector3 GetPos()
    {
        return transform.position;
    }

    public int GetCurrentStateKey()
    {
        return stateMachine.currentStateKey;
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
            if (hit.collider.CompareTag("Ground")
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

