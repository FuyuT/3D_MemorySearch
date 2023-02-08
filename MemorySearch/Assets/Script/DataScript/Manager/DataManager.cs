using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    /*******************************
    * private
    *******************************/
    [SerializeField] TextAsset memoryDataTxt;
    [SerializeField] TextAsset enemyDataTxt;

    [Header("memorySpriteには、メモリの画像を「Script/Memory/Memory.cs MemoryType」の順番に配置してください。")]
    [SerializeField] Sprite[] memorySprite;

    MemoryData memoryData;
    PlayerData playerData;
    EnemyData  enemyData;
    OptionData optionData;

    string saveDataPath;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            memoryData = new MemoryData();
            playerData = new PlayerData();
            enemyData  = new EnemyData();
            optionData = new OptionData();
            Load();
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Load()
    {
        //メモリーデータ
        memoryData.Load(memoryDataTxt);
        //敵データ
        enemyData.Load(enemyDataTxt);

        //セーブデータパスの取得
        saveDataPath = SaveDataProcess.GetSaveDataPath();
        //セーブデータの読み込み
        SaveDataProcess.Load(saveDataPath, ref playerData, ref optionData);

        //セーブデータ反映
        //サウンド
        try
        {
            SoundManager soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
            soundManager.BgmVolume = optionData.soundOption.isMuteBGM ? 0 : optionData.soundOption.bgmVolume;
           
            soundManager.SeVolume = optionData.soundOption.isMuteSE ? 0 : optionData.soundOption.seVolume;
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

    //メモリデータのインターフェイス
    public IMemoryData IMemoryData()
    {
        return memoryData;
    }

    //敵データインターフェイス
    public IEnemyData IEnemyData()
    {
        return enemyData;
    }

    //プレイヤーデータのインターフェイス
    public IPlayerData IPlayerData()
    {
        return playerData;
    }

    //オプションデータのインターフェイス
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
