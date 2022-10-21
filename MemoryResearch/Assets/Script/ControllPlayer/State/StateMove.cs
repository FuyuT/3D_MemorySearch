using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;


/// <summary>
/// 移動
/// </summary>
public class StateMove : State
{
    protected override void OnEnter(State prevState)
    {
    }

    protected override void OnUpdate()
    {

        //方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 inputVector = Vector3.zero;
        if (Input.GetKey("up"))
        {
            inputVector += new Vector3(0, 0, 1);
        }
        if (Input.GetKey("down"))
        {
            inputVector += new Vector3(0, 0, -1);
        }
        if (Input.GetKey("right"))
        {
            inputVector += new Vector3(1, 0, 0);
        }
        if (Input.GetKey("left"))
        {
            inputVector += new Vector3(-1, 0, 0);
        }

        //単位ベクトルを作成
        Vector3 moveForward = Camera.main.transform.forward * inputVector.z + Camera.main.transform.right * inputVector.x;
        moveForward.y = 0;

        //移動スピードを掛ける
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Owner.moveVec += moveForward * Owner.RunSpeed;
        }
        else
        {
            Owner.moveVec += moveForward * Owner.MoveSpeed;
        }

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //ダッシュ系
        if (Owner.nowDushDelayTime < 0 && Input.GetKey(KeyCode.Z))
        {
            switch (Owner.situation)
            {
                case (int)Player.Situation.Jump:
                case (int)Player.Situation.Floating:
                    //空中ダッシュ
                    stateMachine.Dispatch((int)Player.Event.Air_Dush);
                    break;
                default:
                    //タックル
                    stateMachine.Dispatch((int)Player.Event.Attack_Tackle);
                    break;
            }
        }

        //ジャンプ
        if (Owner.situation != (int)Player.Situation.Floating) //浮遊していない時
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("ジャンプ状態へ移行");

                stateMachine.Dispatch((int)Player.Event.Jump);
            }
        }

        //待機状態
        if (Owner.moveVec == Vector3.zero)
        {
            stateMachine.Dispatch((int)Player.Event.Idle);
        }
    }
}
