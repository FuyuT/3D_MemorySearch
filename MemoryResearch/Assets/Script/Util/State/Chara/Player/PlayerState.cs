using System.Collections.Generic;
using UnityEngine;

namespace MyUtil
{
    /// <summary>
    /// �X�e�[�g��\���N���X
    /// </summary>
    public abstract class PlayerState : ActorState<Player>
    {
        //�X�e�[�g����J�ڂł���X�e�[�g
        protected Player.State[] dispatchStates;

        //�J�ڂł���X�e�[�g���Z�b�g����
        public void SetDispatchStates(Player.State[] dispatchStates)
        {
            this.dispatchStates = dispatchStates;
        }

        //�����̃L�[��������Ă��邩�m�F����@������Ă���Α����̔ԍ����A������Ă��Ȃ����-1��Ԃ�
        protected int IsInputEquipmentKey()
        {
            //���͂̔��f
            if (!Input.anyKey) return -1;

            for (int n = 0; n < Global.EquipmentMemoryMax; n++)
            {
                if (Input.GetKeyDown(Owner.equipmentMemories[n].GetKeyCode()))
                {
                    return n;
                }
            }

            return -1;
        }

        //���݂̏�Ԃ���J�ڂł�����̂��擾����
        protected Player.State GetDispachableState(List<Player.State> equipmentStates)
        {
            //�������J�ڂ��邱�Ƃ̂ł����Ԃ̐������[�v
            for (int n = 0; n < equipmentStates.Count; n++)
            {
                if (IsCombineDispatchStates(equipmentStates[n]))
                {
                    return equipmentStates[n];
                }
            }
            return Player.State.None;
        }

        protected bool IsCombineDispatchStates(Player.State state)
        {
            //�J�ڂł����Ԃ̐������[�v
            for (int n = 0; n < dispatchStates.Length; n++)
            {
                if (state == dispatchStates[n])
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// �����̃X�e�[�g�I������
        /// �J�ڐ悪������ΑJ�ڂ���true�A������Ȃ����false��Ԃ�
        /// </summary>
        /// <returns></returns>
        protected bool EquipmentSelectNextState()
        {
            //�����̓��͂���Ă���L�[���m�F���� �Y������L�[�����͂���Ă��Ȃ���ΏI��
            int equipmentNo = IsInputEquipmentKey();
            if (equipmentNo == -1) return false;

            //���������݂̏�Ԃ���J�ڂł�����̂ɊY�����邩�m���߂�
            Player.State nextState = GetDispachableState(Owner.equipmentMemories[equipmentNo].GetStatesList());

            //�������g�p�\���m���߂�
            if (!Owner.equipmentMemories[equipmentNo].IsPossible(nextState, Owner)) return false;

            //���͂��ꂽ��Ԃ֑J��
            stateMachine.Dispatch((int)nextState);
            Owner.currentEquipmentNo = equipmentNo;
            return true;
        }
    }
}