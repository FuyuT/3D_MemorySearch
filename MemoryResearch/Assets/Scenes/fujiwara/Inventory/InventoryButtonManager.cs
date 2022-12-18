using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButtonManager : MonoBehaviour
{
    [SerializeField] DataManager dataManager;
    [SerializeField] GameObject  selectMemory;

    public void Awake()
    {
    }

    public void SelectMemory(GameObject Object)
    {
        dataManager.SetMaterial(MemoryTypeEnum.Jump);

        //選択したメモリの種類を設定
        selectMemory.GetComponent<SelectMemory>().SetMemoryType(Object.GetComponent<MemoryType>().GetMemoryType());

        //選択したメモリの画像を取得して、選択中メモリに渡す
        selectMemory.GetComponent<Image>().sprite = Object.GetComponent<Image>().sprite;

        //選択中メモリをドラッグ状態に移行する
        selectMemory.GetComponent<SelectMemory>().SetSituationDragMaterial();

        //色設定
        var color = selectMemory.GetComponent<Image>().color;
        //selectMemory.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 255); 
    }

}
