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
        //当たりをふらつく

        //目標に向かって追従移動


        NextStateUpdate();
    }

    protected override void NextStateUpdate()
    {

    }
}
