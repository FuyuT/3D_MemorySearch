using UnityEngine;

namespace MyUtil
{
    public enum MoveType
    {
        Rigidbody,
        Transform,
    }

    public class TransForm<TOwner> where TOwner : MonoBehaviour
    {
        /*******************************
        * private
        *******************************/
        //このクラスのオーナー
        TOwner owner;
        Rigidbody rigidbody;

        //rigidbodyを設定
        void SetRigidBody()
        {
            try
            {
                rigidbody = owner.gameObject.GetComponent<Rigidbody>();
            }
            catch
            {
                Debug.LogError("オブジェクト" + owner.gameObject
                    + "にrigidbodyがアタッチされていません");
            }
        }

        //座標
        private MyUtil.Position<TOwner> position;
        //速度
        private MyUtil.Velocity velocity;
        private MyUtil.Rotate<TOwner> rotate;

        /*******************************
        * public
        *******************************/
        //座標
        public MyUtil.IPosition IPosition()
        {
            return position;
        }

        public MyUtil.IVelocity IVelocity()
        {
            return velocity;
        }
        public MyUtil.IRotate IRotate()
        {
            return rotate;
        }

        //角度

        public TransForm(ref TOwner owner)
        {
            this.owner = owner;
            position   = new MyUtil.Position<TOwner>(ref owner);
            velocity   = new MyUtil.Velocity();
            rotate     = new MyUtil.Rotate<TOwner>(ref owner);
        }

        public void Init()
        {
            SetRigidBody();
            velocity.SetRigidbody(ref rigidbody);
        }

        //移動の更新
        public void PositionUpdate(MyUtil.MoveType moveType)
        {
            //timeScaleが0だったら処理を終了
            if (Time.timeScale == 0)
            {
                velocity.InitVelocity();
                return;
            }
            //速度の更新
            velocity.Update(moveType);
            //移動の更新
            switch (moveType)
            {
                case MyUtil.MoveType.Rigidbody:
                    if(rigidbody == null)
                    {
                        SetRigidBody();
                    }
                    position.UpdateByRigidBody(ref rigidbody, velocity.GetVelocity(), velocity.IsUseGravity());
                    break;
                case MyUtil.MoveType.Transform:
                    position.UpdateByTransform(velocity.GetVelocity());
                    break;
            }
        }

        /// <summary>
        /// 角度の更新
        /// 引数の方向に回転させる
        /// </summary>
        /// <param name="vec">向く方向</param>
        public void RotateUpdateToVec(Vector3 vec, float rotateSpeed)
        {
            rotate.RotateUpdateToVec(vec, rotateSpeed);
        }
    }
}
