using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.IO;


public class PlayerData
{
    //////////////////////////////
    /// public
    public Dictionary<MemoryData, bool> isMemoryPossible;

    public MemoryData[] equipmentMemory;

    public PlayerData()
    {
        isMemoryPossible = new Dictionary<MemoryData, bool>();
        equipmentMemory  = new MemoryData[Global.EquipmentMemoryMax];
    }
}

public class SaveData
{
    //////////////////////////////
    /// private
    PlayerData playerData;

    const string Edit_Directory_Path = "/Assets/Resources/Data/SaveData";
    const string Build_Directory_Path = "/SaveData";
    const string File_Path = "/SaveData.txt";

    string path;
    //////////////////////////////
    /// public
    public SaveData()
    {
        playerData = new PlayerData();

        //パスの設定　ディレクトリ、ファイルが無ければ作成
        path = Directory.GetCurrentDirectory();
#if UNITY_EDITOR
        path += (Edit_Directory_Path);
#else
        path += (Build_Directory_Path);
#endif
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        path += (File_Path);
    }

    public void Load(TextAsset dataText)
    {

    }

    public void Save()
    {
        using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
        {

            sw.WriteLine("通った");
        }
    }
}
