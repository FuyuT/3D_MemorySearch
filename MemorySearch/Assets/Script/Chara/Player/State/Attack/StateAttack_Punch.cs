using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;

public class StateAttack_Punch : State
{
    /*******************************
    * private
    *******************************/
    const float Move_Time_Max = 0.2f;
    const float Move_Speed = 50.0f;
    Vector3 moveVec;
    float moveTime;

    private void InitMove()
    {
        //移動時間を初期化
        moveTime = 0.0f;
        //移動方向を設定
        moveVec = BehaviorMoveToInput.GetInputVec();
        //移動方向が0なら、キャラクターの前方方向を移動方向にする
        if (moveVec == Vector3.zero)
        {
            moveVec = Owner.transform.forward;
            return;
        }
        //カメラの向きを考慮する
        BehaviorMoveToInput.ParseToCameraVec(ref moveVec);
    }
    private void UpdateMoveTime()
    {
        if (moveTime < Move_Time_Max)
        {
            moveTime += Time.deltaTime;
        }
    }
    private void Move()
    {
        if (moveTime < Move_Time_Max)
        {
            Actor.IVelocity().AddVelocity(moveVec * Move_Speed);
        }
    }

    /*******************************
    * protected
    *******************************/
    protected override void OnEnter(MyUtil.ActorState<Player> prevState)
    {
        //攻撃力設定
        Owner.SetAttackPower(1);
        //その場で停止
        Actor.Transform.IVelocity().InitVelocity();

        //SE
        SoundManager.instance.PlaySe(Owner.PunchSE, Owner.transform.position);

        InitMove();
    }
    protected override void OnUpdate()
    {
        //アニメーションを更新
        if(!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Attack_Punch_1"))
        {
            return;
        }

        UpdateMoveTime();

        SelectNextState();
    }
    protected override void OnFiexdUpdate()
    {
        //アニメーションを更新
        if (!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Attack_Punch_1"))
        {
            return;
        }

        Move();
    }
    protected override void SelectNextState()
    {
        //アニメーションが終了していたら待機へ
        if (Owner.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }

        //装備から状態を選択
        EquipmentSelectNextState();
    }

    protected override void OnExit(MyUtil.ActorState<Player> nextState)
    {
        Owner.SetAttackPower(0);

        Owner.animator.ResetTrigger("Attack_Punch_1");

        SoundManager.instance.StopSe(Owner.PunchSE);
    }
}
