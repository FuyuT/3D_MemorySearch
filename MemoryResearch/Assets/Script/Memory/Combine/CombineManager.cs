using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombineManager : MonoBehaviour
{
    MemoryType[] beforeMaterial;

    [SerializeField] MemoryUI[]        combineMaterial;
    [SerializeField] MemoryUI          combineResult;
    [SerializeField] MemoryExplanation explanation;

    [SerializeField] BatteryCountUI    combineCostUI;
    [SerializeField] BatteryCountUI    fromCostUI;
    [SerializeField] BatteryCountUI    toCostUI;

    [SerializeField] Image             combineButtonImage;
    [SerializeField] Color             buttonInitColor;
    [SerializeField] Color             buttonChangeColor;

    /*******************************
    * private
    *******************************/

    float combineCost = 0;
    float possesionCost = 0;
    float toCost = 0;
    void Awake()
    {
        beforeMaterial = new MemoryType[Global.MemoryMaterialMax];
        for(int n = 0; n < beforeMaterial.Length; n++)
        {
            beforeMaterial[n] = MemoryType.None;
        }
        buttonInitColor = combineButtonImage.color;
    }

    private void OnEnable()
    {
        possesionCost = DataManager.instance.IPlayerData().GetPossesionCombineCost();
        toCostUI.SetBatteryCount(possesionCost);
        ButtonColorUpdate();
        CostUpdate(combineResult.GetMemoryType());
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

        //合成後のメモリを検索
        MemoryType combineMemory = FindCombineMemory();

        CostUpdate(combineMemory);

        //合成後のメモリが見つかれば合成後のメモリに変更
        if (combineMemory != MemoryType.None)
        {
            combineResult.ChangeMemoryType(combineMemory);
            //説明文を変更
            explanation.ChangeExplanation(combineMemory);
        }
        //見つからなければ各要素を初期化
        else
        {
            combineResult.ChangeMemoryType(MemoryType.None);
        }

        ButtonColorUpdate();
    }

    void CostUpdate(MemoryType combineMemory)
    {
        //合成コストの取得
        var datamanager = DataManager.instance;
        possesionCost = DataManager.instance.IPlayerData().GetPossesionCombineCost();
        combineCost = datamanager.IMemoryData().GetData(combineMemory).combineCost;
        toCost = possesionCost - combineCost;

        fromCostUI.SetBatteryCount(possesionCost);

        if (combineMemory != MemoryType.None)
        {
            //コストの数を変更
            combineCostUI.SetBatteryCount(combineCost);
            //コストを使用した後の値を設定
            toCostUI.UpdateToCost(toCost);
        }
        else
        {
            combineCostUI.SetBatteryCount(0);
            toCostUI.InitToCost(possesionCost);
        }
    }

    void ButtonColorUpdate()
    {
        if (combineResult.GetMemoryType() == MemoryType.None 
            || DataManager.instance.IPlayerData().PossesionMemoryIsContain(combineResult.GetMemoryType()))
        {
            combineButtonImage.color = buttonChangeColor;
            return;
        }

        //ボタンのカラー変更
        if (toCost >= 0)
        {
            combineButtonImage.color = buttonInitColor;
        }
        else
        {
            combineButtonImage.color = buttonChangeColor;
        }
    }

    MemoryType FindCombineMemory()
    {
        //各素材の種類をもとに、合成後のメモリを検索
        MemoryType[] material = new MemoryType[Global.MemoryMaterialMax];
        material[0] = combineMaterial[0].GetMemoryType();
        material[1] = combineMaterial[1].GetMemoryType();
        material[2] = combineMaterial[2].GetMemoryType();

        return DataManager.instance.IMemoryData().FindCombineMemory(material);
    }

    /*******************************
    * public
    *******************************/

    //メモリを合成して、所持メモリに加える
    public void CombineMemory()
    {
        if (combineResult.GetMemoryType() == MemoryType.None) return;

        if (toCost < 0) return;

        var playerData = DataManager.instance.IPlayerData();
        if (playerData.PossesionMemoryIsContain(combineResult.GetMemoryType())) return;

        playerData.AddPossesionMemory(combineResult.GetMemoryType());

        //各要素を更新
        playerData.SetPossesionCombineCost(toCost);
        CostUpdate(combineResult.GetMemoryType());
        ButtonColorUpdate();
    }
}
