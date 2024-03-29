using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CombineMaterial : MonoBehaviour
{
    //////////////////////////////
    /// private
    //////////////////////////////
    MemoryTypeEnum memoryType;

    private void Awake()
    {
        memoryType = MemoryTypeEnum.None;
    }

    //////////////////////////////
    /// public
    //////////////////////////////

    public MemoryTypeEnum GetMemoryType()
    {
        return memoryType;
    }

    //////////////////////////////
    /// 衝突判定
    //////////////////////////////
    private void OnTriggerStay2D(Collider2D collision)
    {
        //合成フレーム
        if (collision.tag == "SelectMemory")
        {
            //素材を渡す状態ならば、
            if(collision.GetComponent<SelectMemory>().isSetMaterial())
            {
                //素材の情報を貰う
                memoryType = collision.GetComponent<SelectMemory>().GetMemoryType();

                //画像を素材の物と同じにする
                this.gameObject.GetComponent<Image>().sprite = collision.GetComponent<Image>().sprite;
                //不透明にする
                this.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);

                //素材の設定終了
                collision.GetComponent<SelectMemory>().SetMaterialEnd();
            }
        }
    }
}
