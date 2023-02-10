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

    [Header("memorySprite�ɂ́A�������̉摜���uScript/Memory/Memory.cs MemoryType�v�̏��Ԃɔz�u���Ă��������B")]
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
        //�������[�f�[�^
        memoryData.Load(memoryDataTxt);
        //�G�f�[�^
        enemyData.Load(enemyDataTxt);

        //�Z�[�u�f�[�^�p�X�̎擾
        saveDataPath = SaveDataProcess.GetSaveDataPath();
        //�Z�[�u�f�[�^�̓ǂݍ���
        SaveDataProcess.Load(saveDataPath, ref playerData, ref optionData);

        //�Z�[�u�f�[�^���f
        //�T�E���h
        try
        {
            SoundManager soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
            soundManager.BgmVolume = optionData.soundOption.isMuteBGM ? 0 : optionData.soundOption.bgmVolume;
           
            soundManager.SeVolume = optionData.soundOption.isMuteSE ? 0 : optionData.soundOption.seVolume;
        }
        catch
        {
            Debug.Log("SoundManager��������܂���");
        }
        //���邳
    }

    /*******************************
    * public
    *******************************/
    public static DataManager instance;

    //�������f�[�^�̃C���^�[�t�F�C�X
    public IMemoryData IMemoryData()
    {
        return memoryData;
    }

    //�G�f�[�^�C���^�[�t�F�C�X
    public IEnemyData IEnemyData()
    {
        return enemyData;
    }

    //�v���C���[�f�[�^�̃C���^�[�t�F�C�X
    public IPlayerData IPlayerData()
    {
        return playerData;
    }

    //�I�v�V�����f�[�^�̃C���^�[�t�F�C�X
    public IOptionData IOptionData()
    {
        return optionData;
    }

    //�f�[�^���Z�[�u����
    public void Save()
    {
        SaveDataProcess.Save(saveDataPath, ref playerData, ref optionData);
    }

    //�������Ă��郁�����f�[�^�̏��������s��
    public void IniPossesiontMemoryData()
    {
        //�������Ă��郁�����̐�
        playerData.possesionMemories.Clear();
        //�������Ă��郁����
        for (int n = 0; n < Global.EquipmentMemoryMax; n++)
        {
            playerData.equipmentMemory[n] = MemoryType.None;
        }
        //�����R�X�g
        playerData.possesionCombineCost = 0;
        Debug.Log("�������f�[�^��������");
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
