using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MemoryUI : MonoBehaviour
{
    //////////////////////////////
    /// private

    [SerializeField] MemoryType memoryType;

    CombineManager combineManager;

    void Awake()
    {
        combineManager = GameObject.FindWithTag("CombineManager").GetComponent<CombineManager>();
    }

    //////////////////////////////
    /// public

    public bool IsTypeNone()
    {
        return memoryType == MemoryType.None ? true : false;
    }

    public MemoryType GetMemoryType()
    {
        return memoryType;
    }

    //メモリの入れ替え
    public void ReplaceMemory(UnityEngine.Sprite sprite, MemoryType memoryType)
    {
        if (memoryType == MemoryType.None) return;
        GetComponent<Image>().sprite = sprite;  //画像の変更
        this.memoryType = memoryType;           //メモリの種類変更
    }

    //////////////////////////////
    /// 衝突判定
    private void OnTriggerStay2D(Collider2D collision)
    {
        //装備メモリと合成メモリ
        if (collision.tag == "SelectMemory")
        {
            switch(collision.GetComponent<SelectMemoryPreview>().GetSituation())
            {
                case SelectMemoryPreview.SituationType.Set_Memory:
                    SetMemoryUIInfo(collision);
                    break;
                case SelectMemoryPreview.SituationType.Replace_Memory:
                    //入れ替え処理
                    collision.GetComponent<SelectMemoryPreview>().ReplaceMemory(this.gameObject.GetComponent<Image>().sprite, memoryType);
                    SetMemoryUIInfo(collision);
                    //todo:合成メモリの場合、CombineManagerに素材が変わったことを知らせる
                    //if(collision.tag == "CombineMemory")
                    //{
                    //    combineManager.ChangeCombineMemory();
                    //}
                    break;
                default:
                    break;
            }
        }
    }


    void SetMemoryUIInfo(Collider2D collision)
    {
        //素材の種類を取得
        memoryType = collision.GetComponent<SelectMemoryPreview>().GetMemoryType();

        //画像を素材の物と同じにする
        this.gameObject.GetComponent<Image>().sprite = collision.GetComponent<Image>().sprite;
        //不透明にする
        this.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);

        //素材の設定終了
        collision.GetComponent<SelectMemoryPreview>().EndPreview();
    }
}
