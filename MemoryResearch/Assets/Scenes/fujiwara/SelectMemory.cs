using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMemory : MonoBehaviour
{
    //////////////////////////////
    /// private
    //////////////////////////////

    enum SituationType
    {
        None,
        Drag_Material,
        Collision_Material,
        Set_Material,
        Set_Material_End,
    }
    SituationType situation;

    MemoryTypeEnum memoryType;

    void Awake()
    {
        situation = SituationType.None;
        memoryType = MemoryTypeEnum.None;
    }

    private void Init()
    {
        situation = SituationType.None;
        //位置を画面外へ
        transform.position = new Vector3(-100, -100, 0);
    }

    void Update()
    {
        //situationがNoneまたはSet_Material_Endなら終了
        switch(situation)
        {
            case SituationType.None:
                return;
            case SituationType.Set_Material_End:
                Init();
                return;
            default:
                break;
        }

        FollowMouse();

        DragMaterial();
    }

    //マウスに追従する
    void FollowMouse()
    {
        //マウスに追従する
        Vector3 mouse = Input.mousePosition;
        this.transform.position = new Vector3(mouse.x, mouse.y, 0);
    }

    //マテリアルを運ぶ
    void DragMaterial()
    {
        //マウス左クリックを離していなければ終了
        if (!Input.GetKeyUp(KeyCode.Mouse0)) return;
        switch (situation)
        {
            //素材設定フレームに当たっていたら素材セット状態にする
            case SituationType.Collision_Material:
                situation = SituationType.Set_Material;
                break;
            case SituationType.Set_Material_End:
                break;
            default:
                Init();
                break;
        }
    }


    //////////////////////////////
    /// public
    //////////////////////////////

    public void SetSituationDragMaterial()
    {
        situation = SituationType.Drag_Material;
    }

    public void SetMaterialEnd()
    {
        situation = SituationType.Set_Material_End;
    }

    //素材を渡す状況か判断
    public bool isSetMaterial()
    {
        if (situation != SituationType.Set_Material)
        {
            return false;
        }
        return true;
    }

    public MemoryTypeEnum GetMemoryType()
    {
        return memoryType;
    }

    public void SetMemoryType(MemoryTypeEnum memoryType)
    {
        this.memoryType = memoryType;
    }

    //////////////////////////////
    /// 衝突判定
    //////////////////////////////
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (situation == SituationType.None) return;

        //合成フレーム
        if (collision.tag == "CombineMaterial")
        {
            situation = SituationType.Collision_Material;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (situation == SituationType.None) return;

        //合成フレーム
        if (collision.tag == "CombineMaterial")
        {
            situation = SituationType.Drag_Material;
        }
    }
}
