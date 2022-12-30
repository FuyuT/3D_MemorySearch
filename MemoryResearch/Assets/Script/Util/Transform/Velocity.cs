using UnityEngine;

namespace MyUtil
{
    public enum VelocityState
    {
        isNone, //yのvelocityが0の時
        isUp,   //yのvelocityが+の時
        isDown, //yのvelocity-の時
    }

    public class Velocity : IVelocity
    {
        /*******************************
        * private
        *******************************/
        //速度
        Vector3 velocity;
        //最大速度
        Vector3 maxVelocity;
        //重力
        bool isUseGravity;

        //速度情報
        VelocityState velocityState;

        Rigidbody rigidbody;

        //最高速度を超えないように調整
        void AdjustVelocity()
        {
            if(velocity.x > maxVelocity.x)
            {
                velocity.x = maxVelocity.x;
            }
            if (velocity.y > maxVelocity.y)
            {
                velocity.y = maxVelocity.y;
            }
            if (velocity.z > maxVelocity.z)
            {
                velocity.z = maxVelocity.z;
            }
        }

        void StateUpdate(MyUtil.MoveType moveType)
        {
            switch (System.Math.Sign(velocity.y))
            {
                case 0:
                    velocityState = VelocityState.isNone;
                    break;
                case -1:
                    velocityState = VelocityState.isDown;
                    break;
                case 1:
                    velocityState = VelocityState.isUp;
                    break;
            }

            switch (moveType)
            {
                case MoveType.Rigidbody:
                    switch (System.Math.Sign(rigidbody.velocity.y))
                    {
                        case -1:
                            velocityState = VelocityState.isDown;
                            break;
                    }
                    break;
                case MoveType.Transform:
                    break;
            }
        }

        /*******************************
        * public
        *******************************/

        public Velocity()
        {
            velocity        = Vector3.zero;
            velocity        = Vector3.zero;
            maxVelocity     = new Vector3(100, 100, 100);
            isUseGravity    = false;
        }

        public void Update(MyUtil.MoveType moveType)
        {
            AdjustVelocity();
            StateUpdate(moveType);
        }

        //Velocity
        public float GetVelocityX()
        {
            return velocity.x;
        }
        public float GetVelocityY()
        {
            return velocity.y;
        }
        public float GetVelocityZ()
        {
            return velocity.z;
        }

        public Vector3 GetVelocity()
        {
            return velocity;
        }

        public void AddVelocityX(float x)
        {
            velocity.x += x;
        }
        public void AddVelocityY(float y)
        {
            velocity.y += y;
        }
        public void AddVelocityZ(float z)
        {
            velocity.z += z;
        }
        public void AddVelocity(Vector3 vec)
        {
            velocity += vec;
        }


        public void SetVelocityX(float x)
        {
            velocity.x = x;
        }
        public void SetVelocityY(float y)
        {
            velocity.y = y;
        }
        public void SetVelocityZ(float z)
        {
            velocity.z = z;
        }
        public void SetVelocity(Vector3 vec)
        {
            velocity = vec;
        }

        public void InitVelocity()
        {
            velocity = Vector3.zero;
        }

        public void InitRigidBodyVelocity()
        {
            rigidbody.velocity = Vector3.zero;
        }

        //maxVelocity
        public Vector3 GetMaxVelocity()
        {
            return maxVelocity;
        }
        public void SetMaxVelocity(Vector3 maxVelocity)
        {
            this.maxVelocity = maxVelocity;
        }

        //gravity
        public bool IsUseGravity()
        {
            return isUseGravity;
        }
        public void SetUseGravity(bool flg)
        {
            isUseGravity = flg;
        }

        public void SetRigidbody(ref Rigidbody rigidbody)
        {
            this.rigidbody = rigidbody;
        }

        //velocityInfo
        public MyUtil.VelocityState GetState()
        {
            return velocityState;
        }

    }
}