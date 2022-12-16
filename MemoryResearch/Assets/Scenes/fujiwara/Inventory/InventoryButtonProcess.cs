using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�{�^���̓��͂ŌĂяo������
public class InventoryButtonProcess : MonoBehaviour
{
    //////////////////////////////
    /// private

    [SerializeField] GameObject memoryPreview;

    void SetMemoryInfo(GameObject Object)
    {
        //�I�������������̎�ނ�ݒ�
        memoryPreview.GetComponent<SelectMemoryPreview>().SetMemoryType(Object.GetComponent<MemoryUI>().GetMemoryType());

        //�I�������������̉摜���擾���āA�I�𒆃������ɓn��
        memoryPreview.GetComponent<Image>().sprite = Object.GetComponent<Image>().sprite;

        //�I�𒆃��������h���b�O��ԂɈڍs����
        memoryPreview.GetComponent<SelectMemoryPreview>().DispatchDrag();
    }

    //////////////////////////////
    /// public

    //�������I��
    public void SelectMemory(GameObject Object)
    {
        //�������̎�ނ��ݒ肳��Ă��Ȃ��Ȃ�I��
        if (Object.GetComponent<MemoryUI>().IsTypeNone())
        {
            return;
        }
        else
        {
            SetMemoryInfo(Object);
        }
    }

    //�������̓���ւ��p�@�������I��
    public void SelectReplaceMemory(GameObject Object)
    {
        //�������̎�ނ��ݒ肳��Ă��Ȃ��Ȃ�I��
        if (Object.GetComponent<MemoryUI>().IsTypeNone())
        {
            return;
        }
        else
        {
            SetMemoryInfo(Object);
            //�I�����Ă���I�u�W�F�N�g��n��
            memoryPreview.GetComponent<SelectMemoryPreview>().SetSelectObject(ref Object);
        }
    }
}