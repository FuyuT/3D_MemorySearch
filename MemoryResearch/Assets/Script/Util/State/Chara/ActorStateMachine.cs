using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyUtil
{
    /// <summary>
    /// �X�e�[�g�}�V��
    /// </summary>
    public class ActorStateMachine<TOwner> where TOwner : MonoBehaviour
    {
        /// <summary>
        /// �ǂ̃X�e�[�g����ł�����̃X�e�[�g�֑J�ڂł���悤�ɂ��邽�߂̉��z�X�e�[�g
        /// </summary>
        public sealed class AnyState : ActorState<TOwner> { }

        /// <summary>
        /// ���̃X�e�[�g�}�V���̃I�[�i�[
        /// </summary>
        public TOwner Owner { get; }

        /// �A�N�^�[
        public MyUtil.Actor<TOwner> Actor { get; }

        /// <summary>
        /// ���݂̃X�e�[�g
        /// </summary>
        public ActorState<TOwner> CurrentState { get; private set; }

        /// <summary>
        /// StateMachine��Dispath�i�X�e�[�g�̕ύX)���_�ŏ����l������
        /// Start�ł͓���Ȃ��̂Œ���
        /// �����l��-1
        /// </summary>
        public int currentStateKey { get; set; }

        /// <summary>
        /// �O��̃X�e�[�g�L�[���i�[
        /// �����l��-1
        /// </summary>
        public int beforeStateKey { get; set; }

        // �X�e�[�g���X�g
        private LinkedList<ActorState<TOwner>> states = new LinkedList<ActorState<TOwner>>();

        /// <summary>
        /// �X�e�[�g�}�V��������������
        /// </summary>
        /// <param name="owner">�X�e�[�g�}�V���̃I�[�i�[</param>
        public ActorStateMachine(TOwner owner, ref MyUtil.Actor<TOwner> actor)
        {
            Owner = owner;
            Actor = actor;
            currentStateKey = -1;
            beforeStateKey = -1;
        }

        /// <summary>
        /// �X�e�[�g��ǉ�����i�W�F�l���b�N�Łj
        /// </summary>
        public T Add<T>() where T : ActorState<TOwner>, new()
        {
            var state = new T();
            state.SetStateMachine(this);
            states.AddLast(state);
            return state;
        }

        /// <summary>
        /// ����̃X�e�[�g���擾�A�Ȃ���ΐ�������
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
        /// �J�ڂ��`����
        /// </summary>
        /// <param name="eventId">�C�x���gID</param>
        public void AddTransition<TFrom, TTo>(int eventId)
            where TFrom : ActorState<TOwner>, new()
            where TTo : ActorState<TOwner>, new()
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
        public void AddAnyTransition<TTo>(int eventId) where TTo : ActorState<TOwner>, new()
        {
            AddTransition<AnyState, TTo>(eventId);
        }

        /// <summary>
        /// �X�e�[�g�}�V���̎��s���J�n����i�W�F�l���b�N�Łj
        /// </summary>
        public void Start<TFirst>() where TFirst : ActorState<TOwner>, new()
        {
            Start(GetOrAddState<TFirst>());
        }

        /// <summary>
        /// �X�e�[�g�}�V���̎��s���J�n����
        /// </summary>
        /// <param name="firstState">�N�����̃X�e�[�g</param>
        /// <param name="param">�p�����[�^</param>
        public void Start(ActorState<TOwner> firstState)
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
            ActorState<TOwner> to;
            if (!CurrentState.transitions.TryGetValue(eventId, out to))
            {
                if (!GetOrAddState<AnyState>().transitions.TryGetValue(eventId, out to))
                {
                    // �C�x���g�ɑΉ�����J�ڂ�������Ȃ�����
                    return;
                }
            }

            //�O���StateKey���i�[
            beforeStateKey = currentStateKey;
            //���݂̃X�e�[�g��ύX��̃X�e�[�g�ɂ���
            currentStateKey = eventId;

            Change(to);
        }

        /// <summary>
        /// �X�e�[�g��ύX����
        /// </summary>
        /// <param name="nextState">�J�ڐ�̃X�e�[�g</param>
        private void Change(ActorState<TOwner> nextState)
        {
            CurrentState.Exit(nextState);
            nextState.Enter(CurrentState);
            CurrentState = nextState;
        }
    }
}