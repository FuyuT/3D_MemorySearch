using System.Collections.Generic;
using UnityEngine;

namespace MyUtil
{
    /// <summary>
    /// ステートを表すクラス
    /// </summary>
    public abstract class ActorState<TOwner> where TOwner : MonoBehaviour
    {
        /// <summary>
        /// このステートを管理しているステートマシン
        /// </summary>
        protected ActorStateMachine<TOwner> stateMachine;

        /// <summary>
        /// ステートマシンを設定
        /// </summary>
        public void SetStateMachine(ActorStateMachine<TOwner> stateMachine) { this.stateMachine = stateMachine; }

        /// <summary>
        /// 遷移の一覧
        /// </summary>
        public Dictionary<int, ActorState<TOwner>> transitions = new Dictionary<int, ActorState<TOwner>>();

        /// <summary>
        /// このステートのオーナー
        /// </summary>
        protected TOwner Owner => stateMachine.Owner;

        /// アクター
        protected Actor<TOwner> Actor => stateMachine.Actor;

        /// <summary>
        /// ステート開始
        /// </summary>
        public void Enter(ActorState<TOwner> prevState)
        {
            OnEnter(prevState);
        }
        /// <summary>
        /// ステートを開始した時に呼ばれる
        /// </summary>
        protected virtual void OnEnter(ActorState<TOwner> prevState) { }

        /// <summary>
        /// ステート更新
        /// </summary>
        public void Update()
        {
            OnUpdate();
        }
        public void FiexdUpdate()
        {
            OnFiexdUpdate();
        }

        /// <summary>
        /// 毎フレーム呼ばれる
        /// </summary>
        protected virtual void OnUpdate() { }

        /// <summary>
        /// 一定間隔で呼ばれる
        /// </summary>
        protected virtual void OnFiexdUpdate() { }

        /// <summary>
        /// 次のステートへ遷移する処理を書く関数
        /// 中身を記述してOnUpdate中に好きなタイミングで呼び出してください
        /// </summary>
        protected virtual void SelectNextState() { }

        /// <summary>
        /// ステート終了
        /// </summary>
        public void Exit(ActorState<TOwner> nextState)
        {
            OnExit(nextState);
        }
        /// <summary>
        /// ステートを終了した時に呼ばれる
        /// </summary>
        protected virtual void OnExit(ActorState<TOwner> nextState) { }
    }
}