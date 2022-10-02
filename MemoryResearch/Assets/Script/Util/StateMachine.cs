using System.Collections.Generic;

/// <summary>
/// �X�e�[�g�}�V��
/// </summary>
public class StateMachine<TOwner>
{
    /// <summary>
    /// �ǂ̃X�e�[�g����ł�����̃X�e�[�g�֑J�ڂł���悤�ɂ��邽�߂̉��z�X�e�[�g
    /// </summary>
    public sealed class AnyState : State<TOwner> { }

    /// <summary>
    /// ���̃X�e�[�g�}�V���̃I�[�i�[
    /// </summary>
    public TOwner Owner { get; }
    /// <summary>
    /// ���݂̃X�e�[�g
    /// </summary>
    public State<TOwner> CurrentState { get; private set; }

    // �X�e�[�g���X�g
    private LinkedList<State<TOwner>> states = new LinkedList<State<TOwner>>();

    /// <summary>
    /// �X�e�[�g�}�V��������������
    /// </summary>
    /// <param name="owner">�X�e�[�g�}�V���̃I�[�i�[</param>
    public StateMachine(TOwner owner)
    {
        Owner = owner;
    }

    /// <summary>
    /// �X�e�[�g��ǉ�����i�W�F�l���b�N�Łj
    /// </summary>
    public T Add<T>() where T : State<TOwner>, new()
    {
        var state = new T();
        state.SetStateMachine(this);
        states.AddLast(state);
        return state;
    }

    /// <summary>
    /// ����̃X�e�[�g���擾�A�Ȃ���ΐ�������
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
    /// �J�ڂ��`����
    /// </summary>
    /// <param name="eventId">�C�x���gID</param>
    public void AddTransition<TFrom, TTo>(int eventId)
        where TFrom : State<TOwner>, new()
        where TTo : State<TOwner>, new()
    {
        var from = GetOrAddState<TFrom>();
        if (from.transitions.ContainsKey(eventId))
        {
            // �����C�x���gID�̑J�ڂ��`��
            throw new System.ArgumentException(
                $"�X�e�[�g'{nameof(TFrom)}'�ɑ΂��ăC�x���gID'{eventId.ToString()}'�̑J�ڂ͒�`�ςł�");
        }

        var to = GetOrAddState<TTo>();
        from.transitions.Add(eventId, to);
    }

    /// <summary>
    /// �ǂ̃X�e�[�g����ł�����̃X�e�[�g�֑J�ڂł���C�x���g��ǉ�����
    /// </summary>
    /// <param name="eventId">�C�x���gID</param>
    public void AddAnyTransition<TTo>(int eventId) where TTo : State<TOwner>, new()
    {
        AddTransition<AnyState, TTo>(eventId);
    }

    /// <summary>
    /// �X�e�[�g�}�V���̎��s���J�n����i�W�F�l���b�N�Łj
    /// </summary>
    public void Start<TFirst>() where TFirst : State<TOwner>, new()
    {
        Start(GetOrAddState<TFirst>());
    }

    /// <summary>
    /// �X�e�[�g�}�V���̎��s���J�n����
    /// </summary>
    /// <param name="firstState">�N�����̃X�e�[�g</param>
    /// <param name="param">�p�����[�^</param>
    public void Start(State<TOwner> firstState)
    {
        CurrentState = firstState;
        CurrentState.Enter(null);
    }

    /// <summary>
    /// �X�e�[�g���X�V����
    /// </summary>
    public void Update()
    {
        CurrentState.Update();
    }

    /// <summary>
    /// �C�x���g�𔭍s����
    /// </summary>
    /// <param name="eventId">�C�x���gID</param>
    public void Dispatch(int eventId)
    {
        State<TOwner> to;
        if (!CurrentState.transitions.TryGetValue(eventId, out to))
        {
            if (!GetOrAddState<AnyState>().transitions.TryGetValue(eventId, out to))
            {
                // �C�x���g�ɑΉ�����J�ڂ�������Ȃ�����
                return;
            }
        }
        Change(to);
    }

    /// <summary>
    /// �X�e�[�g��ύX����
    /// </summary>
    /// <param name="nextState">�J�ڐ�̃X�e�[�g</param>
    private void Change(State<TOwner> nextState)
    {
        CurrentState.Exit(nextState);
        nextState.Enter(CurrentState);
        CurrentState = nextState;
    }
}