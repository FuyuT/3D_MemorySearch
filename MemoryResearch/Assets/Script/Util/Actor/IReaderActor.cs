using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReaderActor
{
    //todo:パラメータへのアクセス権限を厳しくする

    /// <summary>
    /// パラメータ取得
    /// </summary>
    /// <returns>AnyParameterMap</returns>
    ref AnyParameterMap GetParameterMap();

    #region 座標関係
    /// <summary>
    /// 座標取得
    /// </summary>
    /// <returns>座標X</returns>
    float GetPositionX();

    /// <summary>
    /// 座標取得
    /// </summary>
    /// <returns>座標Y</returns>
    float GetPositionY();

    /// <summary>
    /// 座標取得
    /// </summary>
    /// <returns>座標Z</returns>
    float GetPositionZ();

    /// <summary>
    /// 座標取得
    /// </summary>
    /// <returns>座標</returns>
    Vector3 GetPosition();

    #endregion

    #region 角度関係
    /// <summary>
    /// 角度取得
    /// </summary>
    /// <returns>AnyParameterMap</returns>
    Vector3 GetRotate();
    #endregion
}
