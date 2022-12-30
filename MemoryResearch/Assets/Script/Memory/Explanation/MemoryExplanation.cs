using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryExplanation : MonoBehaviour
{
    //////////////////////////////
    /// private
    IMemoryData iMemoryData;

    void Awake()
    {
        iMemoryData = GameObject.FindWithTag("DataManager").GetComponent<DataManager>().GetIMemoryData();
    }

    //////////////////////////////
    /// public
    public void ChangeExplanation(MemoryType type)
    {
        //\n�����̂܂ܓǂݍ���ł���Ɖ��s�R�[�h�Ƃ��ĔF������Ȃ��̂ŁA/n��ǂݍ����\n�ɕϊ����Ă���
        gameObject.GetComponent<Text>().text = iMemoryData.GetData(type).explanation.Replace("/n", "\n");
    }
}
