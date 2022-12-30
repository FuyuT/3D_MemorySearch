using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryExplanation : MonoBehaviour
{
    /*******************************
    * public 
    *******************************/

    public void ChangeExplanation(MemoryType type)
    {
        //\n�����̂܂ܓǂݍ���ł���Ɖ��s�R�[�h�Ƃ��ĔF������Ȃ��̂ŁA/n��ǂݍ����\n�ɕϊ����Ă���
        gameObject.GetComponent<Text>().text =
            DataManager.instance.IMemoryData().GetData(type).explanation.Replace("/n", "\n");
    }
}
