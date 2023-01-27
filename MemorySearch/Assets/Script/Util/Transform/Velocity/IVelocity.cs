using UnityEngine;

namespace MyUtil
{
    public interface IVelocity
    {
        float GetVelocityX();
        float GetVelocityY();
        float GetVelocityZ();
        Vector3 GetVelocity();

        public void AddVelocityX(float x);
        public void AddVelocityY(float y);
        public void AddVelocityZ(float z);
        public void AddVelocity(Vector3 vec);

        public void SetVelocityX(float x);
        public void SetVelocityY(float y);
        public void SetVelocityZ(float z);
        public void SetVelocity(Vector3 vec);

        public void InitVelocity();
        public void InitRigidBodyVelocity();

        public Vector3 GetMaxVelocity();
        public void SetMaxVelocityX(float maxVelocityX);
        public void SetMaxVelocityY(float maxVelocityY);
        public void SetMaxVelocityZ(float maxVelocityZ);
        public void SetMaxVelocity(Vector3 maxVelocity);

        public bool IsUseGravity();
        public void SetUseGravity(bool flg);

        public void SetRigidbody(ref Rigidbody rigidbody);

        public MyUtil.VelocityState GetState();
    }
}