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
        //�ʒu����ʊO��
        transform.position = new Vector3(-100, -100, 0);
    }

    void Update()
    {
        //situation��None�܂���Set_Material_End�Ȃ�I��
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

    //�}�E�X�ɒǏ]����
    void FollowMouse()
    {
        //�}�E�X�ɒǏ]����
        Vector3 mouse = Input.mousePosition;
        this.transform.position = new Vector3(mouse.x, mouse.y, 0);
    }

    //�}�e���A�����^��
    void DragMaterial()
    {
        //�}�E�X���N���b�N�𗣂��Ă��Ȃ���ΏI��
        if (!Input.GetKeyUp(KeyCode.Mouse0)) return;
        switch (situation)
        {
            //�f�ސݒ�t���[���ɓ������Ă�����f�ރZ�b�g��Ԃɂ���
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

    //�f�ނ�n���󋵂����f
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
    /// �Փ˔���
    //////////////////////////////
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (situation == SituationType.None) return;

        //�����t���[��
        if (collision.tag == "CombineMaterial")
        {
            situation = SituationType.Collision_Material;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (situation == SituationType.None) return;

        //�����t���[��
        if (collision.tag == "CombineMaterial")
        {
            situation = SituationType.Drag_Material;
        }
    }
}
