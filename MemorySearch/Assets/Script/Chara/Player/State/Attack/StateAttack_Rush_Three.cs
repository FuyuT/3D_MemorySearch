using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;

public class StateAttack_Rush_Three : State
{
    int currentPunchNo;
    bool isChangeNextPunch;
    const int Punch_No_Max = 3;
    const float Move_Time_Max = 0.2f;
    const float Move_Speed = 50.0f;
    Vector3 moveVec;
    float moveTime;

    protected override void OnEnter(MyUtil.ActorState<Player> prevState)
    {
        //攻撃力設定
        Owner.SetAttackPower(1);
        //その場で停止
        Actor.Transform.IVelocity().InitVelocity();

        //SE
        SoundManager.instance.PlaySe(Owner.PunchSE, Owner.transform.position);

        currentPunchNo = 1;
        isChangeNextPunch = false;
        Owner.animator.SetTrigger("Attack_Punch_1");

        InitMove();
    }

    protected override void OnUpdate()
    {
        Move();

        NextPunch();

        SelectNextState();
    }

    private void Move()
    {
        if (moveTime < Move_Time_Max)
        {
            moveTime += Time.deltaTime;
            Actor.IVelocity().AddVelocity(moveVec * Move_Speed);
        }
    }
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

    private void NextPunch()
    {
        if (BehaviorAnimation.IsName(ref Owner.animator, "Attack_Punch_" + currentPunchNo)
            && currentPunchNo != Punch_No_Max)
        {
            if (Input.GetKeyDown(Owner.equipmentMemories[Owner.currentEquipmentNo].GetKeyCode()))
            {
                currentPunchNo++;
                isChangeNextPunch = true;
            }
        }

        if (!isChangeNextPunch) return;
        if (BehaviorAnimation.IsPlayEnd(ref Owner.animator, "Attack_Punch_" + (currentPunchNo - 1)))
        {
            Owner.animator.SetTrigger("Attack_Punch_" + currentPunchNo);
            isChangeNextPunch = false;

            InitMove();
        }
    }

    protected override void SelectNextState()
    {
        //パンチが続くなら終了
        if (!BehaviorAnimation.IsName(ref Owner.animator, "Attack_Punch_" + currentPunchNo)) return;

        //アニメーションが終了していたら待機へ
        if (Owner.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }
    }

    protected override void OnExit(MyUtil.ActorState<Player> nextState)
    {
        Owner.SetAttackPower(0);

        Owner.animator.ResetTrigger("Attack_Punch_1");

        SoundManager.instance.StopSe(Owner.PunchSE);
    }
}
