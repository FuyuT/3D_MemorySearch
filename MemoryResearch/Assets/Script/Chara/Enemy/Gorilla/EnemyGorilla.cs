using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyGorilla>;

public class EnemyGorilla : EnemyBase
{
    /*******************************
    * private
    *******************************/
    //�A�N�^�[
    MyUtil.Actor<EnemyGorilla> actor;
    //�X�e�[�g�}�V��
    MyUtil.ActorStateMachine<EnemyGorilla> stateMachine;

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

        mainMemory = MemoryType.Slam;
    }
    void StateMachineInit()
    {
        stateMachine.AddAnyTransition<StateGorillaIdle>((int)State.Idle);
        stateMachine.AddAnyTransition<StateGorillaMove>((int)State.Move);
        stateMachine.AddAnyTransition<StateGorillaPunch>((int)State.Attack_Punch);

        stateMachine.Start(stateMachine.GetOrAddState<StateGorillaIdle>());
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
                SoundManager.instance.PlaySe(DownSE,transform.position);
                //�����蔻�������
                this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                this.gameObject.GetComponent<Rigidbody>().useGravity = false;
                rigidbody.velocity = Vector3.zero;
            }
            else if (BehaviorAnimation.IsPlayEnd(ref animator, "Damage_Dead"))
            {
                if (renderer.enabled)
                {
                    effectExplosion.Play();
                    SoundManager.instance.StopSe(DownSE);
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
    }

    /*******************************
    * public
    *******************************/
    [Header("���f����Renderer")]
    [SerializeField] private Renderer renderer;

    [Header("�G�t�F�N�g")]
    [SerializeField] public Effekseer.EffekseerEmitter effectExplosion;

    public enum State
    {
        Idle,
        Move,
        Attack_Punch,
    }

    [Header("�͈�")]
    [SerializeField] public MyUtil.TargetCollider searchRange;
    [SerializeField] public MyUtil.TargetCollider attackRange;

    [Header("���x")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float rotateSpeed;

    [Header("HP")]
    [SerializeField] public int hpMax;

    [Space]
    [Header("SE�֘A")]
    [SerializeField] public AudioClip WalkSE;
    [SerializeField] public AudioClip AttackSE;
    [SerializeField] public AudioClip DownSE;
    [SerializeField] public AudioClip ExplosionSE;

    public EnemyGorilla()
    {
        actor = new MyUtil.Actor<EnemyGorilla>(this);
        stateMachine = new MyUtil.ActorStateMachine<EnemyGorilla>(this, ref actor);
    }

}