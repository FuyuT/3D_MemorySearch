using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;

public class Player : MonoBehaviour
{
    [Header("�ړ�")]
    [Header("�������̑��x")]
    [SerializeField] public float MoveSpeed;
    [Header("���鎞�̑��x")]
    [SerializeField] public float RunSpeed;

    [Space]
    [Header("�W�����v")]
    [Header("����")]
    [SerializeField] public float JumpStartSpeed;
    [Header("�����l")]
    [SerializeField] public float JumpAcceleration;
    [Header("�d��")]
    [SerializeField] public float Weight;

    [Space]
    [Header("�d��")]
    [SerializeField] public float JumpDecreaseValue;

    [Space]
    [Header("�_�b�V��")]
    [Header("����")]
    [SerializeField] public float DushStartSpeed;
    [Header("�����l")]
    [SerializeField] public float DushAcceleration;
    [Header("�ړ�����")]
    [SerializeField] public float DushTime;

    //�A�N�^�[
    IActor actor;
    public static IReaderActor readActor = new Actor();
    /// <summary>
    /// �X�e�[�genum
    /// </summary>
    public enum Event
    {
        Idle,
        //�ړ�
        Move,
        Air_Dush,
        //�W�����v
        Jump,
        Double_Jump,
        Floating,
        //�U��
        Attack_Punch,
        Attack_Tackle,
    }

    public float nowJumpSpeed;
    public float jumpAcceleration;
    public bool isFloating;
    public bool isJump;

    public Vector3 moveVec;

    public Vector3 dushVec;
    public float nowDushTime;

    StateMachine<Player> stateMachine;

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    Player()
    {
        actor = new Actor();
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
        isFloating = false;
        isJump = false;
        nowJumpSpeed = 0.0f;
        dushVec = Vector3.zero;
        nowDushTime = 0;

        StateMachineInit();
    }

    /// <summary>
    /// �X�e�[�g�}�V���̏�����
    /// </summary>
    void StateMachineInit()
    {
        stateMachine = new StateMachine<Player>(this);

        //�ړ��L�[��������Ă���Ȃ�ړ�
        stateMachine.AddTransition<StateIdle, StateMove>((int)Event.Move);

        //�^�b�N���L�[��������Ă���Ȃ�^�b�N��@
        stateMachine.AddAnyTransition<StateTackle>((int)Event.Attack_Tackle);

        //�W�����v�{�^���ŃW�����v
        stateMachine.AddAnyTransition<StateDoubleJump>((int)Event.Jump);
        stateMachine.AddAnyTransition<StateDoubleJump>((int)Event.Double_Jump);

        //�G�A�_�b�V��
        stateMachine.AddAnyTransition<StateAirDush>((int)Event.Air_Dush);

        //����������Ă��Ȃ��Ȃ�ҋ@��Ԃ�
        stateMachine.AddAnyTransition<StateIdle>((int)Event.Idle);

        //�X�e�[�g�}�V���̊J�n�@�����X�e�[�g�͈����Ŏw��
        stateMachine.Start(stateMachine.GetOrAddState<StateIdle>());
    }

    /// <summary>
    /// MainUpdate
    /// </summary>
    void Update()
    {
        //�X�e�[�g�}�V���X�V
        stateMachine.Update();

        //�ʒu�X�V
        //���V���Ă�����d�͂��v�Z�ɒǉ�
        if(isFloating)
        {
            if(!isJump)
            {
                //moveVec -= new Vector3(0, Weight + Gravity, 0);
            }
        }
        transform.position += moveVec * Time.deltaTime;
        moveVec = Vector3.zero;

        actor.TransformUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�n��
        if (collision.gameObject.tag == "Ground")
        {
            if (isFloating)
            {
                isFloating = false;
                isJump = false;
                nowJumpSpeed = 0.0f;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //�n��
        if (collision.gameObject.tag == "Ground")
        {
            if (nowJumpSpeed < 0)
            {
                isFloating = false;
                isJump = false;
                nowJumpSpeed = 0.0f;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //�n��
        if (collision.gameObject.tag == "Ground")
        {
            //���V��Ԃ�
            isFloating = true;
        }
    }
}

