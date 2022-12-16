using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//このenumは、必ずcsvデータ「MemoryData」に列挙されているMemoryTypeと同じ順番で定義する
public enum MemoryType
{
    None = -1,
    Jump,
    DowbleJump,
    Punch,
    AirDush,
    Dush,
    Tackle,


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
