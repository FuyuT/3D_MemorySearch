using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombineManager : MonoBehaviour
{
    [SerializeField] CombineMaterial[] combineMaterial;
    [SerializeField] GameObject   combineResult;
    [SerializeField] Sprite[] sprite;

    //////////////////////////////
    /// private
    //////////////////////////////

    void Start()
    {

    }

    void Update()
    {
        //�f�ނ������Ă��邩�m�F
        for(int n = 0; n < combineMaterial.Length; n++)
        {
            if(combineMaterial[n].GetMemoryType() == MemoryTypeEnum.None)
            {
                return;
            }      
        }

        //�����Ă��ĕύX����Ă��邩�m�F

        //�e�f�ނ̎�ނ����ƂɁA������̃�����������
        MemoryTypeEnum memoryType1 = combineMaterial[0].GetMemoryType();
        MemoryTypeEnum memoryType2 = combineMaterial[1].GetMemoryType();

        MemoryTypeEnum combineMemory = MemoryTypeEnum.None;
        switch(memoryType1)
        {
            case MemoryTypeEnum.Jump:
                if(memoryType2 == MemoryTypeEnum.Jump)
                {
                    combineMemory = MemoryTypeEnum.DowbleJump;
                }
                else if (memoryType2 == MemoryTypeEnum.Dush)
                {
                    combineMemory = MemoryTypeEnum.AirDush;
                }
                break;
            case MemoryTypeEnum.Dush:
                if (memoryType2 == MemoryTypeEnum.Jump)
                {
                    combineMemory = MemoryTypeEnum.AirDush;
                }
                break;
        }

        //����������������\������
        switch (combineMemory)
        {
            case MemoryTypeEnum.DowbleJump:
                combineResult.GetComponent<Image>().sprite = sprite[0];
                break;
            case MemoryTypeEnum.AirDush:
                combineResult.GetComponent<Image>().sprite = sprite[1];
                break;
            default:
                combineResult.GetComponent<Image>().color = new Color(255, 255, 255, 0);
                return;
        }
        combineResult.GetComponent<Image>().color  = new Color(255, 255, 255, 255);
    }

    //////////////////////////////
    /// public
    //////////////////////////////
}
