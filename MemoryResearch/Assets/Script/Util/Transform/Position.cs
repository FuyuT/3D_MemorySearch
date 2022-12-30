using UnityEngine;

namespace MyUtil
{
    public class Position<TOwner> : MyUtil.IPosition where TOwner : MonoBehaviour
    {
        /*******************************
        * private
        *******************************/
        TOwner owner;

        /*******************************
        * public
        *******************************/
        public Position(ref TOwner owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// 位置の更新 rigidBodyのVelocityを使用して進行方向にmoveVecを設定する
        /// isUseGravityがtrueなら重力をベクトルに加え、falseなら加えない
        /// </summary>
        public void UpdateByRigidBody(ref Rigidbody rigidbody, Vector3 velocity, bool isUseGravity)
        {
            if (isUseGravity)
            {
                //重力をベクトルに加える
                velocity += new Vector3(0, rigidbody.velocity.y, 0);
            }

            rigidbody.velocity = velocity;
        }

        /// <summary>
        /// 位置の更新 重力を使用せずに、
        /// transform.position +=moveVec * TimeDeltatimeするのみ
        /// </summary>
        public void UpdateByTransform(Vector3 moveVec)
        {
            //ベクトルを設定
            owner.transform.position += moveVec * Time.deltaTime;
        }

        //getter
        public float GetPositionX()
        {
            return owner.transform.position.x;
        }

        public float GetPositionY()
        {
            return owner.transform.position.y;
        }

        public float GetPositionZ()
        {
            return owner.transform.position.z;
        }

        public Vector3 GetPosition()
        {
            return owner.transform.position;
        }

        //setter
        public void SetPositionX(float x)
        {
            owner.transform.position = new Vector3(x, owner.transform.position.y, owner.transform.position.z);
        }
        public void SetPositionY(float y)
        {
            owner.transform.position = new Vector3(owner.transform.position.x, y, owner.transform.position.z);
        }
        public void SetPositionZ(float z)
        {
            owner.transform.position = new Vector3(owner.transform.position.x, owner.transform.position.y, z);
        }
        public void SetPosition(Vector3 pos)
        {
            owner.transform.position = pos;
        }


    }
}