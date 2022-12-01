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
    /// Õ“Ë”»’è
    //////////////////////////////
    private void OnTriggerStay2D(Collider2D collision)
    {
        //‡¬ƒtƒŒ[ƒ€
        if (collision.tag == "SelectMemory")
        {
            //‘fŞ‚ğ“n‚·ó‘Ô‚È‚ç‚ÎA
            if(collision.GetComponent<SelectMemory>().isSetMaterial())
            {
                //‘fŞ‚Ìî•ñ‚ğ–á‚¤
                memoryType = collision.GetComponent<SelectMemory>().GetMemoryType();

                //‰æ‘œ‚ğ‘fŞ‚Ì•¨‚Æ“¯‚¶‚É‚·‚é
                this.gameObject.GetComponent<Image>().sprite = collision.GetComponent<Image>().sprite;
                //•s“§–¾‚É‚·‚é
                this.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);

                //‘fŞ‚Ìİ’èI—¹
                collision.GetComponent<SelectMemory>().SetMaterialEnd();
            }
        }
    }
}
