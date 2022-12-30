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

    [Header("memorySprite�ɂ́A�������̉摜���uMemory.cs MemoryType�v�̏��Ԃɔz�u���Ă��������B")]
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

    //�R���X�g���N�^
    public DataManager()
    {
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
