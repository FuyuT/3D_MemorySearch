using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Enemy>;

public class StateEnemyAttack : State
{
    protected override void OnEnter(State prevState)
    {
        //�擾�ł��郁������ݒ�
        Owner.param.Set((int)Enemy.ParamKey.PossesionMemory, (int)Player.Event.Attack_Punch);
    }

    protected override void OnUpdate()
    {
        //�U��
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //todo:�G�U�����I�������ړ���
        stateMachine.Dispatch((int)Enemy.Event.Move);
    }

    protected override void OnExit(State nextState)
    {
    }
}
