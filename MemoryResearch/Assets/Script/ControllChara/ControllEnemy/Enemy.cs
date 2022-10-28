using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Enemy>;

public class Enemy : CharaBase
{
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
    }

    public enum Situation
    {
        None,
        Jump,
        Jump_End,
        Floating,
        Dush,
    }


    StateMachine<Enemy> stateMachine;

    //�X�R�[�v����������
    [SerializeField] public Transform PlayerTransform;

    [SerializeField] public Vector3 moveVec;

    //�W�����v�֌W
    public float nowJumpDelayTime;

    public float nowJumpSpeed;
    public float jumpAcceleration;

    public int situation;

    [SerializeField] IReaderActor playerReadActor = new Actor();

    [Header("��]���x(��b�ŕς���)")]
    [SerializeField] float RotateSpeed;

    [Space]
    [Header("Hp")]
    [SerializeField] public int HpMax;

    [Space]
    [Header("�ړ�")]
    [Header("�������̑��x")]
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
    [Header("�_�b�V��")]
    [Header("�_�b�V���ɓ���܂ł̎���")]
    [SerializeField] public float DushDelayTime;

    [Header("����")]
    [SerializeField] public float DushStartSpeed;
    [Header("�����l")]
    [SerializeField] public float DushAcceleration;
    [Header("�ړ�����")]
    [SerializeField] public float DushTime;

    [Space]
    [Header("���G����")]
    [SerializeField] public float SearchDistance;

    public Vector3 dushVec;
    public float nowDushTime;
    public float nowDushDelayTime;



    //todo:unity��Őݒu����Ă�I�u�W�F�N�g�̓R���X�g���N�^�ʂ�Ȃ��H
    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    Enemy()
    {
        Init();
    }

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
        nowDushDelayTime = DushDelayTime;

        CharaBaseInit();
        param.Add((int)Enemy.ParamKey.PossesionMemory, Player.Event.None);
        param.Add((int)ParamKey.AttackPower, 0);

        param.Add((int)Enemy.ParamKey.Hp, HpMax);


        StateMachineInit();


    }

    /// <summary>
    /// �X�e�[�g�}�V���̏�����
    /// </summary>
    void StateMachineInit()
    {
        stateMachine = new StateMachine<Enemy>(this);

        stateMachine.AddAnyTransition<StateEnemyMove>((int)Event.Move);

        stateMachine.AddAnyTransition<StateEnemyJump>((int)Event.Jump);

        //stateMachine.AddAnyTransition<StateEnemyAttack>((int)Event.Attack_Punch);

        //stateMachine.AddAnyTransition<StateEnemyTackle>((int)Event.Attack_Tackle);

        //�X�e�[�g�}�V���̊J�n�@�����X�e�[�g�͈����Ŏw��
        stateMachine.Start(stateMachine.GetOrAddState<StateEnemyMove>());
    }


    // Update is called once per frame
    void Update()
    {
        //�X�e�[�g�}�V���X�V
        stateMachine.Update();

        //�W�����v�ҋ@���Ԃ̍X�V
        if(nowJumpDelayTime > 0)
        {
            nowJumpDelayTime -= Time.deltaTime;
        }
        
        //�_�b�V���ҋ@���Ԃ̍X�V
        if(nowDushDelayTime > 0)
        {
            nowDushDelayTime -= Time.deltaTime;
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
        //todo:���݂̃X�e�[�gkey���擾����֐����쐬����
        if (moveVec != Vector3.zero)
        {
            //�󒆂ɂ��鎞�́Ay����]�̗v�f�ɉ�����
            if (moveVec.y != 0)
            {
                var quaternion = Quaternion.LookRotation(moveVec);
                transform.rotation = Quaternion.Slerp(this.transform.rotation, quaternion, RotateSpeed * Time.deltaTime);
            }
            else
            {
                //y�͉�]�̗v�f�ɉ����Ȃ�
                Vector3 temp = moveVec;
                temp.y = 0;
                var quaternion = Quaternion.LookRotation(temp);
                transform.rotation = Quaternion.Slerp(this.transform.rotation, quaternion, RotateSpeed * Time.deltaTime);
            }
        }
    }

    /// <summary>
    /// �ʒu�̍X�V
    /// </summary>
    void PositionUpdate()
    {
        var rb = GetComponent<Rigidbody>();

        switch (situation)
        {
            //�W�����v��
            case (int)Situation.Jump:
                //�W�����v���͏d�͂��g�p���Ȃ�
                rb.velocity = moveVec;
                break;
            //����ȊO
            default:
                //�x�N�g����ݒ�i�d�͂������Ă����j
                rb.velocity = moveVec + new Vector3(0, rb.velocity.y, 0);
                break;
        }

        moveVec = Vector3.zero;
    }


    private void OnCollisionEnter(Collision collision)
    {
        //�n��
        if (collision.gameObject.tag == "Ground")
        {
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
