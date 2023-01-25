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
    OptionData optionData;

    string saveDataPath;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            memoryData = new MemoryData();
            playerData = new PlayerData();
            optionData = new OptionData();
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
        SaveDataProcess.Load(saveDataPath, ref playerData, ref optionData);

        //セーブデータ反映
        //サウンド
        try
        {
            SoundManager soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
            soundManager.BgmVolume = optionData.soundOption.bgmVolume;
            soundManager.SeVolume = optionData.soundOption.seVolume;
        }
        catch
        {
            Debug.Log("SoundManagerが見つかりません");
        }
        //明るさ
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

    //オプションデータのインターフェイスを取得
    public IOptionData IOptionData()
    {
        return optionData;
    }

    //データをセーブする
    public void Save()
    {
        SaveDataProcess.Save(saveDataPath, ref playerData, ref optionData);
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
