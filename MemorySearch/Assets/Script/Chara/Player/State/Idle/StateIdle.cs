using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;

/// <summary>
/// 待機
/// </summary>
public class StateIdle : State
{
    protected override void OnEnter(MyUtil.ActorState<Player> prevState)
    {
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Idle");
    }

    protected override void OnUpdate()
    {
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //移動
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            stateMachine.Dispatch((int)Player.State.Move_Walk);
            return;
        }

        //装備から次の状態を選択
        EquipmentSelectNextState();
    }

    protected override void OnExit(MyUtil.ActorState<Player> nextState)
    {
        //アニメーションのトリガーを解除
        Owner.animator.ResetTrigger("Idle");
    }
}
