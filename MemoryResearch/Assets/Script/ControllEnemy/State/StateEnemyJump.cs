using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Enemy>;

public class StateEnemyJump : State
{
    Vector3 JumpVec;
    protected override void OnEnter(State prevState)
    {
        Owner.situation = (int)Enemy.Situation.Jump;
        //初速を設定
        Owner.nowJumpSpeed = Owner.JumpStartSpeed;

        //目標に向かうベクトル
        JumpVec = Vector3.zero;
        JumpVec = Owner.PlayerTransform.position - Owner.transform.position;
        JumpVec = Vector3.Normalize(JumpVec) * Owner.JumpToTargetPower;
        JumpVec.y = 0;


        Owner.GetComponent<Rigidbody>().useGravity = false;
    }

    protected override void OnUpdate()
    {
        //ジャンプ処理
        //ジャンプ速度を減速
        Owner.nowJumpSpeed -= Owner.Gravity;
        //移動ベクトルに進行方向のベクトルとジャンプのベクトルを足す
        Owner.moveVec += JumpVec + new Vector3(0, Owner.nowJumpSpeed + Owner.JumpAcceleration, 0);

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //ジャンプを終了して移動へ
        if (Owner.situation == (int)Enemy.Situation.Jump_End && Owner.moveVec.y < 0)
        {
            stateMachine.Dispatch((int)Enemy.Event.Move);
        }
    }

    protected override void OnExit(State nextState)
    {
        Owner.situation = (int)Enemy.Situation.Jump_End;

        Owner.nowJumpDelayTime = Owner.JumpDelayTime;
        Owner.moveVec.y = 0;

        Owner.GetComponent<Rigidbody>().useGravity = true;
    }
}
