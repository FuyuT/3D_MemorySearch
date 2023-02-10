using System.Collections;
using System.Collections.Generic;

public interface IPlayerData
{
    //所持メモリを取得
    List<MemoryType> GetPossesionMemories();

    //所持メモリに引数の値が存在するか確認
    bool PossesionMemoryIsContain(MemoryType type);

    //引数の値を所持メモリに追加
    void AddPossesionMemory(MemoryType type);

    //装備を取得
    MemoryType GetEquipmentMemory(int n);

    //装備を設定
    void SetEquipmentMemory(int n, MemoryType type);

    //合成コストを取得
    float GetPossesionCombineCost();

    //合成コストを設定
    void SetPossesionCombineCost(float cost);

    //合成コストを追加
    void AddPossesionCombineCost(float add);
}
