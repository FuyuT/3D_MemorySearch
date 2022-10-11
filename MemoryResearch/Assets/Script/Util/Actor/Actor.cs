using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IActor, IReaderActor
{
    AnyParameterMap parameters;

    public Actor()
    {
        parameters = new AnyParameterMap();
    }


    /// <summary>
    /// アクターの更新処理
    /// 位置の更新
    /// 角度の更新を行う
    /// </summary>
    public void TransformUpdate()
    {

    }

    /// <summary>
    /// パラメータ取得
    /// </summary>
    /// <returns>AnyParameterMap</returns>
    public ref AnyParameterMap GetParameterMap()
    {
        return ref parameters;
    }

    #region 座標関係
    /// <summary>
    /// 座標取得
    /// </summary>
    /// <returns>座標X</returns>
    public float GetPositionX()
    {
        return transform.position.x;
    }

    /// <summary>
    /// 座標取得
    /// </summary>
    /// <returns>座標Y</returns>
    public float GetPositionY()
    {
        return transform.position.y;
    }

    /// <summary>
    /// 座標取得
    /// </summary>
    /// <returns>座標Z</returns>
    public float GetPositionZ()
    {
        return transform.position.z;
    }

    /// <summary>
    /// 座標取得
    /// </summary>
    /// <returns>座標</returns>
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    /// <summary>
    /// 座標設定
    /// </summary>
    /// <param name="pos">設定する座標</param>
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    #endregion

    #region 角度関係
    /// <summary>
    /// 角度取得
    /// </summary>
    /// <returns>AnyParameterMap</returns>
    public Vector3 GetRotate()
    {
        return transform.rotation.eulerAngles;
    }

    /// <summary>
    /// 角度設定
    /// </summary>
    /// <param name="rotate">設定する角度</param>
    public void SetRotate(Vector3 rotate)
    {
        transform.rotation = Quaternion.Euler(rotate);
    }

    #endregion
}
