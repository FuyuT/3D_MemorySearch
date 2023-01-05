using UnityEditor;
using UnityEngine;

using State = MyUtil.ActorState<EnemyCow>;

public class StateCowTackle : State
{
    bool isTackleReady;
    Vector3 moveVec;

    protected override void OnEnter(State prevState)
    {
        //待機状態を1回再生して、チャージしている動作に見せる
        Owner.animator.SetTrigger("Idle");
        isTackleReady = false;

        Owner.SetAttackPower(3);
    }

    protected override void OnUpdate()
    {
        //タックルの準備ができていなければ、準備をして、処理終了
        if (!isTackleReady)
        {
            //進行方向を更新
            moveVec = Vector3.Normalize(Player.readPlayer.GetPos() - Actor.IPosition().GetPosition());
            moveVec.y = 0;
            //進行方向に回転
            Actor.Transform.RotateUpdateToVec(moveVec, Owner.rotateSpeed);

            TackleReady();
            return;
        }

        Tackle();

        SelectNextState();
    }

    void TackleReady()
    {
        //アニメーションが変更されていなければ処理終了
        if (!Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) return;
        //待機モーションが1週したら、タックルの準備完了
        if (Owner.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            isTackleReady = true;
            Owner.animator.SetTrigger("Move_Run");
        }
    }

    void Tackle()
    {
        //タックルが終了していれば終了
        if (Owner.tackleTime > Owner.tackleTimeMax) return;

        //アニメーションが変更されていなければ処理終了
        if (!Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Move_Run")) return;


        //目標に向かって移動
        Actor.IVelocity().AddVelocity(moveVec * Owner.tackleSpeed);

        Owner.tackleTime += Time.deltaTime;
    }

    protected override void SelectNextState()
    {
        //アニメーションが変更されていなければ処理終了
        if (!Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Move_Run")) return;

        //タックルが終了したら待機へ
        if (Owner.tackleTime > Owner.tackleTimeMax)
        {
            stateMachine.Dispatch((int)EnemyCow.State.Idle);
            return;
        }
    }

    protected override void OnExit(State prevState)
    {
        Owner.delayTackle = 0;
        Owner.tackleTime  = 0;

        Owner.InitAttackPower();
    }
}