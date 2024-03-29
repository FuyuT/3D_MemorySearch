using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Enemy>;

public class StateEnemyAttack : State
{
    protected override void OnEnter(State prevState)
    {
        //取得できるメモリを設定
        Owner.param.Set((int)Enemy.ParamKey.PossesionMemory, (int)Player.Event.Attack_Punch);
    }

    protected override void OnUpdate()
    {
        //攻撃
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //todo:敵攻撃が終わったら移動へ
        stateMachine.Dispatch((int)Enemy.Event.Move);
    }

    protected override void OnExit(State nextState)
    {
    }
}
