using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;

/// <summary>
/// 空中ダッシュ
/// </summary>
public class StateAirDush : State
{
    Vector3 accelerateionVec;
    protected override void OnEnter(State prevState)
    {
        Owner.situation = (int)Player.Situation.Dush;

        //方向キーの入力値とカメラの向きから、移動方向を決定
        Owner.dushVec = Vector3.zero;
        if (Input.GetKey("up"))
        {
            Owner.dushVec += new Vector3(0, 0, 1);
        }
        if (Input.GetKey("down"))
        {
            Owner.dushVec += new Vector3(0, 0, -1);
        }
        if (Input.GetKey("right"))
        {
            Owner.dushVec += new Vector3(1, 0, 0);
        }
        if (Input.GetKey("left"))
        {
            Owner.dushVec += new Vector3(-1, 0, 0);
        }

        //ダッシュベクトルが0なら
        if (Owner.dushVec == Vector3.zero)
        {
            //キャラの前方ベクトル単位ベクトルを取得
            Owner.dushVec = Owner.transform.forward;
        }
        else
        {
            //単位ベクトルを作成
            Owner.dushVec = Camera.main.transform.forward * Owner.dushVec.z + Camera.main.transform.right * Owner.dushVec.x;
            Owner.dushVec.y = 0;
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


    protected override void OnExit(State nextState)
    {
        Owner.situation = (int)Player.Situation.None;
        Owner.nowDushDelayTime = Owner.DushDelayTime;
    }

}
