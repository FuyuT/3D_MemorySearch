using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;

/// <summary>
/// タックル
/// </summary>
public class StateAttackTackle : State
{
    Vector3 tackleVelocity;
    Vector3 accelerateionVec;
    float   nowTackleTime;
    bool    isMove;


    protected override void OnEnter(MyUtil.ActorState<Player> prevState)
    {
        //アニメーションの更新
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Attack_Tackle");

        //ダッシュベクトルを作成
        tackleVelocity = BehaviorMoveToInput.GetDushVec(Owner.transform.forward);

        //加速値を作成
        accelerateionVec = tackleVelocity * Owner.TackleAcceleration;

        //初速を設定
        tackleVelocity *= Owner.TackleStartSpeed;

        //nowTackleTime
        nowTackleTime = Owner.TackleTime;

        //攻撃力設定
        Owner.SetAttackPower(8);

        isMove = false;

        //SE
        SoundManager.instance.PlaySe(Owner.TackleSE, Owner.transform.position);
    }

    protected override void OnUpdate()
    {
        //タックルの移動中で無ければ終了
        if (!Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Tackle_Move_Start")
            && !Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Tackle_Move"))
        {
            return;
        }
        else if(BehaviorAnimation.IsName(ref Owner.animator, "Tackle_Move_Start"))
        {
            if (!isMove)
            {
                Owner.effectWind.Play();
                isMove = true;
            }
        }
        //目標地点まで毎フレーム移動
        if (nowTackleTime > 0)
        {
            nowTackleTime -= Time.deltaTime;

            tackleVelocity += accelerateionVec;

            //スピード設定
            Actor.IVelocity().SetVelocity(tackleVelocity);

            Owner.effectWind.transform.position = Owner.transform.position;
        }

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //移動が終了していたら待機へ
        if (nowTackleTime < 0)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }

        //装備から状態を選択
        EquipmentSelectNextState();
    }

    protected override void OnExit(MyUtil.ActorState<Player> nextState)
    {
        Owner.nowTackleDelayTime = Owner.TackleDelayTime;

        Owner.SetAttackPower(0);

        Owner.animator.ResetTrigger("Tackle_Move");

        Owner.effectWind.Stop();

        SoundManager.instance.StopSe(Owner.TackleSE);
    }
}
