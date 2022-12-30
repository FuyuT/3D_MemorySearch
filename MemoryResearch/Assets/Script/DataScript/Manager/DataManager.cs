using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    /*******************************
    * private
    *******************************/
    [SerializeField] TextAsset memoryDataTxt;

    [Header("memorySprite�ɂ́A�������̉摜���uMemory.cs MemoryType�v�̏��Ԃɔz�u���Ă��������B")]
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
        //�������[�f�[�^�̓ǂݍ���
        memoryData.Load(memoryDataTxt);

        //�Z�[�u�f�[�^�p�X�̎擾
        saveDataPath = SaveDataProcess.GetSaveDataPath();
        //�Z�[�u�f�[�^�̓ǂݍ���
        SaveDataProcess.Load(saveDataPath, ref playerData);
    }

    /*******************************
    * public
    *******************************/
    public static DataManager instance;

    //�������f�[�^�̃C���^�[�t�F�C�X���擾
    public IMemoryData IMemoryData()
    {
        return memoryData;
    }

    //�v���C���[�f�[�^�̃C���^�[�t�F�C�X���擾
    public IPlayerData IPlayerData()
    {
        return playerData;
    }

    //�f�[�^���Z�[�u����
    public void Save()
    {
        SaveDataProcess.Save(saveDataPath, ref playerData);
    }

    //�������̉摜���擾
    public Sprite GetMemorySprite(MemoryType type)
    {
        try
        {
            return memorySprite[(int)type];
        }
        catch
        {
            //�z��O�̐�����������0�Ԃ̗v�f��Ԃ�
            return memorySprite[0];
        }
    }
}
