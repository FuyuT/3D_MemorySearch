using MyUtil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;


/// <summary>
/// ガード
/// </summary>
public class StateGuard : State
{
    bool isReady;

    protected override void OnEnter(State prevState)
    {
        isReady = true;
        Owner.SetDefencePower(5);
    }

    protected override void OnUpdate()
    {
        AnimUpdate();

        SelectNextState();
    }

    //アニメーションの更新
    void AnimUpdate()
    {
        if(isReady)
        {
            //再生できていなければ再生する
            BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Guard_Guarding");
        }
        else
        {
            //再生できていなければ再生する
            BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Guard_Ready");

            //再生が終了していたら準備完了
            if (BehaviorAnimation.IsPlayEnd(ref Owner.animator, "Guard_Ready"))
            {
                isReady = true;
            }
        }
    }
    
    protected override void SelectNextState()
    {
        //ガードモーションに入っていなければ終了
        if (!BehaviorAnimation.IsName(ref Owner.animator, "Guard_Guarding")) return;

        //待機
        if (!Input.GetKey(KeyCode.H))
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }
    }

    protected override void OnExit(ActorState<Player> nextState)
    {
        Owner.InitDefencePower();
    }
}
