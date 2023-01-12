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
    * protected
    *******************************/
    protected override void OnEnter(MyUtil.ActorState<Player> prevState)
    {
    }

    protected override void OnUpdate()
    {
        //アニメーションの更新
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Move_Walk");

        Move();

        SelectNextState();
    }

    void Move()
    {       
        //方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveAdd = BehaviorMoveToInput.GetInputVec();
        BehaviorMoveToInput.ParseToCameraVec(ref moveAdd);

        //移動スピードを掛ける
        moveAdd *= Owner.MoveSpeed;

        //歩くSEを流す
        SoundManager.instance.PlaySe(Owner.WalkSE);

        Actor.Transform.IVelocity().AddVelocity(moveAdd);
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
        if (Actor.Transform.IVelocity().GetVelocity() == Vector3.zero)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }

        //装備から状態を選択
        EquipmentSelectNextState();
    }

    protected override void OnExit(MyUtil.ActorState<Player> nextState)
    {
        Owner.animator.ResetTrigger("Idle");
    }
}
