using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;

/// <summary>
/// 落下（落下中の加速はrigidbodyで行う）
/// </summary>
public class StateFall : State
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
        //アニメーションの更新
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Jump_Fall");
    }

    protected override void OnUpdate()
    {
        MoveInput();
        SelectNextState();
    }

    protected override void OnFiexdUpdate()
    {
        Move();
    }

    protected override void SelectNextState()
    {
        if (Owner.isGround || Actor.IVelocity().GetState() != MyUtil.VelocityState.isDown)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }

        //装備から状態を選択
        EquipmentSelectNextState();
    }

    protected override void OnExit(MyUtil.ActorState<Player> nextState)
    {
        Owner.animator.ResetTrigger("Jump_Fall");

        Actor.Transform.IVelocity().SetVelocityY(0);
        Actor.Transform.IVelocity().InitRigidBodyVelocity();
    }
}
