using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;

/// <summary>
/// 移動
/// </summary>
public class StateMoveWalk : State
{
    /*******************************
    * private
    *******************************/
    Vector3 moveAdd;

    void MoveInput()
    {
        //方向キーの入力値とカメラの向きから、移動方向を決定
        moveAdd = BehaviorMoveToInput.GetInputVec();
        BehaviorMoveToInput.ParseToCameraVec(ref moveAdd);
    }

    void Move()
    {
        Actor.Transform.IVelocity().AddVelocity(moveAdd * Owner.MoveSpeed);
    }
    /*******************************
    * protected
    *******************************/
    protected override void OnEnter(MyUtil.ActorState<Player> prevState)
    {
    }

    protected override void OnUpdate()
    {
        //アニメーションの更新
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Move_Walk");

        MoveInput();

        SelectNextState();
    }

    protected override void OnFiexdUpdate()
    {
        Move();
    }

    protected override void SelectNextState()
    {
        //歩きモーションでなければ終了
        if (!BehaviorAnimation.IsName(ref Owner.animator, "Move_Walk")) return;

        //走る
        if (Input.GetKey(KeyCode.LeftShift))
        {
            stateMachine.Dispatch((int)Player.State.Move_Run);
            return;
        }

        //待機状態
        if (moveAdd == Vector3.zero)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }

        //装備から状態を選択
        EquipmentSelectNextState();
    }

    protected override void OnExit(MyUtil.ActorState<Player> nextState)
    {
        Owner.animator.ResetTrigger("Move_Walk");
    }
}
