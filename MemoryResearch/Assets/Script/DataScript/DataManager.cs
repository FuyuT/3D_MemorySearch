using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    /*******************************
    * private
    *******************************/

    [SerializeField] MemoryData memoryData;
    SaveData   saveData;
    
    [SerializeField] TextAsset saveDataText;

    [Header("memorySpriteには、メモリの画像を「Memory.cs MemoryType」の順番に配置してください。")]
    [SerializeField] Sprite[] memorySprite;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            saveData = new SaveData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        memoryData.Load();

        saveData.Load(saveDataText);
    }

    void Update()
    {
    }

    /*******************************
    * public
    *******************************/
    public static DataManager instance;

    //コンストラクタ
    public DataManager()
    {
    }

    //メモリデータのインターフェイスを取得
    public IMemoryData GetIMemoryData()
    {
        return memoryData;
    }

    public Sprite GetMemorySprite(MemoryType type)
    {
        //todo:例外処理
        return memorySprite[(int)type];
    }

    public void testSave()
    {
        saveData.Save();
    }
}
