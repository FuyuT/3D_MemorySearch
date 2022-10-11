using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Enemy>;

public class StateEnemyAttack : State
{
    protected override void OnEnter(State prevState)
    {
        //取得できるメモリを設定
        Owner.parameter.Set("所持メモリ", (int)Player.Event.Attack_Punch);
    }

    protected override void OnUpdate()
    {
        //攻撃
        Debug.Log("敵:攻撃状態");
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //todo:敵攻撃が終わったら移動へ
        stateMachine.Dispatch((int)Enemy.Event.Move);
    }

    protected override void OnExit(State nextState)
    {
        Owner.parameter.Set("攻撃可能判定", false);
    }
}
