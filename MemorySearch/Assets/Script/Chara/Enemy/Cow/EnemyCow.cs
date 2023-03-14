using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyCow>;

public class EnemyCow : EnemyBase
{
    /*******************************
    * private
    *******************************/
    //�A�N�^�[
    MyUtil.Actor<EnemyCow> actor;
    //�X�e�[�g�}�V��
    MyUtil.ActorStateMachine<EnemyCow> stateMachine;

    private void Awake()
    {
        Init();
        StateMachineInit();
        actor.Transform.Init();
    }
    private void Init()
    {
        CharaBaseInit();
        charaParam.hp = hpMax;

        mainMemory = MemoryType.Dush;
    }
    void StateMachineInit()
    {
        stateMachine.AddAnyTransition<StateCowIdle>((int)State.Idle);
        stateMachine.AddAnyTransition<StateCowMove>((int)State.Move);
        stateMachine.AddAnyTransition<StateCowTackle>((int)State.Attack_Tackle);
        stateMachine.AddAnyTransition<StateCowDead>((int)State.Dead);

        stateMachine.Start(stateMachine.GetOrAddState<StateCowIdle>());
    }

    private void Update()
    {
        //�v���C���[�̎��̂��Ȃ���ΏI��
        if (Player.readPlayer == null) return;

        if (IsDead())
        {
            if (stateMachine.currentStateKey != (int)State.Dead)
            {
                stateMachine.Dispatch((int)State.Dead);
            }
            stateMachine.Update();
            return;
        }

        if (!UpdateCharaBase())
        {
            if (stateMachine.currentStateKey != (int)State.Idle)
            {
                stateMachine.Dispatch((int)State.Idle);
            }
            return;
        }

        stateMachine.Update();

        UpdateRotate();

        UpdatePosition();

        UpdateDelay();
    }

    //�p�x�X�V
    void UpdateRotate()
    {
        Vector3 temp = actor.IVelocity().GetVelocity();
        temp.y = 0;
        if (temp != Vector3.zero)
        {
            actor.Transform.RotateUpdateToVec(temp, rotateSpeed);
        }
    }

    //�ʒu�X�V
    void UpdatePosition()
    {
        //�ʒu���X�V
        actor.Transform.PositionUpdate(MyUtil.MoveType.Rigidbody);

        //velocity��������
        actor.IVelocity().InitVelocity();
    }

    void UpdateDelay()
    {
        //�T�m�͈͂Ƀ^�[�Q�b�g�������Ă��Ȃ���ΏI��
        if (!searchRange.InTarget) return;
        if (delayTackle < delayTackleMax)
        {
            delayTackle += Time.deltaTime;
        }
    }
    

    /*******************************
    * public
    *******************************/
    public enum State
    {
        Idle,
        Move,
        Attack_Tackle,
        Dead,
    }

    [Header("�͈�")]
    [SerializeField] public MyUtil.TargetCollider searchRange;
    [SerializeField] public MyUtil.TargetCollider attackRange;

    [Header("���x")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float rotateSpeed;

    [Header("HP")]
    [SerializeField] public int hpMax;

    [Header("�^�b�N��")]
    [SerializeField]  public float delayTackleMax;
    [HideInInspector] public float delayTackle;
    [SerializeField]  public float tackleTimeMax;
    [HideInInspector] public float tackleTime;
    [SerializeField]  public float tackleSpeed;

    [Header("���f����Renderer")]
    [SerializeField] public Renderer renderer;

    [Header("�G�t�F�N�g")]
    [SerializeField] public Effekseer.EffekseerEmitter effectTackle;

    [Space]
    [Header("SE�֘A")]
    [SerializeField] public AudioClip AttackSE;
    [SerializeField] public AudioClip DownSE;
    [SerializeField] public AudioClip ExplosionSE;

    public EnemyCow()
    {
        actor = new MyUtil.Actor<EnemyCow>(this);
        stateMachine = new MyUtil.ActorStateMachine<EnemyCow>(this, ref actor);
    }

}