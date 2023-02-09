using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;
public class Player : CharaBase, IReadPlayer
{
    /*******************************
    * private
    *******************************/

    [Header("HP��UI")]
    [SerializeField] HpUI hpUI;

    [Header("���G����")]
    [SerializeField] float InvincibleTimeMax;
    public float nowInvincibleTime;
    [Header("�`���؂�Ԋu����")]
    [SerializeField] float DrawCancelTimeMax;
    float nowDrawCancelTime;

    [SerializeField] PhysicMaterial physicMaterial;

    //�A�N�^�[
    MyUtil.Actor<Player> actor;
    //�X�e�[�g�}�V��
    MyUtil.ActorStateMachine<Player> stateMachine;

    MyUtil.MoveType moveType;

    [Header("�n�ʂƂ̓����蔻��Ŏg�p���郌�C�̒���")]
    [SerializeField] float DirectionCheckHitGround;
    void Awake()
    {
        EquipmentInit();
        combineBattery = new CombineBattery();
        Init();
        readPlayer = this;
    }

    void EquipmentInit()
    {
        equipmentMemories = new EquipmentMemory[Global.EquipmentMemoryMax];
        //�L�[�̐ݒ�
        equipmentMemories[0] = new EquipmentMemory(KeyCode.I);
        equipmentMemories[1] = new EquipmentMemory(KeyCode.J);
        equipmentMemories[2] = new EquipmentMemory(KeyCode.K);
        equipmentMemories[3] = new EquipmentMemory(KeyCode.L);

        //�����̏����ݒ�
        IPlayerData plyaerData = DataManager.instance.IPlayerData();
        for (int n = 0; n < equipmentMemories.Length; n++)
        {
            equipmentMemories[n].InitState((Player.State)plyaerData.GetEquipmentMemory(n));
        }
    }

    private void Init()
    {
        actor.Transform.Init();

        nowInvincibleTime = 0;
        nowDrawCancelTime = 0;

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

        //���x�̏���l��ݒ�
        actor.IVelocity().SetMaxVelocityY(120);

        isPossibleDush = true;
    }

    void StateMachineInit()
    {
        //�ҋ@
        stateMachine.AddAnyTransition<StateIdle>((int)State.Idle);
        stateMachine.GetOrAddState<StateIdle>().SetDispatchStates(new State[9]
            { State.Move_Walk,    State.Jump,              State.Move_Dush, 
              State.Attack_Punch, State.Attack_Rush_Three, State.Attack_Rush_Five,
              State.Guard,        State.Attack_Slam,       State.Attack_Tackle});

        //����
        stateMachine.AddTransition<StateIdle, StateMoveWalk>((int)State.Move_Walk);
        stateMachine.GetOrAddState<StateMoveWalk>().SetDispatchStates(new State[9]
            { State.Move_Walk,    State.Jump,              State.Move_Dush,
              State.Attack_Punch, State.Attack_Rush_Three, State.Attack_Rush_Five,
              State.Guard,        State.Attack_Slam,       State.Attack_Tackle});

        //����
        stateMachine.AddAnyTransition<StateMoveRun>((int)State.Move_Run);
        stateMachine.GetOrAddState<StateMoveRun>().SetDispatchStates(new State[9]
            { State.Move_Walk,    State.Jump,              State.Move_Dush,
              State.Attack_Punch, State.Attack_Rush_Three, State.Attack_Rush_Five,
              State.Guard,        State.Attack_Slam,       State.Attack_Tackle});

        //�K�[�h
        stateMachine.AddAnyTransition<StateGuard>((int)State.Guard);
        stateMachine.GetOrAddState<StateGuard>().SetDispatchStates(new State[1]
        {
            State.None
        });

        //�_�b�V��
        stateMachine.AddAnyTransition<StateDush>((int)State.Move_Dush);
        stateMachine.GetOrAddState<StateDush>().SetDispatchStates(new State[1]
        {
            State.None
        });

        //�W�����v
        stateMachine.AddAnyTransition<StateJump>((int)State.Jump);
        stateMachine.GetOrAddState<StateJump>().SetDispatchStates(new State[2]
        { State.Double_Jump,State.Move_Dush});

        //�_�u���W�����v
        stateMachine.AddAnyTransition<StateDoubleJump>((int)State.Double_Jump);
        stateMachine.GetOrAddState<StateDoubleJump>().SetDispatchStates(new State[1]
        {
            State.Move_Dush
        });

        //�^�b�N��
        stateMachine.AddAnyTransition<StateAttackTackle>((int)State.Attack_Tackle);
        stateMachine.GetOrAddState<StateAttackTackle>().SetDispatchStates(new State[1]
        {
            State.None
        });

        //�p���`
        stateMachine.AddAnyTransition<StateAttack_Punch>((int)State.Attack_Punch);
        stateMachine.GetOrAddState<StateAttack_Punch>().SetDispatchStates(new State[1]
        {
            State.None
        });
        //���b�V���R�A��
        stateMachine.AddAnyTransition<StateAttack_Rush_Three>((int)State.Attack_Rush_Three);
        stateMachine.GetOrAddState<StateAttack_Rush_Three>().SetDispatchStates(new State[1]
        {
            State.None
        });
        //���b�V���T�A��
        stateMachine.AddAnyTransition<StateAttack_Rush_Five>((int)State.Attack_Rush_Five);
        stateMachine.GetOrAddState<StateAttack_Rush_Five>().SetDispatchStates(new State[1]
        {
            State.None
        });

        //�@����
        stateMachine.AddAnyTransition<StateAttack_Slam>((int)State.Attack_Slam);
        stateMachine.GetOrAddState<StateAttack_Slam>().SetDispatchStates(new State[1]
        {
            State.None
        });

        //����
        stateMachine.AddAnyTransition<StateFall>((int)State.Fall);
        stateMachine.GetOrAddState<StateFall>().SetDispatchStates(new State[1]
        {
            State.Move_Dush
        });

        //�X�e�[�g�}�V���̊J�n�@�����X�e�[�g�͈����Ŏw��
        stateMachine.Start(stateMachine.GetOrAddState<StateIdle>());
    }

    void Update()
    {
        batteryCountUI.SetBatteryCount(DataManager.instance.IPlayerData().GetPossesionCombineCost());

        //Delay�̍X�V
        DelayTimeUpdate();
        //���G�̍X�V
        UpdateInvincible();

        if (IsDead())
        {
            SoundManager.instance.PlaySe(DownSE,transform.position);
            BehaviorAnimation.UpdateTrigger(ref animator, "Damage_Dead");
            if(BehaviorAnimation.IsPlayEnd(ref animator, "Damage_Dead"))
            {
                SoundManager.instance.StopSe(DownSE);
            }
            return;
        }

        //TimeScale��0�ȉ��̎��͏������I��
        if (Time.timeScale <= 0) return;

        if (!UpdateCharaBase())
        {
            if(stateMachine.currentStateKey != (int)State.Idle)
            {
                stateMachine.Dispatch((int)State.Idle);
            }
            return;
        }

        //������Ԃւ̑J��
        StateDispatchFall();

        //�X�e�[�g�}�V���X�V
        stateMachine.Update();

        //�n�ʂɒ��n���Ă��邩�m�F����
        CheckCollisionLowerObject();
    }

    private void FixedUpdate()
    {
        if (IsDead())
        {
            return;
        }

        //�X�e�[�g�}�V���X�V
        stateMachine.FiexdUpdate();

        //�p�x�X�V
        RotateUpdate();

        //�ʒu�X�V
        PositionUpdate();
    }

    public bool StateDispatchFall()
    {
        if (isGround) return false;
        if (actor.IVelocity().GetState() != MyUtil.VelocityState.isDown) return false;

        if(stateMachine.currentStateKey != (int)State.Fall
            && stateMachine.currentStateKey != (int)State.Jump
            && stateMachine.currentStateKey != (int)State.Double_Jump)
        {
            stateMachine.Dispatch((int)State.Fall);
            return true;
        }

        return false;
    }

    //�p�x�X�V
    void RotateUpdate()
    {
        //��l�̎��̊p�x�ύX
        if (FPSCamera.activeSelf)
        {
            transform.eulerAngles = new Vector3(0, FPSCamera.transform.eulerAngles.y, 0);
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
        //�_�b�V��
        if (nowDushDelayTime > 0)
        {
            nowDushDelayTime -= Time.deltaTime;
        }
        //�^�b�N��
        if (nowTackleDelayTime > 0)
        {
            nowTackleDelayTime -= Time.deltaTime;
        }
    }

    void UpdateInvincible()
    {
        //���G���ԍX�V
        if (nowInvincibleTime > 0)
        {
            nowInvincibleTime -= Time.deltaTime;
        }
        else
        {
            renderer.enabled = true;
            return;
        }

        //�`��L�����Z�����ԍX�V
        if (nowDrawCancelTime > 0)
        {
            nowDrawCancelTime -= Time.deltaTime;
        }
        else
        {
            renderer.enabled = !renderer.enabled;
            nowDrawCancelTime = DrawCancelTimeMax;
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
    [SerializeField] public GameObject FPSCamera;

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
    [Space]
    [Header("�u�@�����X�e�[�g�v")]

    [Space]
    [Header("����")]
    [SerializeField] public float TackleStartSpeed;
    [Header("�����l")]
    [SerializeField] public float TackleAcceleration;
    [Header("�ړ�����")]
    [SerializeField] public float TackleTime;

    [Header("�v���C���[��Renderer")]
    [SerializeField] Renderer renderer;

    [Header("�G�t�F�N�g")]
    [SerializeField] public Effekseer.EffekseerEmitter effectWind;
    [SerializeField] public Effekseer.EffekseerEmitter effectJump;

    [Space]
    [Header("SE�֘A")]
    [SerializeField] public AudioClip JumpSE;
    [SerializeField] public AudioClip DonbleJunpSE;
    [SerializeField] public AudioClip DownSE;
    [SerializeField] public AudioClip GuardSE;
    [SerializeField] public AudioClip TackleSE;
    [SerializeField] public AudioClip PunchSE;

    [SerializeField] public BatteryCountUI batteryCountUI;

    public bool isGround;

    public bool isPossibleDush;

    public enum State
    {
        //�������̎��
        None = MemoryType.None,
        //�_�b�V��
        Move_Dush = MemoryType.Dush,
        Move_Air_Dush = MemoryType.AirDush,
        //�W�����v
        Jump = MemoryType.Jump,
        Double_Jump = MemoryType.DowbleJump,
        //�U��
        Attack_Punch = MemoryType.Punch,
        Attack_Rush_Three = MemoryType.Rush_Three,
        Attack_Rush_Five = MemoryType.Rush_Five,
        Attack_Slam = MemoryType.Slam,
        Attack_Tackle = MemoryType.Tackle,
        //�h��
        Guard = MemoryType.Guard,
        //�������̎�ވȊO
        Idle = MemoryType.Count,
        //�ړ�
        Move_Walk,
        Move_Run,
        Fall,
    }

    public float nowJumpSpeed;
    public float jumpAcceleration;

    public int[] possessionMemory { get; private set; }

    public EquipmentMemory[] equipmentMemories;
    public CombineBattery combineBattery;
    public int currentEquipmentNo;

    // getter
    public Vector3 GetPos()
    {
        return transform.position;
    }

    public int GetCurrentStateKey()
    {
        return stateMachine.currentStateKey;
    }

    public CombineBattery GetCombineBattery()
    {
        return combineBattery;
    }

    /*******************************
    * override
    *******************************/
    override protected bool IsPossibleDamage()
    {
        return nowInvincibleTime <= 0 ? true : false;
    }
    override protected void AddDamageProcess(int damage)
    {
        //hpUI�̍Đ�
        hpUI.Damage(damage);

        //���G�̍X�V
        if(!IsDead(damage))
        {
            nowInvincibleTime = InvincibleTimeMax;
            nowDrawCancelTime = DrawCancelTimeMax;
            renderer.enabled = false;
        }
    }

    /*******************************
    * �Փ˔���
    *******************************/
    private void CheckCollisionLowerObject()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * DirectionCheckHitGround, Color.red);
        if (Physics.Raycast(ray,out hit, DirectionCheckHitGround))
        {
            //�n�ʂɃ��C���������Ă��āA�v���C���[��Velocity���㏸���Ă��Ȃ���
            switch(hit.transform.tag)
            {
                case "Ground":
                    if(actor.IVelocity().GetState() != MyUtil.VelocityState.isUp)
                    {
                        isGround = true;
                        isPossibleDush = true;
                        gameObject.GetComponent<CapsuleCollider>().material = null;
                    }
                    break;
            }
        }
        //���ɂ��������Ă��Ȃ��Ȃ�
        else
        {
            gameObject.GetComponent<CapsuleCollider>().material = physicMaterial;
            isGround = false;
        }
    }
}

