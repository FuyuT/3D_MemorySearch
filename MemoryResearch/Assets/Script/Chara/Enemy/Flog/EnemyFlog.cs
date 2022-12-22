using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<EnemyFlog>;

public class EnemyFlog : CharaBase
{
    //////////////////////////////
    /// private

    StateMachine<EnemyFlog> stateMachine;

    /// <summary>
    /// MainStart
    /// </summary>
    void Start()
    {
        Init();
    }

    /// <summary>
    /// ������
    /// </summary>
    private void Init()
    {
        situation = (int)Situation.None;

        nowJumpDelayTime = JumpDelayTime;

        CharaBaseInit();
        //todo:param���� dataManager����̎Q�ƂɕύX
        param.Add((int)Enemy.ParamKey.PossesionMemory, Player.Event.None);
        param.Add((int)ParamKey.AttackPower, 0);

        param.Add((int)Enemy.ParamKey.Hp, HpMax);
        param.Set((int)Enemy.ParamKey.Hp, HpMax);

        StateMachineInit();
    }

    /// <summary>
    /// �X�e�[�g�}�V���̏�����
    /// </summary>
    void StateMachineInit()
    {
        stateMachine = new StateMachine<EnemyFlog>(this);

        stateMachine.AddAnyTransition<StateFlogMove>((int)Event.Move);

        //�X�e�[�g�}�V���̊J�n�@�����X�e�[�g�͈����Ŏw��
        stateMachine.Start(stateMachine.GetOrAddState<StateFlogMove>());
    }


    void Update()
    {
        if (param.Get<int>((int)Enemy.ParamKey.Hp) <= 0)
        {
            animator.SetBool("isDead", true);
            return;
        }

        //�X�e�[�g�}�V���X�V
        stateMachine.Update();
        currentState = stateMachine.currentStateKey;

        //�W�����v�ҋ@���Ԃ̍X�V
        if (nowJumpDelayTime > 0)
        {
            nowJumpDelayTime -= Time.deltaTime;
        }

        //�p�x�X�V
        RotateUpdate();

        //�ʒu�X�V
        PositionUpdate();
    }

    /// <summary>
    /// �p�x�̍X�V
    /// </summary>
    void RotateUpdate()
    {
        RotateUpdateToMoveVec();
    }

    /// <summary>
    /// �ʒu�̍X�V
    /// </summary>
    void PositionUpdate()
    {
        switch (situation)
        {
            //�W�����v���͏d�͂��g�p���Ȃ�
            case (int)Situation.Jump:
                objectParam.SetUseGravity(false);
                break;
            default:
                objectParam.SetUseGravity(true);
                break;
        }

        //�U���̎��͈ړ����~�߂�
        if (stateMachine.currentStateKey == (int)Event.Attack_Shot)
        {
            objectParam.InitMoveVec();
        }

        //�ړ�
        ObjectPositionUpdate(ObjectBase.MoveType.Rigidbody);

        SetAnimatarComponent();
    }

    //Animatar�v�f��ݒ�
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


    //////////////////////////////
    /// public

    /// <summary>
    /// �X�e�[�genum
    /// </summary>
    public enum Event
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
        Attack_Tackle,
        Attack_Shot,
    }

    public enum Situation
    {
        None,
        Jump,
        Jump_End,
        Floating,
        Dush,
    }

    [Header("�A�j���[�^�[")]
    [SerializeField] public Animator animator;

    //�X�R�[�v����������
    [SerializeField] public Transform TargetTransform;

    //todo:�r�w�C�r�A�Ɏ�������
    //�W�����v�֌W
    public float nowJumpDelayTime;

    public float nowJumpSpeed;

    public int situation;


    [Space]
    [Header("Hp")]
    [SerializeField] public int HpMax;

    [Space]
    [Header("�ړ�")]
    [SerializeField] public float MoveSpeed;

    [Header("�W�����v")]
    [Header("�W�����v�ɓ���܂ł̎���")]
    [SerializeField] public float JumpDelayTime;
    [Header("�i�ޗ�")]
    [SerializeField] public float JumpToTargetPower;

    [Header("����")]
    [SerializeField] public float JumpStartSpeed;
    [Header("�����l")]
    [SerializeField] public float JumpAcceleration;

    [Space]
    [Header("�d��")]
    [SerializeField] public float Gravity;

    [Space]
    [Header("���G����")]
    [SerializeField] public float SearchDistance;


    private void OnCollisionEnter(Collision collision)
    {
        //�n��
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("IsLanding", true);
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
        //�n��
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
        }

        //�U���͈�
        if (collision.gameObject.tag == "Player")
        {
            param.Set((int)Enemy.ParamKey.PossesionMemory, true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //�n��
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("IsLanding", false);

            switch (situation)
            {
                case (int)Situation.None:
                    //���V��Ԃ�
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
        }

        //�U���͈�
        if (collision.gameObject.tag == "Player")
        {
            param.Set((int)Enemy.ParamKey.PossesionMemory, false);
        }
    }
}
