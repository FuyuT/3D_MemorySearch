using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Enemy>;

public class StateEnemyDefense : State
{
    protected override void OnEnter(State prevState)
    {
        //取得できるメモリを設定
        Owner.param.Set((int)Enemy.ParamKey.PossesionMemory, (int)Player.Event.Attack_Tackle);
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
