using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;


/// <summary>
/// 移動
/// </summary>
public class StateMoveWalk : State
{
    /*******************************
    * protected
    *******************************/
    protected override void OnEnter(State prevState)
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

        Actor.Transform.IVelocity().AddVelocity(moveAdd);
    }

    protected override void SelectNextState()
    {
        //歩きモーションでなければ終了
        if (!BehaviorAnimation.IsName(ref Owner.animator, "Move_Walk")) return;

        //ダッシュ系
        if (Owner.nowDushDelayTime < 0 && Input.GetKey(KeyCode.Z))
        {
            if(Owner.isGround)
            {
                //タックル
                stateMachine.Dispatch((int)Player.State.Attack_Tackle);
            }
            else
            {
                //空中ダッシュ
                stateMachine.Dispatch((int)Player.State.Move_Dush);
            }
        }

        //ジャンプ
        if (Owner.isGround) //浮遊していない時
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (Owner.CheckPossesionMemory((int)Player.State.Jump) || Owner.CheckPossesionMemory((int)Player.State.Double_Jump)) //メモリを持っているか確認
                {
                    stateMachine.Dispatch((int)Player.State.Jump);
                    return;
                }
            }
        }

        //パンチ
        if (Input.GetMouseButtonDown(0) && !Owner.ChapterCamera.activeSelf)
        {
            stateMachine.Dispatch((int)Player.State.Attack_Punch);
            return;
        }

        //待機状態
        if (Actor.Transform.IVelocity().GetVelocity() == Vector3.zero)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }

        //走る
        if(Input.GetKey(KeyCode.LeftShift))
        {
            stateMachine.Dispatch((int)Player.State.Move_Run);
            return;
        }

        //タックル
        if (Input.GetKey(KeyCode.K))
        {
            stateMachine.Dispatch((int)Player.State.Attack_Tackle);
            return;
        }

    }
}
