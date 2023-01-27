using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReaderActor
{
    #region ���W�֌W
    /// <summary>
    /// ���W�擾
    /// </summary>
    /// <returns>���WX</returns>
    float GetPositionX();

    /// <summary>
    /// ���W�擾
    /// </summary>
    /// <returns>���WY</returns>
    float GetPositionY();

    /// <summary>
    /// ���W�擾
    /// </summary>
    /// <returns>���WZ</returns>
    float GetPositionZ();

    /// <summary>
    /// ���W�擾
    /// </summary>
    /// <returns>���W</returns>
    Vector3 GetPosition();

    #endregion

    #region �p�x�֌W
    /// <summary>
    /// �p�x�擾
    /// </summary>
    /// <returns>AnyParameterMap</returns>
    Vector3 GetRotate();
    #endregion
}
