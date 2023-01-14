using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyCow>;

public class EnemyCow : EnemyBase
{
    /*******************************
    * private
    *******************************/
    //�A�N�^�[
    MyUtil.Actor<EnemyCow> actor;
    //�X�e�[�g�}�V��
    MyUtil.ActorStateMachine<EnemyCow> stateMachine;

    private void Awake()
    {
        Init();
        StateMachineInit();
        actor.Transform.Init();
    }
    private void Init()
    {
        CharaBaseInit();
        charaParam.hp = hpMax;

        mainMemory = MemoryType.Dush;
    }
    void StateMachineInit()
    {
        stateMachine.AddAnyTransition<StateCowIdle>((int)State.Idle);
        stateMachine.AddAnyTransition<StateCowMove>((int)State.Move);
        stateMachine.AddAnyTransition<StateCowTackle>((int)State.Attack_Tackle);

        stateMachine.Start(stateMachine.GetOrAddState<StateCowIdle>());
    }

    private void Update()
    {
        //�v���C���[�̎��̂��Ȃ���ΏI��
        if (Player.readPlayer == null) return;

        if (IsDead())
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Damage_Dead"))
            {
                BehaviorAnimation.UpdateTrigger(ref animator, "Damage_Dead");
                //�o���o��SE
                SoundManager.instance.PlaySe(DownSE, transform.position);

                //�����蔻�������
                this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                this.gameObject.GetComponent<Rigidbody>().useGravity = false;
                rigidbody.velocity = Vector3.zero;
            }
            else if(BehaviorAnimation.IsPlayEnd(ref animator, "Damage_Dead"))
            {
                if(renderer.enabled)
                {
                    effectExplosion.Play();
                    //����SE
                    SoundManager.instance.PlaySe(ExplosionSE, transform.position);
                    renderer.enabled = false;
                }
            }
            return;
        }

        stateMachine.Update();

        UpdateRotate();

        UpdatePosition();

        UpdateDelay();

        CharaUpdate();
    }
    //�p�x�X�V
    void UpdateRotate()
    {
        Vector3 temp = actor.IVelocity().GetVelocity();
        temp.y = 0;
        if (temp != Vector3.zero)
        {
            actor.Transform.RotateUpdateToVec(temp, rotateSpeed);
        }
    }
    //�ʒu�X�V
    void UpdatePosition()
    {
        //�ʒu���X�V
        actor.Transform.PositionUpdate(MyUtil.MoveType.Rigidbody);

        //velocity��������
        actor.IVelocity().InitVelocity();
    }
    void UpdateDelay()
    {
        //�T�m�͈͂Ƀ^�[�Q�b�g�������Ă��Ȃ���ΏI��
        if (!searchRange.InTarget) return;
        if (delayTackle < delayTackleMax)
        {
            delayTackle += Time.deltaTime;
        }
    }
    

    /*******************************
    * public
    *******************************/
    public enum State
    {
        Idle,
        Move,
        Attack_Tackle,
    }

    [Header("�͈�")]
    [SerializeField] public MyUtil.TargetCollider searchRange;
    [SerializeField] public MyUtil.TargetCollider attackRange;

    [Header("���x")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float rotateSpeed;

    [Header("HP")]
    [SerializeField] public int hpMax;

    [Header("�^�b�N��")]
    [SerializeField]  public float delayTackleMax;
    [HideInInspector] public float delayTackle;
    [SerializeField]  public float tackleTimeMax;
    [HideInInspector] public float tackleTime;
    [SerializeField]  public float tackleSpeed;

    [Header("���f����Renderer")]
    [SerializeField] private Renderer renderer;

    [Header("�G�t�F�N�g")]
    [SerializeField] public Effekseer.EffekseerEmitter effectTackle;
    [SerializeField] public Effekseer.EffekseerEmitter effectExplosion;

    [Space]
    [Header("SE�֘A")]
    [SerializeField] public AudioClip WalkSE;
    [SerializeField] public AudioClip AttackSE;
    [SerializeField] public AudioClip DownSE;
    [SerializeField] public AudioClip ExplosionSE;

    public EnemyCow()
    {
        actor = new MyUtil.Actor<EnemyCow>(this);
        stateMachine = new MyUtil.ActorStateMachine<EnemyCow>(this, ref actor);
    }

}