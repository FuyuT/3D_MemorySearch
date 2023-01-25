using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;


/// <summary>
/// 移動
/// </summary>
public class StateMoveRun : State
{
    bool isReady;

    protected override void OnEnter(MyUtil.ActorState<Player> prevState)
    {
        isReady = false;
    }

    protected override void OnUpdate()
    {
        AnimUpdate();

        Run();

        SelectNextState();
    }

    //アニメーションの更新
    void AnimUpdate()
    {
        if(isReady)
        {
            //再生できていなければ再生する
            BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Move_Run");
        }
        else
        {
            //再生できていなければ再生する
            BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Move_Run_Ready");

            //再生が終了していたら準備完了
            if (BehaviorAnimation.IsPlayEnd(ref Owner.animator, "Move_Run_Ready"))
            {
                isReady = true;
            }
        }
    }
    
    void Run()
    {

        //方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveAdd = BehaviorMoveToInput.GetInputVec();
        BehaviorMoveToInput.ParseToCameraVec(ref moveAdd);

        //移動スピードを掛ける
        moveAdd *= Owner.RunSpeed;

        Actor.Transform.IVelocity().AddVelocity(moveAdd);
    }

    protected override void SelectNextState()
    {
        //走るモーションに入っていなければ終了
        if (!BehaviorAnimation.IsName(ref Owner.animator, "Move_Run")) return;

        //待機状態
        if (!Input.GetKey(KeyCode.LeftShift) 
            || Actor.Transform.IVelocity().GetVelocity() == Vector3.zero)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }

        //装備から状態を選択
        EquipmentSelectNextState();
    }
    protected override void OnExit(MyUtil.ActorState<Player> nextState)
    {
        //アニメーションのトリガーを解除
        Owner.animator.ResetTrigger("Move_Run");
    }

}
