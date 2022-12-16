using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMemoryPreview : MonoBehaviour
{
    //////////////////////////////
    /// private
    public enum SituationType
    {
        None,
        Drag_Preview,
        Replace_Memory,
        Set_Memory,
    }
    SituationType situation;

    MemoryType memoryType;

    GameObject selectObject;

    void Awake()
    {
        Init();
    }
    private void Init()
    {
        situation = SituationType.None;
        memoryType = MemoryType.None;
        //位置を画面外へ
        transform.position = new Vector3(-100, -100, 0);

        selectObject = null;
    }

    void Update()
    {
        switch(situation)
        {
            case SituationType.None:
                return;
            default:
                DragMaterial();
                break;
        }
    }


    //マテリアルを運ぶ
    void DragMaterial()
    {
        FollowMouse();

        if (Input.GetKey(KeyCode.Mouse0)) return;

        //マウス左クリックしていない時
        switch (situation)
        {
            //設定中ならなにもしない
            case SituationType.Set_Memory:
            case SituationType.Replace_Memory:
                break;
            //設定中でなければ初期化
            default:
                Init();
                break;
        }
    }

    //マウスに追従する
    void FollowMouse()
    {
        //マウスに追従する
        Vector3 mouse = Input.mousePosition;
        this.transform.position = new Vector3(mouse.x, mouse.y, 0);
    }


    //////////////////////////////
    /// public
       
    public void SetSelectObject(ref GameObject memory)
    {
        selectObject = memory;
    }

    public void DispatchDrag()
    {
        situation = SituationType.Drag_Preview;
    }

    public void EndPreview()
    {
        Init();
    }

    public SituationType GetSituation()
    {
        //マウス左クリックを押していたらSituationType.Drag_Previewを返す
        if (Input.GetKey(KeyCode.Mouse0))
        {
            return SituationType.Drag_Preview;
        }
        
        return situation;
    }

    public MemoryType GetMemoryType()
    {
        return memoryType;
    }

    public void SetMemoryType(MemoryType memoryType)
    {
        this.memoryType = memoryType;
    }

    public void ReplaceMemory(UnityEngine.Sprite sprite, MemoryType memoryType)
    {
        if (memoryType == MemoryType.None) return;
        selectObject.GetComponent<MemoryUI>().ReplaceMemory(sprite, memoryType);
    }

    //////////////////////////////
    /// 衝突判定
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (situation == SituationType.None) return;

        //合成フレーム
        if (collision.tag == "EuipmentMemory" || collision.tag == "CombineMemory")
        {
            //選択しているメモリが、すでにセットされているものならば入れ替え状態にする
            if (selectObject != null)
            {
                situation = SituationType.Replace_Memory;
            }
            else
            {
                situation = SituationType.Set_Memory;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (situation == SituationType.None) return;

        //合成フレーム
        if (collision.tag == "CombineMemory")
        {
            situation = SituationType.Drag_Preview;
        }
    }
}
