using UnityEngine;

namespace MyUtil
{
    public interface IPosition
    {
        float GetPositionX();
        float GetPositionY();
        float GetPositionZ();
        Vector3 GetPosition();
    }
}