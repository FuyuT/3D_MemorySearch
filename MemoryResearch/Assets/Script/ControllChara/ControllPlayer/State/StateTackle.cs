using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;

/// <summary>
/// タックル
/// </summary>
public class StateTackle : State
{
    Vector3 accelerateionVec;
    protected override void OnEnter(State prevState)
    {
        Owner.situation = (int)Player.Situation.Dush;

        //方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 inputVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            inputVector += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector += new Vector3(-1, 0, 0);
        }

        //入力がない場合
        if (inputVector == Vector3.zero)
        {
            //キャラの前方ベクトルを進行方向に設定
            inputVector = Owner.transform.forward;
        }

        Vector3 moveForward = Camera.main.transform.forward * inputVector.z + Camera.main.transform.right * inputVector.x;
        moveForward.y = 0;
        Owner.dushVec = moveForward;

        //加速値を作成
        accelerateionVec = moveForward * Owner.DushAcceleration;

        //初速を設定
        Owner.dushVec *= Owner.DushStartSpeed;

        //時間を設定
        Owner.nowDushTime = Owner.DushTime;

        //攻撃力設定
        Owner.param.Set((int)Player.ParamKey.AttackPower, 10);
    }

    protected override void OnUpdate()
    {
        //目標地点まで毎フレーム移動
        if (Owner.nowDushTime > 0)
        {
            Owner.nowDushTime -= Time.deltaTime;

            Owner.dushVec += accelerateionVec;
            Owner.moveVec += Owner.dushVec;
        }

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //移動が終了していたら待機へ
        if (Owner.nowDushTime < 0)
        {
            stateMachine.Dispatch((int)Player.Event.Idle);
        }
    }
    protected override void OnExit(State nextState)
    {
        Owner.situation = (int)Player.Situation.None;
        Owner.nowDushDelayTime = Owner.DushDelayTime;

        //攻撃力設定
        Owner.param.Set((int)Player.ParamKey.AttackPower, 0);


        Owner.Attack_Tackle.SetAttackPossible();
    }
}
