using UnityEngine;

public interface IReadPlayer
{
    /// <summary>
    /// 死んでいるかを返す
    /// </summary>
    /// <returns>死んでいればtrue、そうでなければfalse</returns>
    bool IsDead();

    bool IsEndDeadMotion();

    //座標の取得
    Vector3 GetPos();

    //HPの取得
    int GetHP();
}