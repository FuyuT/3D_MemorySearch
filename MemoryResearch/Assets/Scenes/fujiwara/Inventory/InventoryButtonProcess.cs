using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ボタンの入力で呼び出す処理
public class InventoryButtonProcess : MonoBehaviour
{
    //////////////////////////////
    /// private

    [SerializeField] GameObject memoryPreview;

    void SetMemoryInfo(GameObject Object)
    {
        //選択したメモリの種類を設定
        memoryPreview.GetComponent<SelectMemoryPreview>().SetMemoryType(Object.GetComponent<MemoryUI>().GetMemoryType());

        //選択したメモリの画像を取得して、選択中メモリに渡す
        memoryPreview.GetComponent<Image>().sprite = Object.GetComponent<Image>().sprite;

        //選択中メモリをドラッグ状態に移行する
        memoryPreview.GetComponent<SelectMemoryPreview>().DispatchDrag();
    }

    //////////////////////////////
    /// public

    //メモリ選択
    public void SelectMemory(GameObject Object)
    {
        //メモリの種類が設定されていないなら終了
        if (Object.GetComponent<MemoryUI>().IsTypeNone())
        {
            return;
        }
        else
        {
            SetMemoryInfo(Object);
        }
    }

    //メモリの入れ替え用　メモリ選択
    public void SelectReplaceMemory(GameObject Object)
    {
        //メモリの種類が設定されていないなら終了
        if (Object.GetComponent<MemoryUI>().IsTypeNone())
        {
            return;
        }
        else
        {
            SetMemoryInfo(Object);
            //選択しているオブジェクトを渡す
            memoryPreview.GetComponent<SelectMemoryPreview>().SetSelectObject(ref Object);
        }
    }
}