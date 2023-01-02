using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyCow>;

public class EnemyCow : CharaBase
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
    }
    void StateMachineInit()
    {
        stateMachine.AddAnyTransition<StateCowIdle>((int)State.Idle);
        stateMachine.AddAnyTransition<StateCowMove>((int)State.Move);
        stateMachine.AddAnyTransition<StateCowTackle>((int)State.Attack_Tackle);

        stateMachine.Start(stateMachine.GetOrAddState<StateCowIdle>());
    }

    private void Update()
    {
        if (IsDead())
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Damage_Dead"))
            {
                animator.SetTrigger("Damage_Dead");
                //�����蔻�������
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
    }

    [Header("�A�j���[�^�[")]
    [SerializeField] public Animator animator;

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

    public EnemyCow()
    {
        actor = new MyUtil.Actor<EnemyCow>(this);
        stateMachine = new MyUtil.ActorStateMachine<EnemyCow>(this, ref actor);
    }

}