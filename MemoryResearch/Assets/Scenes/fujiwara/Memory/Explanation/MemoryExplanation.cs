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
        //\nをそのまま読み込んでくると改行コードとして認識されないので、/nを読み込んで\nに変換している
        gameObject.GetComponent<Text>().text = iMemoryData.GetData(type).explanation.Replace("/n", "\n");
    }
}
