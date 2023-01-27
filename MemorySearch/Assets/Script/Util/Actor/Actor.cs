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
        //このクラスのオーナー
        TOwner owner;

        [Header("回転速度(一秒で変わる量)")]
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

        //インターフェイス呼び出し用
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

        #region 角度関係
        /// <summary>
        /// 角度取得
        /// </summary>
        /// <returns>AnyParameterMap</returns>
        public Vector3 GetRotate()
        {
            return owner.transform.rotation.eulerAngles;
        }

        /// <summary>
        /// 角度設定
        /// </summary>
        /// <param name="rotate">設定する角度</param>
        public void SetRotate(Vector3 rotate)
        {
            owner.transform.rotation = Quaternion.Euler(rotate);
        }

        #endregion

    }
}
