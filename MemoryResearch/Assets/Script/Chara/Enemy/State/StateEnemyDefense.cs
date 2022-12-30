using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyCrab>;

public class StateEnemyDefense : State
{
    protected override void OnEnter(State prevState)
    {
    }

    protected override void OnUpdate()
    {
        //ガード
    }

    protected override void SelectNextState()
    {
        stateMachine.Dispatch((int)Enemy.Event.Move);
    }
}
