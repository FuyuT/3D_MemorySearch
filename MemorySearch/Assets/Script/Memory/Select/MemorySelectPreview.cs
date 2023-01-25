using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemorySelectPreview : MemoryUI
{
    /*******************************
    * private
    *******************************/
    public enum SituationType
    {
        None,
        Drag_Preview,
        Replace_Memory,
        Set_Memory,
        Remove_Memory,
    }

    SituationType situation;

    MemoryUI collisionMemoryUI;

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
    }

    void Update()
    {
        switch(situation)
        {
            case SituationType.None:
                return;
            default:
                DragPreview();
                break;
        }
    }

    //プレビューをドラッグ
    void DragPreview()
    {
        FollowMouse();

        if (Input.GetKey(KeyCode.Mouse0)) return;

        //マウス左クリックしていない時
        switch (situation)
        {
            case SituationType.Drag_Preview:
                Init();
                break;
            default:
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

    /*******************************
    * public
    *******************************/

    public void DispatchDrag()
    {
        situation = SituationType.Drag_Preview;
    }

    public void EndPreview()
    {
        Init();
    }

    //getter
    public SituationType GetSituation()
    {
        //マウス左クリックを押していたらSituationType.Drag_Previewを返す
        if (Input.GetKey(KeyCode.Mouse0))
        {
            return SituationType.Drag_Preview;
        }
        
        return situation;
    }

    public ref MemoryUI GetCollisionUI()
    {
        return ref collisionMemoryUI;
    }
    
    //setter
    public void SetMoveType(MemorySelectManager.MoveType type)
    {
        moveType = type;
    }

    //メモリUIに当たっている時の処理
    public void PointerEnterToMemoryUI(GameObject collision)
    {
        if (situation == SituationType.None) return;

        //合成フレーム
        if (collision.tag == "EuipmentMemory" || collision.tag == "CombineMemory")
        {
            //合成フレーム
            switch (moveType)
            {
                case MemorySelectManager.MoveType.Set:
                    situation = SituationType.Set_Memory;

                    break;
                case MemorySelectManager.MoveType.Replace:
                    situation = SituationType.Replace_Memory;
                    break;
            }
        }
        //衝突相手を格納
        collisionMemoryUI = collision.GetComponent<MemoryUI>();

        if (moveType == MemorySelectManager.MoveType.Replace && collision.tag == "Inventory")
        {
            situation = SituationType.Remove_Memory;
        }
    }

    public void PointerExitToMemoryUI(GameObject collision)
    {
        if (situation == SituationType.None) return;

        situation = SituationType.Drag_Preview;
        collisionMemoryUI = null;
    }
}
