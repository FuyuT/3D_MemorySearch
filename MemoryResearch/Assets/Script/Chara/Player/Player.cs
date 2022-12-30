using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;
public class Player : CharaBase, IReadPlayer
{
    /*******************************
    * private
    *******************************/
    //�A�N�^�[
    MyUtil.Actor<Player> actor;
    //�X�e�[�g�}�V��
    MyUtil.ActorStateMachine<Player> stateMachine;

    MyUtil.MoveType moveType;

    [Header("�n�ʂƂ̓����蔻��Ŏg�p���郌�C�̒���")]
    [SerializeField] float DirectionCheckHitGround;
    void Start()
    {
        Init();
        if (readPlayer == null)
        {
            readPlayer = this;
        }
    }

    private void Init()
    {
        nowJumpSpeed = 0.0f;
        dushSpeed = Vector3.zero;
        nowDushTime = 0;
        nowDushDelayTime = DushDelayTime;
        isGround = true;
        possessionMemory = new int[MemoryMax];
        for (int n = 0; n < MemoryMax; n++)
        {
            possessionMemory[n] = 0;
        }

        possessionMemory[0] = (int)Event.Double_Jump;

        CharaBaseInit();

        charaParam.hp = HpMax;

        StateMachineInit();
        actor.Transform.Init();
    }

    void StateMachineInit()
    {
        //�ړ��L�[��������Ă���Ȃ�ړ�
        stateMachine.AddTransition<StateIdle, StateMove>((int)Event.Move);

        //�^�b�N���L�[��������Ă���Ȃ�^�b�N��
        stateMachine.AddAnyTransition<StateTackle>((int)Event.Attack_Tackle);

        //�W�����v�{�^���ŃW�����v
        stateMachine.AddAnyTransition<StateJump>((int)Event.Jump);
        stateMachine.AddAnyTransition<StateDoubleJump>((int)Event.Double_Jump);

        //�G�A�_�b�V��
        stateMachine.AddAnyTransition<StateAirDush>((int)Event.Air_Dush);

        //�p���`
        stateMachine.AddAnyTransition<StateAttack_Punch>((int)Event.Attack_Punch);

        //����������Ă��Ȃ��Ȃ�ҋ@��Ԃ�
        stateMachine.AddAnyTransition<StateIdle>((int)Event.Idle);

        //�X�e�[�g�}�V���̊J�n�@�����X�e�[�g�͈����Ŏw��
        stateMachine.Start(stateMachine.GetOrAddState<StateIdle>());
    }

    void Update()
    {
        if (IsDead())
        {
            animator.SetBool("isDead", true);
            return;
        }

        //FPS�J�����̎��́A�v���C���[���\���ɂ���
        if (ChapterCamera.activeSelf)
        {
            playerRenderer.enabled = false;
        }
        else
        {
            playerRenderer.enabled = true;
        }

        //�X�e�[�g�}�V���X�V
        stateMachine.Update();

        //�p�x�X�V
        RotateUpdate();

        //�ʒu�X�V
        PositionUpdate();

        //Delay�̍X�V
        DelayTimeUpdate();

        //�n�ʂɒ��n���Ă��邩�m�F����
        CheckCollisionGround();
    }

    //�p�x�X�V
    void RotateUpdate()
    {
        //��l�̎��̊p�x�ύX
        if (ChapterCamera.activeSelf)
        {
            transform.eulerAngles = new Vector3(0, ChapterCamera.transform.eulerAngles.y, 0);
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

    //�ʒu�X�V
    void PositionUpdate()
    {
        moveType = MyUtil.MoveType.Rigidbody;
        switch (stateMachine.currentStateKey)
        {
            //�W�����v��
            case (int)Event.Jump:
            case (int)Event.Double_Jump:
                //�d�͂��g�p���Ȃ�
                actor.IVelocity().SetUseGravity(false);
                break;
            default:
                //�x�N�g����ݒ�i�d�͂������Ă����j
                actor.IVelocity().SetUseGravity(true);
                break;
        }
        actor.Transform.PositionUpdate(moveType);

        //velocity��������
        actor.IVelocity().InitVelocity();

        SetAnimatarComponent();
    }

    //�A�j���[�^�[�ɗv�f��ݒ�
    void SetAnimatarComponent()
    {
        float speed = 0;
        if (Mathf.Abs(rigidbody.velocity.x) + Mathf.Abs(rigidbody.velocity.z) > 0)
        {
            speed = 1;
        }

        animator.SetFloat("Speed", speed);
        animator.SetFloat("Speed_Y", rigidbody.velocity.y);
        animator.SetInteger("StateNo", (int)stateMachine.currentStateKey);
    }

    //�f�B���C���Ԃ̍X�V
    void DelayTimeUpdate()
    {
        if (nowDushDelayTime > 0)
        {
            nowDushDelayTime -= Time.deltaTime;
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
    [Header("�`���v�^�[�J����")]
    [SerializeField] public GameObject ChapterCamera;

    [Header("�A�j���[�^�[")]
    [SerializeField] public Animator animator;

    [Space]
    [Header("�U���͈�")]
    [SerializeField] public AttackRange Attack_Punch;
    [SerializeField] public AttackRange Attack_Tackle;

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

    [Header("�J�����}�l�[�W���[")]
    [SerializeField] CameraManager camemana;

    [Header("�v���C���[��Renderer")]
    [SerializeField] Renderer playerRenderer;

    ChangeCamera changeCame;

    public bool isGround;

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

    public enum AttackInfo
    {
        Attack_Not_Possible,
        Attack_Possible,
        Attack_End,
    }

    public float nowJumpSpeed;
    public float jumpAcceleration;

    public Vector3 dushSpeed;
    public float nowDushTime;

    public int[] possessionMemory { get; private set; }

    // getter
    public Vector3 GetPos()
    {
        return transform.position;
    }

    //���������������Ă��邩�m�F����
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
    //��������ݒ�ł���z��ԍ����擾����
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
    //��������ݒ肷��
    public void SetPossesionMemory(int memory, int arrayValue)
    {
        //todo:�f�o�b�O:�ݒ肵���s���������\���p
        switch(memory)
        {
            case (int)Event.Jump:
                Debug.Log("�W�����v�o�^");
                break;
            case (int)Event.Double_Jump:
                Debug.Log("�_�u���W�����v�o�^");
                break;
            default:
                break;
        }

        possessionMemory[arrayValue] = memory;
    }

    /*******************************
    * �Փ˔���
    *******************************/

    private void CheckCollisionGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * DirectionCheckHitGround, Color.red);
        if (Physics.Raycast(ray,out hit, DirectionCheckHitGround)) //���x��0�̎��ɗႪ0�ɂȂ�Ȃ��悤��+RayAdjust���Ă���
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

