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
        //\nをそのまま読み込んでくると改行コードとして認識されないので、/nを読み込んで\nに変換している
        gameObject.GetComponent<Text>().text =
            DataManager.instance.IMemoryData().GetData(type).explanation.Replace("/n", "\n");
    }
}
