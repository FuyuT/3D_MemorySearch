using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;

/// <summary>
/// 落下（落下中の加速はrigidbodyで行う）
/// </summary>
public class StateFall : State
{
    protected override void OnEnter(MyUtil.ActorState<Player> prevState)
    {
        //アニメーションの更新   todo
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Jump_Fall");
        Debug.Log("落下");
    }

    protected override void OnUpdate()
    {
        Move();
        SelectNextState();
    }

    void Move()
    {
        //方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveAdd = BehaviorMoveToInput.GetInputVec();
        BehaviorMoveToInput.ParseToCameraVec(ref moveAdd);

        Actor.Transform.IVelocity().AddVelocity(moveAdd * Owner.MoveSpeed);
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
