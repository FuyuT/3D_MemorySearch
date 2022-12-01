using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButtonManager : MonoBehaviour
{
    [SerializeField] DataManager dataManager;
    [SerializeField] GameObject  selectMemory;

    public void Awake()
    {
    }

    public void SelectMemory(GameObject Object)
    {
        dataManager.SetMaterial(MemoryTypeEnum.Jump);

        //�I�������������̎�ނ�ݒ�
        selectMemory.GetComponent<SelectMemory>().SetMemoryType(Object.GetComponent<MemoryType>().GetMemoryType());

        //�I�������������̉摜���擾���āA�I�𒆃������ɓn��
        selectMemory.GetComponent<Image>().sprite = Object.GetComponent<Image>().sprite;

        //�I�𒆃��������h���b�O��ԂɈڍs����
        selectMemory.GetComponent<SelectMemory>().SetSituationDragMaterial();

        //�F�ݒ�
        var color = selectMemory.GetComponent<Image>().color;
        //selectMemory.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 255); 
    }

}
