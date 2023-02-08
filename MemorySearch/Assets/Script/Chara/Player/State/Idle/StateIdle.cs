using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;

/// <summary>
/// �ҋ@
/// </summary>
public class StateIdle : State
{
    protected override void OnEnter(MyUtil.ActorState<Player> prevState)
    {
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Idle");
    }

    protected override void OnUpdate()
    {
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //�ړ�
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            stateMachine.Dispatch((int)Player.State.Move_Walk);
            return;
        }

        //�������玟�̏�Ԃ�I��
        EquipmentSelectNextState();
    }

    protected override void OnExit(MyUtil.ActorState<Player> nextState)
    {
        //�A�j���[�V�����̃g���K�[������
        Owner.animator.ResetTrigger("Idle");
    }
}
