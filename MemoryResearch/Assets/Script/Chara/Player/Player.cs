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
        nowDushDelayTime = DushDelayTime;
        isGround = false;
        actor.IVelocity().SetUseGravity(true);
        possessionMemory = new int[MemoryMax];
        for (int n = 0; n < MemoryMax; n++)
        {
            possessionMemory[n] = 0;
        }

        possessionMemory[0] = (int)State.Double_Jump;

        CharaBaseInit();

        charaParam.hp = HpMax;

        StateMachineInit();
        actor.Transform.Init();

        //���x�̏���l��ݒ�
        actor.IVelocity().SetMaxVelocityY(120);
    }

    void StateMachineInit()
    {
        //����
        stateMachine.AddTransition<StateIdle, StateMoveWalk>((int)State.Move_Walk);
        //����
        stateMachine.AddAnyTransition<StateMoveRun>((int)State.Move_Run);
        //�K�[�h
        stateMachine.AddAnyTransition<StateGuard>((int)State.Guard);

        //�_�b�V��
        stateMachine.AddAnyTransition<StateDush>((int)State.Move_Dush);

        //�W�����v
        stateMachine.AddAnyTransition<StateJump>((int)State.Jump);
        stateMachine.AddAnyTransition<StateDoubleJump>((int)State.Double_Jump);

        //�^�b�N��
        stateMachine.AddAnyTransition<StateAttackTackle>((int)State.Attack_Tackle);

        //�p���`
        stateMachine.AddAnyTransition<StateAttack_Punch>((int)State.Attack_Punch);

        //�@����
        stateMachine.AddAnyTransition<StateAttack_Slam>((int)State.Attack_Slam);

        //����������Ă��Ȃ��Ȃ�ҋ@��Ԃ�
        stateMachine.AddAnyTransition<StateIdle>((int)State.Idle);

        //�X�e�[�g�}�V���̊J�n�@�����X�e�[�g�͈����Ŏw��
        stateMachine.Start(stateMachine.GetOrAddState<StateIdle>());
    }

    void Update()
    {
        if (IsDead())
        {
            BehaviorAnimation.UpdateTrigger(ref animator, "Damage_Dead");
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

        CharaUpdate();
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
        actor.Transform.PositionUpdate(MyUtil.MoveType.Rigidbody);

        //velocity��������
        actor.IVelocity().InitVelocity();

    }


    //�f�B���C���Ԃ̍X�V
    void DelayTimeUpdate()
    {
        if (nowDushDelayTime > 0)
        {
            nowDushDelayTime -= Time.deltaTime;
        }

        if (nowTackleDelayTime > 0)
        {
            nowTackleDelayTime -= Time.deltaTime;
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
    [Header("�u�W�����v�X�e�[�g�v")]
    [Header("����")]
    [SerializeField] public float JumpStartSpeed;
    [Header("�����l")]
    [SerializeField] public float JumpAcceleration;

    [Space]
    [Header("�d��")]
    [SerializeField] public float JumpDecreaseValue;

    [Space]
    [Header("�u�_�b�V���X�e�[�g�v")]
    [Header("�f�B���C�b��")]
    [SerializeField] public float DushDelayTime;
    public float nowDushDelayTime;

    [Header("����")]
    [SerializeField] public float DushStartSpeed;
    [Header("�����l")]
    [SerializeField] public float DushAcceleration;
    [Header("�ړ�����")]
    [SerializeField] public float DushTime;

    [Space]
    [Header("�u�^�b�N���X�e�[�g�v")]
    [Header("�f�B���C�b��")]
    [SerializeField] public float TackleDelayTime;

    public float nowTackleDelayTime;

    [Header("����")]
    [SerializeField] public float TackleStartSpeed;
    [Header("�����l")]
    [SerializeField] public float TackleAcceleration;
    [Header("�ړ�����")]
    [SerializeField] public float TackleTime;

    [Header("�v���C���[��Renderer")]
    [SerializeField] Renderer playerRenderer;

    ChangeCamera changeCame;

    public bool isGround;

    //�A�N�^�[
    /// <summary>
    /// �X�e�[�genum
    /// </summary>
    public enum State
    {
        None,
        Idle,
        //�ړ�
        Move_Walk,
        Move_Run,
        Move_Dush,
        //�W�����v
        Jump,
        Double_Jump,
        Floating,
        //�U��
        Attack_Punch,
        Attack_Slam,
        Attack_Tackle,
        //�h��
        Guard,
    }

    public float nowJumpSpeed;
    public float jumpAcceleration;


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
            case (int)State.Jump:
                Debug.Log("�W�����v�o�^");
                break;
            case (int)State.Double_Jump:
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

