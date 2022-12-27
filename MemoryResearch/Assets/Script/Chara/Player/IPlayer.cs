using UnityEngine;

public interface IPlayer
{
    /// <summary>
    /// 死んでいるかを返す
    /// </summary>
    /// <returns>死んでいればtrue、そうでなければfalse</returns>
    bool IsDead();

    Vector3 GetPos();
}