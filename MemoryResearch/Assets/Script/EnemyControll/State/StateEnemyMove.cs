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


        NextStateUpdate();
    }

    protected override void NextStateUpdate()
    {

    }
}
