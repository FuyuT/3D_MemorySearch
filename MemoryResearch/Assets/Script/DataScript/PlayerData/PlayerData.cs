using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : IPlayerData
{
    /*******************************
    * public
    *******************************/
    public List<MemoryType> possesionMemories;

    public MemoryType[] equipmentMemory;

    public PlayerData()
    {
        possesionMemories = new List<MemoryType>();
        equipmentMemory = new MemoryType[Global.EquipmentMemoryMax];
    }

    //所持メモリ
    public List<MemoryType> GetPossesionMemories()
    {
        return possesionMemories;
    }

    //所持メモリに引数の値が存在するか確認
    public bool PossesionMemoryIsContain(MemoryType type)
    {
        for(int n = 0; n < possesionMemories.Count; n++)
        {
            if(possesionMemories[n] == type)
            {
                return true;
            }
        }
        return false;
    }

    //引数の値を所持メモリに追加
    public void AddPossesionMemory(MemoryType type)
    {
        //すでに存在していれば終了
        if (PossesionMemoryIsContain(type)) return;

        possesionMemories.Add(type);
    }

    //装備メモリ
    public MemoryType GetEquipmentMemory(int n)
    {
        return equipmentMemory[n];
    }

    public void SetEquipmentMemory(int n, MemoryType type)
    {
        equipmentMemory[n] = type;
    }
}