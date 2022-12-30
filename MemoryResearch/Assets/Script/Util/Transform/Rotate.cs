using UnityEngine;

namespace MyUtil
{
    public class Rotate<TOwner> : IRotate where TOwner : MonoBehaviour
    {
        /*******************************
        * private
        *******************************/
        TOwner owner;

        /*******************************
        * public
        *******************************/
        public Rotate(ref TOwner owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// 角度の更新
        /// 引数の方向に回転させる
        /// </summary>
        /// <param name="vec">向く方向</param>
        public void RotateUpdateToVec(Vector3 vec, float rotateSpeed)
        {
            //yは回転の要素に加えない
            Vector3 temp = vec;
            temp.y = 0;
            var quaternion = Quaternion.LookRotation(temp);
            owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, quaternion, rotateSpeed * Time.deltaTime);
        }

    }
}