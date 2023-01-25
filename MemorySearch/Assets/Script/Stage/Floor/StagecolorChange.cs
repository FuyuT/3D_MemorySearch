using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagecolorChange : MonoBehaviour
{
    //�������Ȃ��I�u�W�F�N�g
    [SerializeField]
    GameObject NotMoveObject;

    //���������I�u�W�F�N�g
    [SerializeField]
    GameObject MoveObject;

    public Material ChangeNotMoveAfterMaterial;
    public Material ChangeMoveAfterMaterial;

    //�������Ȃ��I�u�W�F�N�g�̌��}�e���A��
    Material[] NotMoveObjectChildrenBeforeMaterial;

    //���������I�u�W�F�N�g�̌��}�e���A��
    Material[] MoveObjectChildrenBeforeMaterial;


    // Start is called before the first frame update
    void Awake()
    {
        //�v�f���������m��
        NotMoveObjectChildrenBeforeMaterial = new Material[NotMoveObject.transform.childCount];
        MoveObjectChildrenBeforeMaterial = new Material[MoveObject.transform.childCount];
        //�q�I�u�W�F�N�g�����Ă�����
        for (int i = 0; i < NotMoveObject.transform.childCount; i++)
        {
            NotMoveObjectChildrenBeforeMaterial[i] = NotMoveObject.transform.GetChild(i).GetComponent<MeshRenderer>().material;
        }
        for (int i = 0; i < MoveObject.transform.childCount; i++)
        {
            MoveObjectChildrenBeforeMaterial[i] = MoveObject.transform.GetChild(i).GetComponent<MeshRenderer>().material;
        }
    }

    public void ChangeAfterMaterial()
    {
       
        //��������Ȃ��q�I�u�W�F�N�g�̃}�e���A����ύX
        for (int i = 0; i < NotMoveObject.transform.childCount; i++)
        {
            NotMoveObject.transform.GetChild(i).GetComponent<MeshRenderer>().material = ChangeNotMoveAfterMaterial;
        }

        //���������q�I�u�W�F�N�g�̃}�e���A����ύX
        for (int i = 0; i < MoveObject.transform.childCount; i++)
        {
            MoveObject.transform.GetChild(i).GetComponent<MeshRenderer>().material = ChangeMoveAfterMaterial;
        }
    }

    public void ChangeBeforeMaterial()
    {
        //��������Ȃ��q�I�u�W�F�N�g�̃}�e���A�������ɖ߂�
        for (int i = 0; i < NotMoveObject.transform.childCount; i++)
        {
            NotMoveObject.transform.GetChild(i).GetComponent<MeshRenderer>().material = NotMoveObjectChildrenBeforeMaterial[i];
        }

        //���������q�I�u�W�F�N�g�̃}�e���A�������ɖ߂�
        for (int i = 0; i < MoveObject.transform.childCount; i++)
        {
            MoveObject.transform.GetChild(i).GetComponent<MeshRenderer>().material = MoveObjectChildrenBeforeMaterial[i];
        }
    }
}

