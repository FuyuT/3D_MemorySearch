using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //////////////////////////////
    /// private
    
    [SerializeField] TextAsset saveDataText;

    MemoryData memoryData;
    SaveData   saveData;

    void Start()
    {
        memoryData.Load();

        saveData.Load(saveDataText);
    }

    // Update is called once per frame
    void Update()
    {
    }

    //////////////////////////////
    /// public

    public DataManager()
    {
        memoryData = new MemoryData();
        saveData = new SaveData();
    }


    /// <summary>
    /// メモリデータを取得
    /// </summary>
    /// <param name="memoryType">種類</param>
    /// <returns>メモリデータ</returns>
    public Memory GetMemoryData(MemoryType memoryType)
    {
        return memoryData.GetData(memoryType);
    }

    //素材から合成されるメモリを検索
    public MemoryType FindCombineMemory(MemoryType[] material)
    {
        return memoryData.FindCombineMemory(material);
    }
}
