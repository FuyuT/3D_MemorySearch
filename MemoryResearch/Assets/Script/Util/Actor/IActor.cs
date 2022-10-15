using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActor
{
    /// <summary>
    /// アクターの更新処理
    /// 位置の更新
    /// 角度の更新を行う
    /// </summary>
    void TransformUpdate();

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

    /// <summary>
    /// 座標設定
    /// </summary>
    /// <param name="pos">設定する座標</param>
    void SetPosition(Vector3 pos);
    #endregion

    #region 角度関係
    /// <summary>
    /// 角度取得
    /// </summary>
    /// <returns>AnyParameterMap</returns>
    Vector3 GetRotate();

    /// <summary>
    /// 角度設定
    /// </summary>
    /// <param name="rotate">設定する角度</param>
    void SetRotate(Vector3 rotate);

    #endregion
}
