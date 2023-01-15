using MyUtil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;

/// <summary>
/// ガード
/// </summary>
public class StateGuard : State
{
    bool isReady;

    protected override void OnEnter(MyUtil.ActorState<Player> prevState)
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
            SoundManager.instance.PlaySe(Owner.GuardSE, Owner.transform.position);
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
        if (!Input.GetKey(Owner.equipmentMemories[Owner.currentEquipmentNo].GetKeyCode()))
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }

        //装備から状態を選択
        EquipmentSelectNextState();
    }

    protected override void OnExit(MyUtil.ActorState<Player> nextState)
    {
        Owner.InitDefencePower();

        Owner.animator.ResetTrigger("Guard_Guarding");
        SoundManager.instance.StopSe(Owner.GuardSE);
    }
}
