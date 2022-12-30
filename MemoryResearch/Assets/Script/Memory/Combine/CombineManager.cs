using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombineManager : MonoBehaviour
{
    MemoryType[] beforeMaterial;

    [SerializeField] MemoryUI[]  combineMaterial;
    [SerializeField] MemoryUI    combineResult;
    [SerializeField] MemoryExplanation explanation;

    DataManager dataManager;

    /*******************************
    * private
    *******************************/

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
                beforeMaterial[n] = combineMaterial[n].GetMemoryType();
                return true;
            }
        }
        return false;
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

        MemoryType combineMemory = dataManager.IMemoryData().FindCombineMemory(material);

        //������̃�������������΍�����̃������ɕύX�A������Ȃ����MemoryType.None�ɕύX
        if (combineMemory != MemoryType.None)
        {
            combineResult.ChangeMemoryType(combineMemory);
            //��������ύX
            explanation.ChangeExplanation(combineMemory);
        }
        else
        {
            combineResult.ChangeMemoryType(MemoryType.None);
        }
    }

    /*******************************
    * public
    *******************************/

    //���������������āA�����������ɉ�����
    public void CombineMemory()
    {
        if (combineResult.GetMemoryType() == MemoryType.None) return;

        var playerData = DataManager.instance.IPlayerData();
        playerData.AddPossesionMemory(combineResult.GetMemoryType());
        Debug.Log("�ʂ���");

    }
}
