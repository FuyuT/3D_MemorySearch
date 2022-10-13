using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Enemy>;

public class StateEnemyTackle : State
{
    Vector3 accelerateionVec;
    protected override void OnEnter(State prevState)
    {

        //現在の移動を止める
        Owner.moveVec = Vector3.zero;

        //目標へのベクトルを計算
        Owner.dushVec = Owner.PlayerTransform.position - Owner.transform.position;
        Vector3.Normalize(Owner.dushVec);
        Owner.dushVec.y = 0;

        //加速値を作成
        accelerateionVec = Owner.dushVec * Owner.DushAcceleration;

        //初速を設定
        Owner.dushVec *= Owner.DushStartSpeed;

        //時間を設定
        Owner.nowDushTime = Owner.DushTime;

        //取得できるメモリを設定
        Owner.parameter.Set("所持メモリ", (int)Player.Event.Attack_Tackle);
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
        //タックルが終了したら移動へ
        if (Owner.nowDushTime < 0)
        {
            stateMachine.Dispatch((int)Enemy.Event.Move);
        }
    }

    protected override void OnExit(State nextState)
    {
        Owner.nowDushDelayTime = Owner.DushDelayTime;
    }
}
