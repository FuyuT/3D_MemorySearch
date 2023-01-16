using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombineManager : MonoBehaviour
{
    MemoryType[] beforeMaterial;

    [SerializeField] MemoryUI[]        combineMaterial;
    [SerializeField] MemoryUI          combineResult;
    [SerializeField] MemoryExplanation explanation;

    [SerializeField] BatteryCountUI    combineCostUI;
    [SerializeField] BatteryCountUI    fromCostUI;
    [SerializeField] BatteryCountUI    toCostUI;

    [SerializeField] Image             combineButtonImage;
    [SerializeField] Color             buttonInitColor;
    [SerializeField] Color             buttonChangeColor;

    /*******************************
    * private
    *******************************/

    float combineCost = 0;
    float possesionCost = 0;
    float toCost = 0;
    void Awake()
    {
        beforeMaterial = new MemoryType[Global.MemoryMaterialMax];
        for(int n = 0; n < beforeMaterial.Length; n++)
        {
            beforeMaterial[n] = MemoryType.None;
        }
        buttonInitColor = combineButtonImage.color;
    }

    private void OnEnable()
    {
        possesionCost = DataManager.instance.IPlayerData().GetPossesionCombineCost();
        toCostUI.SetBatteryCount(possesionCost);
        ButtonColorUpdate();
        CostUpdate(combineResult.GetMemoryType());
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

        //������̃�����������
        MemoryType combineMemory = FindCombineMemory();

        CostUpdate(combineMemory);

        //������̃�������������΍�����̃������ɕύX
        if (combineMemory != MemoryType.None)
        {
            combineResult.ChangeMemoryType(combineMemory);
            //��������ύX
            explanation.ChangeExplanation(combineMemory);
        }
        //������Ȃ���Ίe�v�f��������
        else
        {
            combineResult.ChangeMemoryType(MemoryType.None);
        }

        ButtonColorUpdate();
    }

    void CostUpdate(MemoryType combineMemory)
    {
        //�����R�X�g�̎擾
        var datamanager = DataManager.instance;
        possesionCost = DataManager.instance.IPlayerData().GetPossesionCombineCost();
        combineCost = datamanager.IMemoryData().GetData(combineMemory).combineCost;
        toCost = possesionCost - combineCost;

        fromCostUI.SetBatteryCount(possesionCost);

        if (combineMemory != MemoryType.None)
        {
            //�R�X�g�̐���ύX
            combineCostUI.SetBatteryCount(combineCost);
            //�R�X�g���g�p������̒l��ݒ�
            toCostUI.UpdateToCost(toCost);
        }
        else
        {
            combineCostUI.SetBatteryCount(0);
            toCostUI.InitToCost(possesionCost);
        }
    }

    void ButtonColorUpdate()
    {
        if (combineResult.GetMemoryType() == MemoryType.None 
            || DataManager.instance.IPlayerData().PossesionMemoryIsContain(combineResult.GetMemoryType()))
        {
            combineButtonImage.color = buttonChangeColor;
            return;
        }

        //�{�^���̃J���[�ύX
        if (toCost >= 0)
        {
            combineButtonImage.color = buttonInitColor;
        }
        else
        {
            combineButtonImage.color = buttonChangeColor;
        }
    }

    MemoryType FindCombineMemory()
    {
        //�e�f�ނ̎�ނ����ƂɁA������̃�����������
        MemoryType[] material = new MemoryType[Global.MemoryMaterialMax];
        material[0] = combineMaterial[0].GetMemoryType();
        material[1] = combineMaterial[1].GetMemoryType();
        material[2] = combineMaterial[2].GetMemoryType();

        return DataManager.instance.IMemoryData().FindCombineMemory(material);
    }

    /*******************************
    * public
    *******************************/

    //���������������āA�����������ɉ�����
    public void CombineMemory()
    {
        if (combineResult.GetMemoryType() == MemoryType.None) return;

        if (toCost < 0) return;

        var playerData = DataManager.instance.IPlayerData();
        if (playerData.PossesionMemoryIsContain(combineResult.GetMemoryType())) return;

        playerData.AddPossesionMemory(combineResult.GetMemoryType());

        //�e�v�f���X�V
        playerData.SetPossesionCombineCost(toCost);
        CostUpdate(combineResult.GetMemoryType());
        ButtonColorUpdate();
    }
}
