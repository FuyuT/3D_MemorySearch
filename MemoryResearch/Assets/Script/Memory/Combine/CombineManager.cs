using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombineManager : MonoBehaviour
{
    MemoryType[] beforeMaterial;

    [SerializeField] MemoryUI[]  combineMaterial;
    [SerializeField] MemoryUI    combineResult;
    [SerializeField] MemoryExplanation explanation;

    DataManager dataManager;

    /*******************************
    * private
    *******************************/

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
                beforeMaterial[n] = combineMaterial[n].GetMemoryType();
                return true;
            }
        }
        return false;
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

        MemoryType combineMemory = dataManager.IMemoryData().FindCombineMemory(material);

        //合成後のメモリが見つかれば合成後のメモリに変更、見つからなければMemoryType.Noneに変更
        if (combineMemory != MemoryType.None)
        {
            combineResult.ChangeMemoryType(combineMemory);
            //説明文を変更
            explanation.ChangeExplanation(combineMemory);
        }
        else
        {
            combineResult.ChangeMemoryType(MemoryType.None);
        }
    }

    /*******************************
    * public
    *******************************/

    //メモリを合成して、所持メモリに加える
    public void CombineMemory()
    {
        if (combineResult.GetMemoryType() == MemoryType.None) return;

        var playerData = DataManager.instance.IPlayerData();
        playerData.AddPossesionMemory(combineResult.GetMemoryType());
        Debug.Log("通った");

    }
}
