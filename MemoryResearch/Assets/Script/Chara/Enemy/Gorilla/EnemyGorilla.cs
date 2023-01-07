using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyGorilla>;

public class EnemyGorilla : CharaBase
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
                //�����蔻�������
                this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                this.gameObject.GetComponent<Rigidbody>().useGravity = false;
                rigidbody.velocity = Vector3.zero;
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

    public EnemyGorilla()
    {
        actor = new MyUtil.Actor<EnemyGorilla>(this);
        stateMachine = new MyUtil.ActorStateMachine<EnemyGorilla>(this, ref actor);
    }

}