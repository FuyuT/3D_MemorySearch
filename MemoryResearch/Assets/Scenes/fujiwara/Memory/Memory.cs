using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/*このenumは、必ずcsvデータ「Data/AdjustmentData　メモリ設定用」
  に列挙されているメモリのidと同じ番号にしてください
  揃えていないと正しく読み込むことができません*/
public enum MemoryType
{
    None,
    Jump,
    DowbleJump,
    Punch,
    AirDush,
    Dush,
    Slam,
    Tackle,
    Guard,
    Boost,
    JetPack,
    BigPunch,
    InvisibleGuard,
    Count,
}

[System.Serializable]
public class Memory
{
    public MemoryType   type;
    public string       explanation;
    public MemoryType[] materialType;

    public Memory()
    {
        type = new MemoryType();
        materialType = new MemoryType[Global.MemoryMaterialMax];
    }
}
