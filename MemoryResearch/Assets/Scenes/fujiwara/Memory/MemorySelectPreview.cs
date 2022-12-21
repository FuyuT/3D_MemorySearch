using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemorySelectPreview : MemoryUI
{
    //////////////////////////////
    /// private
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
        dataManager = GameObject.FindWithTag("DataManager").GetComponent<DataManager>();

        Init();
    }

    private void Init()
    {
        situation = SituationType.None;
        memoryType = MemoryType.None;
        //�ʒu����ʊO��
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

    //�v���r���[���h���b�O
    void DragPreview()
    {
        FollowMouse();

        if (Input.GetKey(KeyCode.Mouse0)) return;

        //�}�E�X���N���b�N���Ă��Ȃ���
        switch (situation)
        {
            case SituationType.Drag_Preview:
                Init();
                break;
            default:
                break;
        }
    }

    //�}�E�X�ɒǏ]����
    void FollowMouse()
    {
        //�}�E�X�ɒǏ]����
        Vector3 mouse = Input.mousePosition;
        this.transform.position = new Vector3(mouse.x, mouse.y, 0);
    }


    //////////////////////////////
    /// public
       
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
        //�}�E�X���N���b�N�������Ă�����SituationType.Drag_Preview��Ԃ�
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

    //////////////////////////////
    /// �Փ˔���
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (situation == SituationType.None) return;

        //�����t���[��
        if (collision.tag == "EuipmentMemory" || collision.tag == "CombineMemory")
        {
            switch (moveType)
            {
                case MemorySelectManager.MoveType.Set:
                    situation = SituationType.Set_Memory;
                    break;
                case MemorySelectManager.MoveType.Replace:
                    situation = SituationType.Replace_Memory;
                    break;
            }

            //�Փˑ�����i�[
            collisionMemoryUI = collision.GetComponent<MemoryUI>();
        }

        if (moveType == MemorySelectManager.MoveType.Replace && collision.tag == "Inventory")
        {
            situation = SituationType.Remove_Memory;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (situation == SituationType.None) return;

        //�����t���[��
        if (collision.tag == "EuipmentMemory" || collision.tag == "CombineMemory" || collision.tag == "Inventory")
        {
            situation = SituationType.Drag_Preview;
            collisionMemoryUI = null;
        }
    }
}
