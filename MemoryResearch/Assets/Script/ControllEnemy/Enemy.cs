using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Enemy>;

public class Enemy : MonoBehaviour
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

    StateMachine<Enemy> stateMachine;

    //�X�R�[�v����������
    [SerializeField] public Transform PlayerTransform;
    public AnyParameterMap parameter;

    [SerializeField] public Vector3 moveVec;

    //�W�����v�֌W
    public float nowJumpDelayTime;

    public float nowJumpSpeed;
    public float jumpAcceleration;
    public bool isJump;
    public bool isFloating;

    [SerializeField] IReaderActor playerReadActor = new Actor();

    [Space]
    [Header("�W�����v")]
    [Header("�W�����v�ɓ���܂ł̎���")]
    [SerializeField] public float JumpDelayTime;

    [Header("����")]
    [SerializeField] public float JumpStartSpeed;
    [Header("�����l")]
    [SerializeField] public float JumpAcceleration;
    [Header("�d��")]
    [SerializeField] public float Weight;

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
        parameter = new AnyParameterMap();

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
        isJump = false;
        isFloating = false;
        nowJumpDelayTime = JumpDelayTime;
        nowDushDelayTime = DushDelayTime;

        parameter = new AnyParameterMap();
        parameter.Add("�U���\����", false);
        parameter.Add("����������", (int)Player.Event.Idle);

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

        stateMachine.AddAnyTransition<StateEnemyAttack>((int)Event.Attack_Punch);

        stateMachine.AddAnyTransition<StateEnemyTackle>((int)Event.Attack_Tackle);

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

        //�ʒu�X�V
        //���V���Ă�����d�͂��v�Z�ɒǉ�
        if (isFloating)
        {
            if (!isJump)
            {
                moveVec -= new Vector3(0, Weight + Gravity, 0);
            }
        }

        if(moveVec.y > 0)
        {
            Debug.Log("�㏸��" + moveVec.y);
        }

        transform.position += moveVec * Time.deltaTime;
        moveVec = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�n��
        if (collision.gameObject.tag == "Ground")
        {
            if (isFloating)
            {
                Debug.Log("���n����");
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
            if (isFloating)
            {
                isFloating = false;
                isJump = false;
                nowJumpSpeed = 0.0f;
            }
        }

        //�U���͈�
        if (collision.gameObject.tag == "Player")
        {
            parameter.Set("�U���\����", true);
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

        //�U���͈�
        if (collision.gameObject.tag == "Player")
        {
            parameter.Set("�U���\����", false);
        }
    }
}
