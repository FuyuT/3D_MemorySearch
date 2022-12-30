using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    /*******************************
    * private
    *******************************/
    [SerializeField] TextAsset memoryDataTxt;

    [Header("memorySpriteには、メモリの画像を「Memory.cs MemoryType」の順番に配置してください。")]
    [SerializeField] Sprite[] memorySprite;

    MemoryData memoryData;
    PlayerData playerData;

    string saveDataPath;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            memoryData = new MemoryData();
            playerData = new PlayerData();

            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Load()
    {
        //メモリーデータの読み込み
        memoryData.Load(memoryDataTxt);

        //セーブデータパスの取得
        saveDataPath = SaveDataProcess.GetSaveDataPath();
        //セーブデータの読み込み
        SaveDataProcess.Load(saveDataPath, ref playerData);
    }

    /*******************************
    * public
    *******************************/
    public static DataManager instance;

    //メモリデータのインターフェイスを取得
    public IMemoryData IMemoryData()
    {
        return memoryData;
    }

    //プレイヤーデータのインターフェイスを取得
    public IPlayerData IPlayerData()
    {
        return playerData;
    }

    //データをセーブする
    public void Save()
    {
        SaveDataProcess.Save(saveDataPath, ref playerData);
    }

    //メモリの画像を取得
    public Sprite GetMemorySprite(MemoryType type)
    {
        try
        {
            return memorySprite[(int)type];
        }
        catch
        {
            //配列外の数字が来たら0番の要素を返す
            return memorySprite[0];
        }
    }
}
