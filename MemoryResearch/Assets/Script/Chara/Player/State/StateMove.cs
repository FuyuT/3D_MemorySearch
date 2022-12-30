using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;


/// <summary>
/// �ړ�
/// </summary>
public class StateMove : State
{
    /*******************************
    * protected
    *******************************/
    protected override void OnEnter(State prevState)
    {
    }

    protected override void OnUpdate()
    {
        Vector3 moveAdd = BehaviorMoveToInput.GetInputVec();

        //�J�����̕����ƈړ��x�N�g�������킹��
        moveAdd = Camera.main.transform.forward * moveAdd.z + Camera.main.transform.right * moveAdd.x;
        moveAdd.y = 0;

        //�ړ��X�s�[�h���|����
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveAdd *= Owner.RunSpeed;
        }
        else
        {
            moveAdd *= Owner.MoveSpeed;
        }

        Actor.Transform.IVelocity().AddVelocity(moveAdd);

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //�_�b�V���n
        if (Owner.nowDushDelayTime < 0 && Input.GetKey(KeyCode.Z))
        {
            if(Owner.isGround)
            {
                //�^�b�N��
                stateMachine.Dispatch((int)Player.Event.Attack_Tackle);
            }
            else
            {
                //�󒆃_�b�V��
                stateMachine.Dispatch((int)Player.Event.Air_Dush);
            }
        }

        //�W�����v
        if (Owner.isGround) //���V���Ă��Ȃ���
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (Owner.CheckPossesionMemory((int)Player.Event.Jump) || Owner.CheckPossesionMemory((int)Player.Event.Double_Jump)) //�������������Ă��邩�m�F
                {
                    stateMachine.Dispatch((int)Player.Event.Jump);
                    return;
                }
            }
        }

        //�p���`
        if (Input.GetMouseButtonDown(0) && !Owner.ChapterCamera.activeSelf)
        {
            stateMachine.Dispatch((int)Player.Event.Attack_Punch);
            return;
        }

        //�ҋ@���
        if (Actor.Transform.IVelocity().GetVelocity() == Vector3.zero)
        {
            stateMachine.Dispatch((int)Player.Event.Idle);
            return;
        }
    }
}
