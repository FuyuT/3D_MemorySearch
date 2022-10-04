using System.Collections.Generic;

/// <summary>
/// ステートマシン
/// </summary>
public class StateMachine<TOwner>
{
    /// <summary>
    /// どのステートからでも特定のステートへ遷移できるようにするための仮想ステート
    /// </summary>
    public sealed class AnyState : State<TOwner> { }

    /// <summary>
    /// このステートマシンのオーナー
    /// </summary>
    public TOwner Owner { get; }
    /// <summary>
    /// 現在のステート
    /// </summary>
    public State<TOwner> CurrentState { get; private set; }

    // ステートリスト
    private LinkedList<State<TOwner>> states = new LinkedList<State<TOwner>>();

    /// <summary>
    /// ステートマシンを初期化する
    /// </summary>
    /// <param name="owner">ステートマシンのオーナー</param>
    public StateMachine(TOwner owner)
    {
        Owner = owner;
    }

    /// <summary>
    /// ステートを追加する（ジェネリック版）
    /// </summary>
    public T Add<T>() where T : State<TOwner>, new()
    {
        var state = new T();
        state.SetStateMachine(this);
        states.AddLast(state);
        return state;
    }

    /// <summary>
    /// 特定のステートを取得、なければ生成する
    /// </summary>
    public T GetOrAddState<T>() where T : State<TOwner>, new()
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
        where TFrom : State<TOwner>, new()
        where TTo : State<TOwner>, new()
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
    public void AddAnyTransition<TTo>(int eventId) where TTo : State<TOwner>, new()
    {
        AddTransition<AnyState, TTo>(eventId);
    }

    /// <summary>
    /// ステートマシンの実行を開始する（ジェネリック版）
    /// </summary>
    public void Start<TFirst>() where TFirst : State<TOwner>, new()
    {
        Start(GetOrAddState<TFirst>());
    }

    /// <summary>
    /// ステートマシンの実行を開始する
    /// </summary>
    /// <param name="firstState">起動時のステート</param>
    /// <param name="param">パラメータ</param>
    public void Start(State<TOwner> firstState)
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
        State<TOwner> to;
        if (!CurrentState.transitions.TryGetValue(eventId, out to))
        {
            if (!GetOrAddState<AnyState>().transitions.TryGetValue(eventId, out to))
            {
                // イベントに対応する遷移が見つからなかった
                return;
            }
        }
        Change(to);
    }

    /// <summary>
    /// ステートを変更する
    /// </summary>
    /// <param name="nextState">遷移先のステート</param>
    private void Change(State<TOwner> nextState)
    {
        CurrentState.Exit(nextState);
        nextState.Enter(CurrentState);
        CurrentState = nextState;
    }
}