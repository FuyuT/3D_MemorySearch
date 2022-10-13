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
        Vector3 moveAdd = Owner.PlayerTransform.position - Owner.transform.position;
        Owner.moveVec += Vector3.Normalize(moveAdd) * Owner.MoveSpeed;

        Owner.moveVec.y = 0;

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //ジャンプ
        if (Owner.nowJumpDelayTime < 0)
        {
            stateMachine.Dispatch((int)Enemy.Event.Jump);
        }

        //パンチ
        if ((bool)Owner.parameter.Get("攻撃可能判定"))
        {
            stateMachine.Dispatch((int)Enemy.Event.Attack_Punch);
        }

        //タックル
        if(Owner.nowDushDelayTime < 0)
        {
            stateMachine.Dispatch((int)Enemy.Event.Attack_Tackle);
        }
    }
}
