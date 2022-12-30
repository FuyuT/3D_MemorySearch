using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyFlog>;

public class EnemyFlog : CharaBase
{
    /*******************************
    * private
    *******************************/
    //�A�N�^�[
    MyUtil.Actor<EnemyFlog> actor;
    //�X�e�[�g�}�V��
    MyUtil.ActorStateMachine<EnemyFlog> stateMachine;

    [Header("�n�ʂƂ̓����蔻��Ŏg�p���郌�C�̒���")]
    [SerializeField] float DirectionCheckHitGround;

    private void Awake()
    {
        Init();
        StateMachineInit();
        actor.Transform.Init();
    }

    private void Init()
    {
        CharaBaseInit();

        delayJumpTime = 0;
        delayShotTime = 0;
        charaParam.hp = HpMax;
    }
    // �X�e�[�g�}�V���̏�����
    void StateMachineInit()
    {
        stateMachine.AddAnyTransition<StateFlogIdle>((int)State.Idle);
        stateMachine.AddAnyTransition<StateFlogJump>((int)State.Jump);
        stateMachine.AddAnyTransition<StateFlogShot>((int)State.Attack_Shot);
        stateMachine.AddAnyTransition<StateFlogAttackTongue>((int)State.Attack_Tongue);

        //�X�e�[�g�}�V���̊J�n�@�����X�e�[�g�͈����Ŏw��
        stateMachine.Start(stateMachine.GetOrAddState<StateFlogIdle>());
    }

    void Update()
    {
        if (IsDead())
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Damage_Dead"))
            {
                Debug.Log("���S");
                animator.SetTrigger("Damage_Dead");
                //�����蔻�������
                this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                this.gameObject.GetComponent<Rigidbody>().useGravity = false;
                rigidbody.velocity = Vector3.zero;
            }
            return;
        }

        //�X�e�[�g�}�V���X�V
        stateMachine.Update();


        //�p�x�X�V
        RotateUpdate();

        //�ʒu�X�V
        PositionUpdate();

        //Delay�̍X�V
        DelayUpdate();

        //�n�ʂɒ��n���Ă��邩�m�F����
        CheckCollisionGround();
    }

    //�p�x�̍X�V
    void RotateUpdate()
    {
        Vector3 temp = actor.IVelocity().GetVelocity();
        temp.y = 0;
        if (temp != Vector3.zero)
        {
            actor.Transform.RotateUpdateToVec(temp, rotateSpeed);
        }
    }

    ///�ʒu�̍X�V
    void PositionUpdate()
    {
        //�d�͂̕ύX
        switch (stateMachine.currentStateKey)
        {
            //�W�����v���͏d�͂��g�p���Ȃ�
            case (int)State.Jump:
                actor.IVelocity().SetUseGravity(false);
                break;
            default:
                actor.IVelocity().SetUseGravity(true);
                break;
        }

        //�U���̎��͈ړ����~�߂�
        if (stateMachine.currentStateKey == (int)State.Attack_Shot)
        {
            actor.IVelocity().InitVelocity();
        }

        //�ړ�
        actor.Transform.PositionUpdate(MyUtil.MoveType.Rigidbody);
    }

    void DelayUpdate()
    {
        //�W�����v�ҋ@���Ԃ̍X�V
        if (delayJumpTime < delayJumpTimeMax)
        {
            delayJumpTime += Time.deltaTime;
        }

        //�ˌ��ҋ@���Ԃ̍X�V
        if (delayShotTime < delayShotTimeMax)
        {
            delayShotTime += Time.deltaTime;
        }
    }


    /*******************************
    * public
    *******************************/

    /// <summary>
    /// �X�e�[�genum
    /// </summary>
    public enum State
    {
        Idle,
        //�ړ�
        Move,
        //�W�����v
        Jump,
        Floating,
        //�K�[�h
        Defense,
        //�U��
        Attack_Punch,
        Attack_Tongue,
        Attack_Shot,
    }
    [Header("�A�j���[�^�[")]
    [SerializeField] public Animator animator;

    [Header("�͈�")]
    [SerializeField] public MyUtil.TargetCollider searchRange;
    [SerializeField] public MyUtil.TargetCollider attackRange;

    [Header("���x")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float rotateSpeed;

    [Space]
    [Header("HP")]
    [SerializeField] public int HpMax;

    [Header("�^�b�N��")]
    [SerializeField] public float delayTackleMax;
    [HideInInspector] public float delayTackle;
    [SerializeField] public float tackleTimeMax;
    [HideInInspector] public float tackleTime;
    [SerializeField] public float tackleSpeed;


    [Header("�ˌ�")]
    [SerializeField] public float delayShotTimeMax;
    public float delayShotTime;

    [SerializeField] public float ShotSpeed;
    [SerializeField] public int ShotDamage;
    [SerializeField] public GameObject bullet;
    [SerializeField] public float ShotInterval;

    [Header("�W�����v")]
    [Header("�W�����v�ɓ���܂ł̎���")]
    [SerializeField] public float delayJumpTimeMax;
    [Header("�i�ޗ�")]
    [SerializeField] public float JumpToTargetPower;

    [Header("����")]
    [SerializeField] public float JumpStartSpeed;
    [Header("�����l")]
    [SerializeField] public float JumpAcceleration;

    [Space]
    [Header("�d��")]
    [SerializeField] public float Gravity;

    public bool isGround;

    //�W�����v�֌W
    public float delayJumpTime;

    public float nowJumpSpeed;


    //�R���X�g���N�^
    public EnemyFlog()
    {
        actor = new MyUtil.Actor<EnemyFlog>(this);
        stateMachine = new MyUtil.ActorStateMachine<EnemyFlog>(this, ref actor);
    }

    /*******************************
    * �Փ˔���
    *******************************/

    private void CheckCollisionGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * DirectionCheckHitGround, Color.red);
        if (Physics.Raycast(ray, out hit, DirectionCheckHitGround)) //���x��0�̎��ɗႪ0�ɂȂ�Ȃ��悤��+RayAdjust���Ă���
        {

            //�n�ʂɃ��C���������Ă��āA�v���C���[��Velocity���㏸���Ă��Ȃ���
            if (hit.collider.gameObject.CompareTag("Ground")
                && actor.IVelocity().GetState() != MyUtil.VelocityState.isUp)
            {
                isGround = true;
            }
        }
        //���ɂ��������Ă��Ȃ��Ȃ�
        else
        {
            isGround = false;
        }
    }
}
