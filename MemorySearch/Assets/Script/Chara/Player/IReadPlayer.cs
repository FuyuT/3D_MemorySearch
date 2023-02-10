using UnityEngine;

public interface IReadPlayer
{
    /// <summary>
    /// 死んでいるかを返す
    /// </summary>
    /// <returns>死んでいればtrue、そうでなければfalse</returns>
    bool IsDead(int damage = 0);

    bool IsEndDeadMotion();

    //座標を取得
    Vector3 GetPos();

    //HPを取得
    int GetHP();

    //バッテリーを取得
    CombineBattery GetCombineBattery();
}