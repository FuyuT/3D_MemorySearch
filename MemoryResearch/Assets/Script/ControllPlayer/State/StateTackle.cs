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
        Debug.Log("タックル状態へ移行");

        Owner.dushVec = Vector3.zero;

        //入力によって進行方向を変更
        if (Input.GetKey("up"))
        {
            Owner.dushVec += Owner.transform.forward;
        }
        if (Input.GetKey("down"))
        {
            Owner.dushVec -= Owner.transform.forward;
        }
        if (Input.GetKey("right"))
        {
            Owner.dushVec += Owner.transform.right;
        }
        if (Input.GetKey("left"))
        {
            Owner.dushVec -= Owner.transform.right;
        }

        //入力がない場合
        if (Owner.dushVec == Vector3.zero)
        {
            //キャラの前方ベクトルを進行方向に設定
            Owner.dushVec = Owner.transform.forward;
        }

        //加速値を作成
        accelerateionVec = Owner.dushVec * Owner.DushAcceleration;

        //初速を設定
        Owner.dushVec *= Owner.DushStartSpeed;

        //時間を設定
        Owner.nowDushTime = Owner.DushTime;
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

}
