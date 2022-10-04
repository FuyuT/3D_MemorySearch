using System.Collections.Generic;

/// <summary>
/// �X�e�[�g��\���N���X
/// </summary>
public abstract class State<TOwner>
{
    /// <summary>
    /// ���̃X�e�[�g���Ǘ����Ă���X�e�[�g�}�V��
    /// </summary>
    protected StateMachine<TOwner> stateMachine;

    /// <summary>
    /// �X�e�[�g�}�V����ݒ�
    /// </summary>
    public void SetStateMachine(StateMachine<TOwner> stateMachine) { this.stateMachine = stateMachine; }

    /// <summary>
    /// �J�ڂ̈ꗗ
    /// </summary>
    public Dictionary<int, State<TOwner>> transitions = new Dictionary<int, State<TOwner>>();

    /// <summary>
    /// ���̃X�e�[�g�̃I�[�i�[
    /// </summary>
    protected TOwner Owner => stateMachine.Owner;

    /// <summary>
    /// �X�e�[�g�J�n
    /// </summary>
    public void Enter(State<TOwner> prevState)
    {
        OnEnter(prevState);
    }
    /// <summary>
    /// �X�e�[�g���J�n�������ɌĂ΂��
    /// </summary>
    protected virtual void OnEnter(State<TOwner> prevState) { }

    /// <summary>
    /// �X�e�[�g�X�V
    /// </summary>
    public void Update()
    {
        OnUpdate();
    }
    /// <summary>
    /// ���t���[���Ă΂��
    /// </summary>
    protected virtual void OnUpdate() { }

    /// <summary>
    /// ���̃X�e�[�g�֑J�ڂ��鏈���������֐�
    /// ���g���L�q����OnUpdate���ɍD���ȃ^�C�~���O�ŌĂяo���Ă�������
    /// </summary>
    protected virtual void NextStateUpdate() { }

    /// <summary>
    /// �X�e�[�g�I��
    /// </summary>
    public void Exit(State<TOwner> nextState)
    {
        OnExit(nextState);
    }
    /// <summary>
    /// �X�e�[�g���I���������ɌĂ΂��
    /// </summary>
    protected virtual void OnExit(State<TOwner> nextState) { }
}
