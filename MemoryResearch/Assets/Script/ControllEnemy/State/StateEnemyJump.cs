using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Enemy>;

public class StateEnemyJump : State
{
    protected override void OnEnter(State prevState)
    {
        Owner.isJump = true;
        Debug.Log("敵：ジャンプ状態へ移行");

        Owner.isFloating = true;
        //初速を設定
        Owner.nowJumpSpeed = Owner.JumpStartSpeed;
    }
    protected override void OnUpdate()
    {
        //目標に向かうベクトル
        Vector3 moveAdd = Owner.PlayerTransform.position - Owner.transform.position;
        Vector3.Normalize(moveAdd);
        moveAdd.y = 0;

        //ジャンプ処理
        //ジャンプ速度を減速
        Owner.nowJumpSpeed -= Owner.Gravity;
        //加速値を足していく
        moveAdd.y += Owner.nowJumpSpeed + Owner.JumpAcceleration;

        Owner.moveVec += moveAdd;

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        Debug.Log(Owner.isFloating);
        if (!Owner.isFloating)
        {
            Debug.Log("敵:移動へ");
            stateMachine.Dispatch((int)Enemy.Event.Move);
        }
    }

    protected override void OnExit(State nextState)
    {
        Owner.nowJumpDelayTime = Owner.JumpDelayTime;
        Owner.moveVec.y = 0;
    }
}
