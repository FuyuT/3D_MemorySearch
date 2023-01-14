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
        Possesion_Memory_Count,
        Possesion_Memory,
        Equipment_Memory,
        Option_BGM,
        Option_SE,
        Option_Brightness,
        Option_Sensitivity_X,
        Option_Sensitivity_Y,
        Option_AutoSave,
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
    public static void Load(string path ,ref PlayerData playerData, ref OptionData optionData)
    {
        //データをバッファに読み込み
        List<string[]> dataBuffer = MyUtil.TextUtility.ReadCSVData(path);

        //各データの設定
        //所持しているメモリの数
        int memoryPossesionCount = int.Parse(dataBuffer[(int)SaveDataLine.Possesion_Memory_Count][0]);
        //所持しているメモリ
        for(int n = 0; n < memoryPossesionCount; n++)
        {
            playerData.possesionMemories.Add(
                (MemoryType)int.Parse(dataBuffer[(int)SaveDataLine.Possesion_Memory][n]));
        }

        //装備しているメモリ
        for (int n = 0; n < Global.EquipmentMemoryMax; n++)
        {
            playerData.equipmentMemory[n] = (MemoryType)int.Parse(dataBuffer[(int)SaveDataLine.Equipment_Memory][n]);
        }

        //BGM音量
        optionData.soundOption.bgmVolume = float.Parse(dataBuffer[(int)SaveDataLine.Option_BGM][0]);
        optionData.soundOption.isMuteBGM = bool.Parse(dataBuffer[(int)SaveDataLine.Option_BGM][1]);

        //SE音量
        optionData.soundOption.seVolume = float.Parse(dataBuffer[(int)SaveDataLine.Option_SE][0]);
        optionData.soundOption.isMuteSE = bool.Parse(dataBuffer[(int)SaveDataLine.Option_SE][1]);

        //明るさ
        optionData.brightness = float.Parse(dataBuffer[(int)SaveDataLine.Option_Brightness][0]);

        //マウス感度X
        optionData.aimOption.sensitivity.x = float.Parse(dataBuffer[(int)SaveDataLine.Option_Sensitivity_X][0]);
        optionData.aimOption.isReverseX    = bool.Parse(dataBuffer[(int)SaveDataLine.Option_Sensitivity_X][1]);

        //マウス感度Y
        optionData.aimOption.sensitivity.y = float.Parse(dataBuffer[(int)SaveDataLine.Option_Sensitivity_Y][0]);
        optionData.aimOption.isReverseY = bool.Parse(dataBuffer[(int)SaveDataLine.Option_Sensitivity_Y][1]);

        //オートセーブ
        optionData.isAutoSave = bool.Parse(dataBuffer[(int)SaveDataLine.Option_AutoSave][0]);        
    }

    /// <summary>
    /// データのセーブ
    /// </summary>
    /// <param name="path">ファイルのパス</param>
    /// <param name="playerData">プレイヤーデータ</param>
    public static void Save(string path, ref PlayerData playerData, ref OptionData optionData)
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

        //BGM音量
        writeData.Add(optionData.soundOption.bgmVolume + ",");
        writeData.Add(optionData.soundOption.isMuteBGM + ",");
        writeData.Add("\n");

        //SE音量
        writeData.Add(optionData.soundOption.seVolume + ",");
        writeData.Add(optionData.soundOption.isMuteSE + ",");
        writeData.Add("\n");

        //明るさ
        writeData.Add(optionData.brightness + ",");
        writeData.Add("\n");

        //マウス感度X
        writeData.Add(optionData.aimOption.sensitivity.x + ",");
        writeData.Add(optionData.aimOption.isReverseX + ",");
        writeData.Add("\n");

        //マウス感度Y
        writeData.Add(optionData.aimOption.sensitivity.y + ",");
        writeData.Add(optionData.aimOption.isReverseY + ",");
        writeData.Add("\n");

        //オートセーブ
        writeData.Add(optionData.isAutoSave + ",");
        writeData.Add("\n");

        //データを書き込む
        MyUtil.TextUtility.WriteText(path, writeData);

        Debug.Log(path + "にセーブした");
    }
}
