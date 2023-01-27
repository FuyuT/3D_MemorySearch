using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyUtil
{
    /// <summary>
    /// ステートマシン
    /// </summary>
    public class ActorStateMachine<TOwner> where TOwner : MonoBehaviour
    {
        /// <summary>
        /// どのステートからでも特定のステートへ遷移できるようにするための仮想ステート
        /// </summary>
        public sealed class AnyState : ActorState<TOwner> { }

        /// <summary>
        /// このステートマシンのオーナー
        /// </summary>
        public TOwner Owner { get; }

        /// アクター
        public MyUtil.Actor<TOwner> Actor { get; }

        /// <summary>
        /// 現在のステート
        /// </summary>
        public ActorState<TOwner> CurrentState { get; private set; }

        /// <summary>
        /// StateMachineのDispath（ステートの変更)時点で初期値が入る
        /// Startでは入らないので注意
        /// 初期値は-1
        /// </summary>
        public int currentStateKey { get; set; }

        /// <summary>
        /// 前回のステートキーを格納
        /// 初期値は-1
        /// </summary>
        public int beforeStateKey { get; set; }

        // ステートリスト
        private LinkedList<ActorState<TOwner>> states = new LinkedList<ActorState<TOwner>>();

        /// <summary>
        /// ステートマシンを初期化する
        /// </summary>
        /// <param name="owner">ステートマシンのオーナー</param>
        public ActorStateMachine(TOwner owner, ref MyUtil.Actor<TOwner> actor)
        {
            Owner = owner;
            Actor = actor;
            currentStateKey = -1;
            beforeStateKey = -1;
        }

        /// <summary>
        /// ステートを追加する（ジェネリック版）
        /// </summary>
        public T Add<T>() where T : ActorState<TOwner>, new()
        {
            var state = new T();
            state.SetStateMachine(this);
            states.AddLast(state);
            return state;
        }

        /// <summary>
        /// 特定のステートを取得、なければ生成する
        /// </summary>
        public T GetOrAddState<T>() where T : ActorState<TOwner>, new()
        {
            foreach (var state in states)
            {
                if (state is T result)
                {
                    return result;
                }
            }
            return Add<T>();
        }

        /// <summary>
        /// 遷移を定義する
        /// </summary>
        /// <param name="eventId">イベントID</param>
        public void AddTransition<TFrom, TTo>(int eventId)
            where TFrom : ActorState<TOwner>, new()
            where TTo : ActorState<TOwner>, new()
        {
            var from = GetOrAddState<TFrom>();
            if (from.transitions.ContainsKey(eventId))
            {
                // 同じイベントIDの遷移を定義済
                throw new System.ArgumentException(
                    $"ステート'{nameof(TFrom)}'に対してイベントID'{eventId.ToString()}'の遷移は定義済です");
            }

            var to = GetOrAddState<TTo>();
            from.transitions.Add(eventId, to);
        }

        /// <summary>
        /// どのステートからでも特定のステートへ遷移できるイベントを追加する
        /// </summary>
        /// <param name="eventId">イベントID</param>
        public void AddAnyTransition<TTo>(int eventId) where TTo : ActorState<TOwner>, new()
        {
            AddTransition<AnyState, TTo>(eventId);
        }

        /// <summary>
        /// ステートマシンの実行を開始する（ジェネリック版）
        /// </summary>
        public void Start<TFirst>() where TFirst : ActorState<TOwner>, new()
        {
            Start(GetOrAddState<TFirst>());
        }

        /// <summary>
        /// ステートマシンの実行を開始する
        /// </summary>
        /// <param name="firstState">起動時のステート</param>
        /// <param name="param">パラメータ</param>
        public void Start(ActorState<TOwner> firstState)
        {
            CurrentState = firstState;
            CurrentState.Enter(null);
        }

        /// <summary>
        /// ステートを更新する
        /// </summary>
        public void Update()
        {
            CurrentState.Update();
        }

        /// <summary>
        /// イベントを発行する
        /// </summary>
        /// <param name="eventId">イベントID</param>
        public void Dispatch(int eventId)
        {
            ActorState<TOwner> to;
            if (!CurrentState.transitions.TryGetValue(eventId, out to))
            {
                if (!GetOrAddState<AnyState>().transitions.TryGetValue(eventId, out to))
                {
                    // イベントに対応する遷移が見つからなかった
                    return;
                }
            }

            //前回のStateKeyを格納
            beforeStateKey = currentStateKey;
            //現在のステートを変更先のステートにする
            currentStateKey = eventId;

            Change(to);
        }

        /// <summary>
        /// ステートを変更する
        /// </summary>
        /// <param name="nextState">遷移先のステート</param>
        private void Change(ActorState<TOwner> nextState)
        {
            CurrentState.Exit(nextState);
            nextState.Enter(CurrentState);
            CurrentState = nextState;
        }
    }
}