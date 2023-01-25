using UnityEngine;

namespace MyUtil
{
    public interface IRotate
    {
        public void RotateUpdateToVec(Vector3 vec, float rotateSpeed);
    }
}