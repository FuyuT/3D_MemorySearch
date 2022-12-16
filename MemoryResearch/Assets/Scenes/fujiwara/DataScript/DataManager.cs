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
    /// �������f�[�^���擾
    /// </summary>
    /// <param name="memoryType">���</param>
    /// <returns>�������f�[�^</returns>
    public Memory GetMemoryData(MemoryType memoryType)
    {
        return memoryData.GetData(memoryType);
    }

    //�f�ނ��獇������郁����������
    public MemoryType FindCombineMemory(MemoryType[] material)
    {
        return memoryData.FindCombineMemory(material);
    }
}
