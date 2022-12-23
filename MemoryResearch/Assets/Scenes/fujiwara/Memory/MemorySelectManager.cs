using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorySelectManager : MonoBehaviour
{
    //////////////////////////////
    /// private
    [SerializeField] MemorySelectPreview preview;
    [SerializeField] MemoryExplanation   explanation;
    MemoryUI selectMemory;

    void Update()
    {
        if (preview.GetSituation() == MemorySelectPreview.SituationType.None) return;

        MoveMemory();    
    }

    void MoveMemory()
    {
        switch (preview.GetSituation())
        {
            //�������̃Z�b�g
            case MemorySelectPreview.SituationType.Set_Memory:
                preview.GetCollisionUI().ChangeMemoryType(selectMemory.GetMemoryType());
                break;
            //�������̓���ւ�
            case MemorySelectPreview.SituationType.Replace_Memory:
                MemoryType temp = preview.GetCollisionUI().GetMemoryType();
                preview.GetCollisionUI().ChangeMemoryType(selectMemory.GetMemoryType());
                selectMemory.ChangeMemoryType(temp);
                break;
            //���������폜
            case MemorySelectPreview.SituationType.Remove_Memory:
                selectMemory.ChangeMemoryType(MemoryType.None);
                break;       
            default:
                return;
        }
        preview.EndPreview();
    }

    //////////////////////////////
    /// public
    public enum MoveType
    {
        None,
        Set,
        Replace,
    }

    //��������UI�{�^����I�������ہi�������ہj�ɌĂяo����鏈��
    //�������̑I��
    public void SelectMemory(MemoryUI memoryUi)
    {
        //�������̎�ނ��ݒ肳��Ă��Ȃ��Ȃ�I��
        if (memoryUi.IsTypeNone()) return;

        //�I������Ui�̎Q�Ƃ��i�[
        selectMemory = memoryUi;

        //��������ύX
        explanation.ChangeExplanation(memoryUi.GetMemoryType());

        //�v���r���[�ݒ�
        //�������̎�ނ�ݒ�
        preview.ChangeMemoryType(memoryUi.GetMemoryType());

        //���������Z�b�g����̂��A����ւ���̂��ݒ肷��
        preview.SetMoveType(memoryUi.GetMoveType());

        //�h���b�O��ԂɈڍs����
        preview.GetComponent<MemorySelectPreview>().DispatchDrag();
    }

    //�������̍폜
    public void RemoveMemory(MemoryUI memoryUi)
    {
        //�E�N���b�N���Ă��Ȃ���ΏI��
        if (!Input.GetKey(KeyCode.Mouse1)) return;

        memoryUi.ChangeMemoryType(MemoryType.None);
    }
}
