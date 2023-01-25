using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyUtil
{
    public class Actor<TOwner> where TOwner : MonoBehaviour
    {
        /*******************************
        * private
        *******************************/
        //���̃N���X�̃I�[�i�[
        TOwner owner;

        [Header("��]���x(��b�ŕς���)")]
        [SerializeField] float rotateSpeed;

        /*******************************
        * public
        *******************************/

        public MyUtil.TransForm<TOwner> Transform { get; }

        public Actor(TOwner owenr)
        {
            this.owner = owenr;

            Transform = new MyUtil.TransForm<TOwner>(ref owenr);
            rotateSpeed = 0;
        }

        //�C���^�[�t�F�C�X�Ăяo���p
        public MyUtil.IPosition IPosition()
        {
            return Transform.IPosition();
        }
        public MyUtil.IVelocity IVelocity()
        {
            return Transform.IVelocity();
        }

        //rotate
        public float GetRotateSpeed()
        {
            return rotateSpeed;
        }

        public void SetRotateSpeed(float speed)
        {
            rotateSpeed = speed;
        }

        #region �p�x�֌W
        /// <summary>
        /// �p�x�擾
        /// </summary>
        /// <returns>AnyParameterMap</returns>
        public Vector3 GetRotate()
        {
            return owner.transform.rotation.eulerAngles;
        }

        /// <summary>
        /// �p�x�ݒ�
        /// </summary>
        /// <param name="rotate">�ݒ肷��p�x</param>
        public void SetRotate(Vector3 rotate)
        {
            owner.transform.rotation = Quaternion.Euler(rotate);
        }

        #endregion

    }
}
