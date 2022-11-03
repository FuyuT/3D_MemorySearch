using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;

public class Player : CharaBase
{
    [Header("�`���v�^�[�J����")]
    [SerializeField] GameObject ChapterCamera;

    [Header("�A�j���[�^�[")]
    [SerializeField] Animator animator;

    [Space]
    [Header("�X�e�[�g�����\��������")]
    [SerializeField] int MemoryMax;

    [Space]
    [Header("��]")]
    [Header("��]���x(��b�ŕς���)")]
    [SerializeField] float RotateSpeed;

    [Space]
    [Header("Hp")]
    [SerializeField] public int HpMax;

    [Space]
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

    [Space]
    [Header("�d��")]
    [SerializeField] public float JumpDecreaseValue;

    [Space]
    [Header("�_�b�V��")]
    [Header("�f�B���C�b��")]
    [SerializeField] public float DushDelayTime;
    public float nowDushDelayTime;

    [Header("����")]
    [SerializeField] public float DushStartSpeed;
    [Header("�����l")]
    [SerializeField] public float DushAcceleration;
    [Header("�ړ�����")]
    [SerializeField] public float DushTime;

    //�A�N�^�[
    /// <summary>
    /// �X�e�[�genum
    /// </summary>
    public enum Event
    {
        None,
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

    public enum Situation
    {
        None,
        Jump,
        Jump_End,
        Floating,
        Dush,
    }

    public enum AttackInfo
    {
        Attack_Not_Possible,
        Attack_Possible,
        Attack_End,
    }

    public float nowJumpSpeed;
    public float jumpAcceleration;

    public int situation;

    public Vector3 moveVec;

    public Vector3 dushVec;
    public float nowDushTime;

    StateMachine<Player> stateMachine;


    public int[] possessionMemory { get; private set; }

    /// <summary>
    /// ���������������Ă��邩�m�F����
    /// �������Ă����true�A���Ȃ����false
    /// </summary>
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


    //todo:�d�����Ă��鋭�����������Ɍ�������
    /// <summary>
    /// �󂢂Ă���z��ԍ����m�F����
    /// �󂢂Ă���z��ԍ���Ԃ�
    /// �����ꍇ�A���łɂ���ꍇ��-1��Ԃ�
    /// </summary>
    public int GetMemoryArrayNullValue()
    {
        for (int n = 0; n < MemoryMax; n++)
        {
            if (possessionMemory[n] == 0)
            {
                return n;
            }
        }

        return -1;
    }


    /// <summary>
    /// ��������ݒ肷��
    /// </summary>
    public void SetPossesionMemory(int memory, int arrayValue)
    {
        if (memory == (int)Event.Jump)
        {
            Debug.Log("�W�����v�o�^");
        }
        possessionMemory[arrayValue] = memory;
    }

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    Player()
    {
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
        nowJumpSpeed = 0.0f;
        dushVec = Vector3.zero;
        nowDushTime = 0;
        nowDushDelayTime = DushDelayTime;

        possessionMemory = new int[MemoryMax];
        for(int n = 0; n < MemoryMax; n++)
        {
            possessionMemory[n] = 0;
        }

        CharaBaseInit();
        Debug.Log(param);
        param.Add((int)ParamKey.AttackPower, 0);
        param.Add((int)ParamKey.Attack_Info, (int)AttackInfo.Attack_Not_Possible);
        param.Add((int)Enemy.ParamKey.Hp, HpMax);

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

        //�^�b�N���L�[��������Ă���Ȃ�^�b�N��
        stateMachine.AddAnyTransition<StateTackle>((int)Event.Attack_Tackle);

        //�W�����v�{�^���ŃW�����v
        stateMachine.AddAnyTransition<StateJump>((int)Event.Jump);
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

        //�p�x�X�V
        RotateUpdate();

        //�ʒu�X�V
        PositionUpdate();

        //Delay�̍X�V
        DelayTimeUpdate();

        //���݂̃X�e�[�g��\��
        //Debug.Log(stateMachine.currentStateKey);
       // Debug.Log("situation:" + situation);

    }

    /// <summary>
    /// �p�x�̍X�V
    /// </summary>
    void RotateUpdate()
    {
        //��l�̎��̊p�x�ύX
        if (ChapterCamera.activeSelf)
        {
            transform.rotation = ChapterCamera.transform.rotation;
        }
        else
        {
            Vector3 temp = moveVec;
            temp.y = 0;
            if (temp != Vector3.zero)
            {
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
                //�d�͂��g�p���Ȃ�
                rb.velocity = moveVec;
                break;
            //�_�b�V����
            case (int)Situation.Dush:
                //�������Ȃ��悤�ɂ���
                moveVec.y = 0;
                rb.velocity = moveVec;
                break;
            default:
                //�x�N�g����ݒ�i�d�͂������Ă����j
                rb.velocity = moveVec + new Vector3(0, rb.velocity.y, 0);
                break;
        }
        moveVec = Vector3.zero;

        float speed = 0;

        if (Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z) > 0)
        {
            speed = 1;
        }

        animator.SetFloat("Speed", speed);
        animator.SetFloat("Speed_Y", rb.velocity.y);

    }

    /// <summary>
    /// �f�B���C���Ԃ̍X�V
    /// </summary>
    void DelayTimeUpdate()
    {
        if(nowDushDelayTime > 0)
        {
            nowDushDelayTime -= Time.deltaTime;
        }
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
    }
}

