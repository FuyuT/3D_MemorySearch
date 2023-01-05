using UnityEngine;

using State = MyUtil.ActorState<EnemyCrab>;

public class EnemyCrab : CharaBase
{
    /*******************************
    * private
    *******************************/
    [Header("範囲")]
    [SerializeField] public MyUtil.TargetCollider searchRange;
    [SerializeField] public MyUtil.TargetCollider attackRange;

    [Header("速度")]
    [SerializeField] public float moveSpeed;
    [SerializeField] float rotateSpeed;

    [Header("HP")]
    [SerializeField] public int hpMax;


    [Header("ディレイ")]
    [SerializeField] public float delayGuardMax;
    [HideInInspector]public float delayGuard;
    //アクター
    MyUtil.Actor<EnemyCrab> actor;
    //ステートマシン
    MyUtil.ActorStateMachine<EnemyCrab> stateMachine;

    private void Awake()
    {
        Init();
        StateMachineInit();
        actor.Transform.Init();
    }
    void Init()
    {
        CharaBaseInit();

        //重力を使用する
        actor.IVelocity().SetUseGravity(true);

        delayGuard = 0;

        charaParam.hp = hpMax;
    }
    void StateMachineInit()
    {
        stateMachine.AddAnyTransition<StateCrabIdle>((int)State.Idle);
        stateMachine.AddAnyTransition<StateCrabMove>((int)State.Move);
        stateMachine.AddAnyTransition<StateCrabAttack>((int)State.Attack);
        stateMachine.AddAnyTransition<StateCrabDefense>((int)State.Defense);

        stateMachine.Start(stateMachine.GetOrAddState<StateCrabIdle>());
    }

    void Update()
    {
        //プレイヤーの実体がなければ終了
        if (Player.readPlayer == null) return;

        if (IsDead())
        {
            if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
            {
                animator.SetTrigger("Dead");
                //当たり判定を消す
                this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                this.gameObject.GetComponent<Rigidbody>().useGravity = false;
                rigidbody.velocity = Vector3.zero;
            }
            return;
        }

        stateMachine.Update();

        UpdateRotate();

        UpdatePosition();

        UpdateDelay();

        CharaUpdate();
    }
    //角度更新
    void UpdateRotate()
    {
        Vector3 temp = actor.IVelocity().GetVelocity();
        temp.y = 0;
        if (temp != Vector3.zero)
        {
            actor.Transform.RotateUpdateToVec(temp, rotateSpeed);
        }
    }
    //位置更新
    void UpdatePosition()
    {
        //位置を更新
        actor.Transform.PositionUpdate(MyUtil.MoveType.Rigidbody);

        //velocityを初期化
        actor.IVelocity().InitVelocity();
    }
    void UpdateDelay()
    {
        if(delayGuard < delayGuardMax)
        {
            delayGuard += Time.deltaTime;
        }
    }

    /*******************************
    * public
    *******************************/
    public enum State
    {
        Idle,
        Move,
        Attack,
        Defense,
    }


    //コンストラクタ
    public EnemyCrab()
    {
        actor = new MyUtil.Actor<EnemyCrab>(this);
        stateMachine = new MyUtil.ActorStateMachine<EnemyCrab>(this, ref actor);
    }
}