using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagecolorChange : MonoBehaviour
{
    //
    [SerializeField]
    GameObject ControlScript;

    //�������Ȃ��I�u�W�F�N�g
    [SerializeField]
    GameObject NotMoveObject;

    //���������I�u�W�F�N�g
    [SerializeField]
    GameObject MoveObject;

    //�������Ȃ��I�u�W�F�N�g�̃}�e���A��
    Transform[] NotObjectChildren;

    //���������I�u�W�F�N�g�̃}�e���A��
    Transform[] MoveObjectChildren; 


    //ControlCamera�̃X�N���v�g���擾
    ControlCamera Script;

    public Material ChangeMaterial ;
    public Material ChangeMoveMaterial;

    // Start is called before the first frame update
    void Start()
    {
        Script = ControlScript.GetComponent<ControlCamera>();

        NotObjectChildren = new Transform[NotMoveObject.transform.childCount];
        MoveObjectChildren = new Transform[MoveObject.transform.childCount];
    }

    // Update is called once per frame
    void Update()
    {
        if(Script.MaterialChange==true)
        {

            //��������Ȃ���I�u�W�F�N�g�̎q�I�u�W�F�N�g���擾
           for(int i=0 ; i < NotMoveObject.transform.childCount;i++)
           {
                NotObjectChildren[i] = NotMoveObject.transform.GetChild(i);
                NotObjectChildren[i].GetComponent<MeshRenderer>().material = ChangeMaterial;
            }

            //����������I�u�W�F�N�g�̎q�I�u�W�F�N�g���擾
           for (int i = 0; i < MoveObject.transform.childCount; i++)
           {
                MoveObjectChildren[i] = MoveObject.transform.GetChild(i);
                MoveObjectChildren[i].GetComponent<MeshRenderer>().material = ChangeMoveMaterial;
            }

            
        }
    }
}
