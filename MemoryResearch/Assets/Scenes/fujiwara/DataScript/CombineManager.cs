using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombineManager : MonoBehaviour
{
    MemoryType[] beforeMaterial;

    [SerializeField] MemoryUI[]  combineMaterial;
    [SerializeField] GameObject  combineResult;
    [Header("memorySpriteには、メモリの画像を「Memory.cs MemoryType」の順番に配置してください。")]
    [SerializeField] Sprite[]    memorySprite;

    DataManager dataManager;

    //////////////////////////////
    /// private

    void Awake()
    {
        beforeMaterial = new MemoryType[Global.MemoryMaterialMax];
        for(int n = 0; n < beforeMaterial.Length; n++)
        {
            beforeMaterial[n] = MemoryType.None;
        }

        dataManager = GameObject.FindWithTag("DataManager").GetComponent<DataManager>();
    }

    //素材が変更されているか
    bool IsChangeMaterial()
    {   
        for (int n = 0; n < beforeMaterial.Length; n++)
        {
            if(beforeMaterial[n] != combineMaterial[n].GetMemoryType())
            {
                return true;
            }
        }
        return false;
    }

    //素材で合成できるメモリを検索する
    void FindCombineMemory()
    {
        DataManager data = new DataManager();
        MemoryType[] materialType = new MemoryType[Global.MemoryMaterialMax];
        
        if(data.FindCombineMemory(materialType) == MemoryType.None)
        {
            //見つからなかった場合
        }
    }

    void Update()
    {
        //前回の素材から変更されているか確認
        if (!IsChangeMaterial()) return;

        //各素材の種類をもとに、合成後のメモリを検索
        MemoryType[] material = new MemoryType[Global.MemoryMaterialMax];
        material[0] = combineMaterial[0].GetMemoryType();
        material[1] = combineMaterial[1].GetMemoryType();
        material[2] = combineMaterial[2].GetMemoryType();
        MemoryType combineMemory = dataManager.FindCombineMemory(material);

        //todo:合成時メモリ情報設定
        //合成後のメモリが見つかれば、情報を設定
        if(combineMemory != MemoryType.None)
        {
            combineResult.GetComponent<Image>().sprite = memorySprite[(int)combineMemory];
            combineResult.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        else//見つからなかったら透明化
        {
            combineResult.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }
    }

    //////////////////////////////
    /// public
    public void ChangeCombineMemory()
    {
        Debug.Log("素材変更");

        //素材が足りているか確かめる
        if (!IsChangeMaterial()) return;

        FindCombineMemory();
    }

}
