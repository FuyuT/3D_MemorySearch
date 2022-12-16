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
        //�ʒu����ʊO��
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


    //�}�e���A�����^��
    void DragMaterial()
    {
        FollowMouse();

        if (Input.GetKey(KeyCode.Mouse0)) return;

        //�}�E�X���N���b�N���Ă��Ȃ���
        switch (situation)
        {
            //�ݒ蒆�Ȃ�Ȃɂ����Ȃ�
            case SituationType.Set_Memory:
            case SituationType.Replace_Memory:
                break;
            //�ݒ蒆�łȂ���Ώ�����
            default:
                Init();
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
        //�}�E�X���N���b�N�������Ă�����SituationType.Drag_Preview��Ԃ�
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
    /// �Փ˔���
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (situation == SituationType.None) return;

        //�����t���[��
        if (collision.tag == "EuipmentMemory" || collision.tag == "CombineMemory")
        {
            //�I�����Ă��郁�������A���łɃZ�b�g����Ă�����̂Ȃ�Γ���ւ���Ԃɂ���
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

        //�����t���[��
        if (collision.tag == "CombineMemory")
        {
            situation = SituationType.Drag_Preview;
        }
    }
}
