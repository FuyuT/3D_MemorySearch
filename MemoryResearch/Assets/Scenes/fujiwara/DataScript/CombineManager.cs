using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombineManager : MonoBehaviour
{
    MemoryType[] beforeMaterial;

    [SerializeField] MemoryUI[]  combineMaterial;
    [SerializeField] GameObject  combineResult;
    [Header("memorySprite�ɂ́A�������̉摜���uMemory.cs MemoryType�v�̏��Ԃɔz�u���Ă��������B")]
    [SerializeField] Sprite[]    memorySprite;

    DataManager dataManager;

    //////////////////////////////
    /// private

    void Awake()
    {
        beforeMaterial = new MemoryType[Global.MemoryMaterialMax];
        for(int n = 0; n < beforeMaterial.Length; n++)
        {
            beforeMaterial[n] = MemoryType.None;
        }

        dataManager = GameObject.FindWithTag("DataManager").GetComponent<DataManager>();
    }

    //�f�ނ��ύX����Ă��邩
    bool IsChangeMaterial()
    {   
        for (int n = 0; n < beforeMaterial.Length; n++)
        {
            if(beforeMaterial[n] != combineMaterial[n].GetMemoryType())
            {
                return true;
            }
        }
        return false;
    }

    //�f�ނō����ł��郁��������������
    void FindCombineMemory()
    {
        DataManager data = new DataManager();
        MemoryType[] materialType = new MemoryType[Global.MemoryMaterialMax];
        
        if(data.FindCombineMemory(materialType) == MemoryType.None)
        {
            //������Ȃ������ꍇ
        }
    }

    void Update()
    {
        //�O��̑f�ނ���ύX����Ă��邩�m�F
        if (!IsChangeMaterial()) return;

        //�e�f�ނ̎�ނ����ƂɁA������̃�����������
        MemoryType[] material = new MemoryType[Global.MemoryMaterialMax];
        material[0] = combineMaterial[0].GetMemoryType();
        material[1] = combineMaterial[1].GetMemoryType();
        material[2] = combineMaterial[2].GetMemoryType();
        MemoryType combineMemory = dataManager.FindCombineMemory(material);

        //todo:���������������ݒ�
        //������̃�������������΁A����ݒ�
        if(combineMemory != MemoryType.None)
        {
            combineResult.GetComponent<Image>().sprite = memorySprite[(int)combineMemory];
            combineResult.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        else//������Ȃ������瓧����
        {
            combineResult.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }
    }

    //////////////////////////////
    /// public
    public void ChangeCombineMemory()
    {
        Debug.Log("�f�ޕύX");

        //�f�ނ�����Ă��邩�m���߂�
        if (!IsChangeMaterial()) return;

        FindCombineMemory();
    }

}
