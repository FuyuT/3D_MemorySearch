using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public static class SaveDataProcess
{
    /*******************************
    * private
    *******************************/
    const string Edit_Directory_Path = "/Assets/Resources/Data/SaveData";
    const string Build_Directory_Path = "/SaveData";
    const string File_Path = "/SaveData.txt";

    enum SaveDataLine
    {
        PossesionMemoryCount,
        PossesionMemory,
        EquipmentMemory,
        Option,
    }

    /*******************************
    * public
    *******************************/
    
    /// <summary>
    /// セーブデータのパスの取得
    /// ファイルが無ければ作成する
    /// </summary>
    /// <returns></returns>
    public static string GetSaveDataPath()
    {
        //パスの設定　ディレクトリ、ファイルが無ければ作成
        string path = Directory.GetCurrentDirectory();
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

        return path;
    }

    /// <summary>
    /// セーブデータのロード
    /// </summary>
    /// <param name="path">ファイルのパス</param>
    /// <param name="playerData">プレイヤーデータ</param>
    public static void Load(string path ,ref PlayerData playerData)
    {
        //データをバッファに読み込み
        List<string[]> dataBuffer = MyUtil.TextUtility.ReadCSVData(path);

        //各データの設定
        //所持しているメモリの数
        int memoryPossesionCount = int.Parse(dataBuffer[(int)SaveDataLine.PossesionMemoryCount][0]);
        //所持しているメモリ
        for(int n = 0; n < memoryPossesionCount; n++)
        {
            playerData.possesionMemories.Add(
                (MemoryType)int.Parse(dataBuffer[(int)SaveDataLine.PossesionMemory][n]));
        }

        //装備しているメモリ
        for (int n = 0; n < Global.EquipmentMemoryMax; n++)
        {
            playerData.equipmentMemory[n] = (MemoryType)int.Parse(dataBuffer[(int)SaveDataLine.EquipmentMemory][n]);
        }

        //todo:追加のセーブデータ
        //BGM音量

        //SE音量

        //マウス感度X

        //マウス感度Y

    }

    /// <summary>
    /// データのセーブ
    /// </summary>
    /// <param name="path">ファイルのパス</param>
    /// <param name="playerData">プレイヤーデータ</param>
    public static void Save(string path, ref PlayerData playerData)
    {
        List<string> writeData = new List<string>();

        //各データの登録
        //所持しているメモリの数
        writeData.Add(playerData.possesionMemories.Count.ToString() + ",");
        writeData.Add("\n");

        //所持しているメモリ
        for (int n = 0; n < playerData.possesionMemories.Count; n++)
        {
            int memoryNo = (int)playerData.possesionMemories[n];
            writeData.Add(memoryNo.ToString() + ",");
        }
        writeData.Add("\n");

        //装備しているメモリ
        for (int n = 0; n < Global.EquipmentMemoryMax; n++)
        {
            int memoryNo = (int)playerData.equipmentMemory[n];
            writeData.Add(memoryNo.ToString() + ",");
        }
        writeData.Add("\n");

        //todo:追加のセーブデータ
        //BGM音量

        //SE音量

        //マウス感度X

        //マウス感度Y

        //データを書き込む
        MyUtil.TextUtility.WriteText(path, writeData);

        Debug.Log(path + "にセーブした");
    }
}
