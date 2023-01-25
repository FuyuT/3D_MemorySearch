using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorySelectManager : MonoBehaviour
{
    //////////////////////////////
    /// private
    [SerializeField] MemorySelectPreview preview;
    [SerializeField] MemoryExplanation   explanation;
    MemoryUI selectMemory;

    void Update()
    {
        if (preview.GetSituation() == MemorySelectPreview.SituationType.None) return;

        MoveMemory();    
    }

    void MoveMemory()
    {
        switch (preview.GetSituation())
        {
            //メモリのセット
            case MemorySelectPreview.SituationType.Set_Memory:
                preview.GetCollisionUI().ChangeMemoryType(selectMemory.GetMemoryType());
                break;
            //メモリの入れ替え
            case MemorySelectPreview.SituationType.Replace_Memory:
                MemoryType temp = preview.GetCollisionUI().GetMemoryType();
                preview.GetCollisionUI().ChangeMemoryType(selectMemory.GetMemoryType());
                selectMemory.ChangeMemoryType(temp);
                break;
            //メモリを削除
            case MemorySelectPreview.SituationType.Remove_Memory:
                selectMemory.ChangeMemoryType(MemoryType.None);
                break;       
            default:
                return;
        }
        preview.EndPreview();
    }

    //////////////////////////////
    /// public
    public enum MoveType
    {
        None,
        Set,
        Replace,
    }

    //メモリのUIボタンを選択した際（押した際）に呼び出される処理
    //メモリの選択
    public void SelectMemory(MemoryUI memoryUi)
    {
        //メモリの種類が設定されていないなら終了
        if (memoryUi.IsTypeNone()) return;

        //選択したUiの参照を格納
        selectMemory = memoryUi;

        //説明文を変更
        explanation.ChangeExplanation(memoryUi.GetMemoryType());

        //プレビュー設定
        //メモリの種類を設定
        preview.ChangeMemoryType(memoryUi.GetMemoryType());

        //メモリをセットするのか、入れ替えるのか設定する
        preview.SetMoveType(memoryUi.GetMoveType());

        //ドラッグ状態に移行する
        preview.GetComponent<MemorySelectPreview>().DispatchDrag();
    }

    //メモリの削除
    public void RemoveMemory(MemoryUI memoryUi)
    {
        //右クリックしていなければ終了
        if (!Input.GetKey(KeyCode.Mouse1)) return;

        memoryUi.ChangeMemoryType(MemoryType.None);
    }
}
