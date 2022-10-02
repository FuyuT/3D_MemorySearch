using System.Collections.Generic;

/// <summary>
/// ステートを表すクラス
/// </summary>
public abstract class State<TOwner>
{
    /// <summary>
    /// このステートを管理しているステートマシン
    /// </summary>
    protected StateMachine<TOwner> stateMachine;

    /// <summary>
    /// ステートマシンを設定
    /// </summary>
    public void SetStateMachine(StateMachine<TOwner> stateMachine) { this.stateMachine = stateMachine; }

    /// <summary>
    /// 遷移の一覧
    /// </summary>
    public Dictionary<int, State<TOwner>> transitions = new Dictionary<int, State<TOwner>>();

    /// <summary>
    /// このステートのオーナー
    /// </summary>
    protected TOwner Owner => stateMachine.Owner;

    /// <summary>
    /// ステート開始
    /// </summary>
    public void Enter(State<TOwner> prevState)
    {
        OnEnter(prevState);
    }
    /// <summary>
    /// ステートを開始した時に呼ばれる
    /// </summary>
    protected virtual void OnEnter(State<TOwner> prevState) { }

    /// <summary>
    /// ステート更新
    /// </summary>
    public void Update()
    {
        OnUpdate();
    }
    /// <summary>
    /// 毎フレーム呼ばれる
    /// </summary>
    protected virtual void OnUpdate() { }

    /// <summary>
    /// 次のステートへ遷移する処理を書く関数
    /// 中身を記述してOnUpdate中に好きなタイミングで呼び出してください
    /// </summary>
    protected virtual void NextStateUpdate() { }

    /// <summary>
    /// ステート終了
    /// </summary>
    public void Exit(State<TOwner> nextState)
    {
        OnExit(nextState);
    }
    /// <summary>
    /// ステートを終了した時に呼ばれる
    /// </summary>
    protected virtual void OnExit(State<TOwner> nextState) { }
}
