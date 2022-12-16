using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MemoryUI : MonoBehaviour
{
    //////////////////////////////
    /// private

    [SerializeField] MemoryType memoryType;

    CombineManager combineManager;

    void Awake()
    {
        combineManager = GameObject.FindWithTag("CombineManager").GetComponent<CombineManager>();
    }

    //////////////////////////////
    /// public

    public bool IsTypeNone()
    {
        return memoryType == MemoryType.None ? true : false;
    }

    public MemoryType GetMemoryType()
    {
        return memoryType;
    }

    //�������̓���ւ�
    public void ReplaceMemory(UnityEngine.Sprite sprite, MemoryType memoryType)
    {
        if (memoryType == MemoryType.None) return;
        GetComponent<Image>().sprite = sprite;  //�摜�̕ύX
        this.memoryType = memoryType;           //�������̎�ޕύX
    }

    //////////////////////////////
    /// �Փ˔���
    private void OnTriggerStay2D(Collider2D collision)
    {
        //�����������ƍ���������
        if (collision.tag == "SelectMemory")
        {
            switch(collision.GetComponent<SelectMemoryPreview>().GetSituation())
            {
                case SelectMemoryPreview.SituationType.Set_Memory:
                    SetMemoryUIInfo(collision);
                    break;
                case SelectMemoryPreview.SituationType.Replace_Memory:
                    //����ւ�����
                    collision.GetComponent<SelectMemoryPreview>().ReplaceMemory(this.gameObject.GetComponent<Image>().sprite, memoryType);
                    SetMemoryUIInfo(collision);
                    //todo:�����������̏ꍇ�ACombineManager�ɑf�ނ��ς�������Ƃ�m�点��
                    //if(collision.tag == "CombineMemory")
                    //{
                    //    combineManager.ChangeCombineMemory();
                    //}
                    break;
                default:
                    break;
            }
        }
    }


    void SetMemoryUIInfo(Collider2D collision)
    {
        //�f�ނ̎�ނ��擾
        memoryType = collision.GetComponent<SelectMemoryPreview>().GetMemoryType();

        //�摜��f�ނ̕��Ɠ����ɂ���
        this.gameObject.GetComponent<Image>().sprite = collision.GetComponent<Image>().sprite;
        //�s�����ɂ���
        this.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);

        //�f�ނ̐ݒ�I��
        collision.GetComponent<SelectMemoryPreview>().EndPreview();
    }
}
