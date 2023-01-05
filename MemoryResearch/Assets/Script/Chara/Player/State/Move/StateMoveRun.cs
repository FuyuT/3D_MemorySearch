using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;


/// <summary>
/// 移動
/// </summary>
public class StateMoveRun : State
{
    bool isReady;

    protected override void OnEnter(State prevState)
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
        if (!Input.GetKey(KeyCode.LeftShift) 
            || Actor.Transform.IVelocity().GetVelocity() == Vector3.zero)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }
    }
}
