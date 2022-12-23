using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //////////////////////////////
    /// private

    [SerializeField] MemoryData memoryData;
    SaveData   saveData;
    
    [SerializeField] TextAsset saveDataText;


    [Header("memorySprite�ɂ́A�������̉摜���uMemory.cs MemoryType�v�̏��Ԃɔz�u���Ă��������B")]
    [SerializeField] Sprite[] memorySprite;

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
        saveData   = new SaveData();
    }

    //�������f�[�^�̃C���^�[�t�F�C�X���擾
    public IMemoryData GetIMemoryData()
    {
        return memoryData;
    }

    public Sprite GetMemorySprite(MemoryType type)
    {
        //todo:��O����
        return memorySprite[(int)type];
    }



    public void testSave()
    {
        saveData.Save();
    }
}
