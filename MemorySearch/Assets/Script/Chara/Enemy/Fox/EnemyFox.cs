using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyFox>;

public class EnemyFox : EnemyBase
{
    /*******************************
    * private
    *******************************/
    //�A�N�^�[
    MyUtil.Actor<EnemyFox> actor;
    //�X�e�[�g�}�V��
    MyUtil.ActorStateMachine<EnemyFox> stateMachine;

    [Header("HP��UI")]
    [SerializeField] HpUI hpUI;

    [Header("�n�ʂƂ̓����蔻��Ŏg�p���郌�C�̒���")]
    [SerializeField] float DirectionCheckHitGround;

    bool isEngagement;
    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        StateMachineInit();
        actor.Transform.Init();
        Init();
        isEngagement = false;

        mainMemory = MemoryType.Rush_Three;
    }

    private void Init()
    {
        CharaBaseInit();

        charaParam.hp = HpMax;

        mainMemory = MemoryType.Jump;

        actor.IVelocity().SetUseGravity(true);
    }

    // �X�e�[�g�}�V���̏�����
    void StateMachineInit()
    {
        stateMachine.AddAnyTransition<StateFoxIdle>((int)State.Idle);
        stateMachine.AddAnyTransition<StateFoxMove>((int)State.Move);
        stateMachine.AddAnyTransition<StateFoxAttack_1>((int)State.Attack_1);
        stateMachine.AddAnyTransition<StateFoxAttack_2>((int)State.Attack_2);
        stateMachine.AddAnyTransition<StateFoxAttack_ShotSlash>((int)State.Attack_Shot_Slash);
        stateMachine.AddAnyTransition<StateFoxAttack_Counter>((int)State.Attack_Counter);
        stateMachine.AddAnyTransition<StateFoxAttack_Tackle>((int)State.Attack_Tackle);
        stateMachine.AddAnyTransition<StateFoxDead>((int)State.Dead);

        //�X�e�[�g�}�V���̊J�n�@�����X�e�[�g�͈����Ŏw��
        stateMachine.Start(stateMachine.GetOrAddState<StateFoxIdle>());
    }

    void Update()
    {
        //�v���C���[�̎��̂��Ȃ���ΏI��
        if (Player.readPlayer == null) return;

        if (!isEngagement)
        {
            if (searchRange.InTarget)
            {
                isEngagement = true;
                hpUI.gameObject.SetActive(true);
                hpUI.SetCurrentHP(0);
                hpUI.Heal(HpMax);
            }
            return;
        }

        if (IsDead())
        {
            if (stateMachine.currentStateKey != (int)State.Dead)
            {
                stateMachine.Dispatch((int)State.Dead);
            }
            stateMachine.Update();
            return;
        }

        if (!UpdateCharaBase()) return;

        stateMachine.Update();

        PositionUpdate();

        DelayUpdate();

        CheckCollisionGround();
    }


    ///�ʒu�̍X�V
    void PositionUpdate()
    {
        //�ʒu���X�V
        actor.Transform.PositionUpdate(MyUtil.MoveType.Rigidbody);
        //velocity��������
        actor.IVelocity().InitVelocity();
    }

    void DelayUpdate()
    {
        if(tackleDelay < tackleDelayMax)
        {
            tackleDelay += Time.deltaTime;
        }

        if (projectileDelay < projectileDelayMax)
        {
            projectileDelay += Time.deltaTime;
        }

        if (closeAttackDelay < closeAttackDelayMax)
        {
            closeAttackDelay += Time.deltaTime;
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
        //�U��
        Attack_1,
        Attack_2,
        Attack_Counter,
        Attack_Shot_Slash,
        Attack_Tackle,

        Dead,
    }

    [Header("���f����Renderer")]
    [SerializeField] public Renderer renderer;

    [Header("�͈�")]
    [SerializeField] public MyUtil.TargetCollider searchRange;
    [SerializeField] public MyUtil.TargetCollider attackRange;

    [Header("���x")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float rotateSpeed;

    [Space]
    [Header("HP")]
    [SerializeField] public int HpMax;

    public bool isGround;

    [Header("�ߋ����a��")]
    [SerializeField] public int closeAttackDamage;
    [SerializeField] public float closeAttackDelay;
    [SerializeField] public float closeAttackDelayMax;

    [Header("��΂��a��")]
    [SerializeField] public ProjectileSlash projectileSlash;
    [SerializeField] public float projectileSpeed;
    [SerializeField] public int   projectileDamage;
    [SerializeField] public float projectileDelay;
    [SerializeField] public float projectileDelayMax;

    [Header("�^�b�N��")]
    [SerializeField] public int   tackleDamage;
    [SerializeField] public float tackleSpeed; 
    [SerializeField] public float tackleDelay;
    [SerializeField] public float tackleDelayMax;

    [HideInInspector]
    public CapsuleCollider capsuleCollider;
    //�R���X�g���N�^
    public EnemyFox()
    {
        actor = new MyUtil.Actor<EnemyFox>(this);
        stateMachine = new MyUtil.ActorStateMachine<EnemyFox>(this, ref actor);
    }

    /*******************************
    * override
    *******************************/
    override protected void AddDamageProcess(int damage)
    {
        //hpUI�̍Đ�
        hpUI.Damage(damage);
    }

    /*******************************
    * collision
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
