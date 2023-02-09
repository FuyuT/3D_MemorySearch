using System.Collections.Generic;
using UnityEngine;

namespace MyUtil
{
    /// <summary>
    /// �X�e�[�g��\���N���X
    /// </summary>
    public abstract class ActorState<TOwner> where TOwner : MonoBehaviour
    {
        /// <summary>
        /// ���̃X�e�[�g���Ǘ����Ă���X�e�[�g�}�V��
        /// </summary>
        protected ActorStateMachine<TOwner> stateMachine;

        /// <summary>
        /// �X�e�[�g�}�V����ݒ�
        /// </summary>
        public void SetStateMachine(ActorStateMachine<TOwner> stateMachine) { this.stateMachine = stateMachine; }

        /// <summary>
        /// �J�ڂ̈ꗗ
        /// </summary>
        public Dictionary<int, ActorState<TOwner>> transitions = new Dictionary<int, ActorState<TOwner>>();

        /// <summary>
        /// ���̃X�e�[�g�̃I�[�i�[
        /// </summary>
        protected TOwner Owner => stateMachine.Owner;

        /// �A�N�^�[
        protected Actor<TOwner> Actor => stateMachine.Actor;

        /// <summary>
        /// �X�e�[�g�J�n
        /// </summary>
        public void Enter(ActorState<TOwner> prevState)
        {
            OnEnter(prevState);
        }
        /// <summary>
        /// �X�e�[�g���J�n�������ɌĂ΂��
        /// </summary>
        protected virtual void OnEnter(ActorState<TOwner> prevState) { }

        /// <summary>
        /// �X�e�[�g�X�V
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
        /// ���t���[���Ă΂��
        /// </summary>
        protected virtual void OnUpdate() { }

        /// <summary>
        /// ���Ԋu�ŌĂ΂��
        /// </summary>
        protected virtual void OnFiexdUpdate() { }

        /// <summary>
        /// ���̃X�e�[�g�֑J�ڂ��鏈���������֐�
        /// ���g���L�q����OnUpdate���ɍD���ȃ^�C�~���O�ŌĂяo���Ă�������
        /// </summary>
        protected virtual void SelectNextState() { }

        /// <summary>
        /// �X�e�[�g�I��
        /// </summary>
        public void Exit(ActorState<TOwner> nextState)
        {
            OnExit(nextState);
        }
        /// <summary>
        /// �X�e�[�g���I���������ɌĂ΂��
        /// </summary>
        protected virtual void OnExit(ActorState<TOwner> nextState) { }
    }
}