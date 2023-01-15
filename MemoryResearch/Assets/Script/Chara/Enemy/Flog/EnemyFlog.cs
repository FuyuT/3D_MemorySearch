using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyFlog>;

public class EnemyFlog : EnemyBase
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

        mainMemory = MemoryType.Jump;

        actor.IVelocity().SetUseGravity(true);
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
        //�v���C���[�̎��̂��Ȃ���ΏI��
        if (Player.readPlayer == null)
        {
            return;
        }

        if (IsDead())
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Damage_Dead"))
            {
                BehaviorAnimation.UpdateTrigger(ref animator, "Damage_Dead");
                SoundManager.instance.PlaySe(DownSE, transform.position);
                //�����蔻�������
                this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                this.gameObject.GetComponent<Rigidbody>().useGravity = false;
                rigidbody.velocity = Vector3.zero;
            }
            else if (BehaviorAnimation.IsPlayEnd(ref animator, "Damage_Dead"))
            {
                SoundManager.instance.StopSe(DownSE);
               // SoundManager.instance.PlaySe(ExplosionSE, transform.position);

                renderer.enabled = false;
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

        CharaUpdate();
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

    [Header("�͈�")]
    [SerializeField] public MyUtil.TargetCollider searchRange;
    [SerializeField] public MyUtil.TargetCollider attackRange;

    [Header("���x")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float rotateSpeed;

    [Space]
    [Header("HP")]
    [SerializeField] public int HpMax;

    [Header("�u�ˌ��X�e�[�g�v")]
    [SerializeField] public float delayShotTimeMax;
    public float delayShotTime;

    [SerializeField] public float ShotSpeed;
    [SerializeField] public int ShotDamage;
    [SerializeField] public GameObject bullet;
    [SerializeField] public float ShotInterval;

    [Header("�u�W�����v�X�e�[�g�v")]
    [SerializeField] public float delayJumpTimeMax;
    [Header("�i�ޗ�")]
    [SerializeField] public float JumpToTargetPower;

    [Header("����")]
    [SerializeField] public float JumpStartSpeed;

    [Header("������")]
    [SerializeField] public float JumpDecreaseValue;

    [Header("���f����Renderer")]
    [SerializeField] private Renderer renderer;

    [Header("�G�t�F�N�g")]
    [SerializeField] public Effekseer.EffekseerEmitter effect;

    [Space]
    [Header("SE�֘A")]
    [SerializeField] public AudioClip JumpSE;
    [SerializeField] public AudioClip AttackSE;
    [SerializeField] public AudioClip ShotSE;
    [SerializeField] public AudioClip DownSE;
    [SerializeField] public AudioClip ExplosionSE;

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
