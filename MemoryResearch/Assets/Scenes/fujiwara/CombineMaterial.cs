using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CombineMaterial : MonoBehaviour
{
    //////////////////////////////
    /// private
    //////////////////////////////
    MemoryTypeEnum memoryType;

    private void Awake()
    {
        memoryType = MemoryTypeEnum.None;
    }

    //////////////////////////////
    /// public
    //////////////////////////////

    public MemoryTypeEnum GetMemoryType()
    {
        return memoryType;
    }

    //////////////////////////////
    /// �Փ˔���
    //////////////////////////////
    private void OnTriggerStay2D(Collider2D collision)
    {
        //�����t���[��
        if (collision.tag == "SelectMemory")
        {
            //�f�ނ�n����ԂȂ�΁A
            if(collision.GetComponent<SelectMemory>().isSetMaterial())
            {
                //�f�ނ̏���Ⴄ
                memoryType = collision.GetComponent<SelectMemory>().GetMemoryType();

                //�摜��f�ނ̕��Ɠ����ɂ���
                this.gameObject.GetComponent<Image>().sprite = collision.GetComponent<Image>().sprite;
                //�s�����ɂ���
                this.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);

                //�f�ނ̐ݒ�I��
                collision.GetComponent<SelectMemory>().SetMaterialEnd();
            }
        }
    }
}
