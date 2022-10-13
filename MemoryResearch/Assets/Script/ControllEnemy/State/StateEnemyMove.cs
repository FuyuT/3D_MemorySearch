using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Enemy>;

public class StateEnemyMove : State
{
    protected override void OnEnter(State prevState)
    {
    }

    protected override void OnUpdate()
    {
        //��������ӂ��

        //�ڕW�Ɍ������ĒǏ]�ړ�
        Vector3 moveAdd = Owner.PlayerTransform.position - Owner.transform.position;
        Owner.moveVec += Vector3.Normalize(moveAdd) * Owner.MoveSpeed;

        Owner.moveVec.y = 0;

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //�W�����v
        if (Owner.nowJumpDelayTime < 0)
        {
            stateMachine.Dispatch((int)Enemy.Event.Jump);
        }

        //�p���`
        if ((bool)Owner.parameter.Get("�U���\����"))
        {
            stateMachine.Dispatch((int)Enemy.Event.Attack_Punch);
        }

        //�^�b�N��
        if(Owner.nowDushDelayTime < 0)
        {
            stateMachine.Dispatch((int)Enemy.Event.Attack_Tackle);
        }
    }
}
