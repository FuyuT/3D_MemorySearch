using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagecolorChange : MonoBehaviour
{
    //
    [SerializeField]
    GameObject ControlScript;

    //動かせないオブジェクト
    [SerializeField]
    GameObject NotMoveObject;

    //動かせれるオブジェクト
    [SerializeField]
    GameObject MoveObject;

    //動かせないオブジェクトのマテリアル
    Transform[] NotObjectChildren;

    //動かせれるオブジェクトのマテリアル
    Transform[] MoveObjectChildren; 


    //ControlCameraのスクリプトを取得
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

            //動かせれない空オブジェクトの子オブジェクトを取得
           for(int i=0 ; i < NotMoveObject.transform.childCount;i++)
           {
                NotObjectChildren[i] = NotMoveObject.transform.GetChild(i);
                NotObjectChildren[i].GetComponent<MeshRenderer>().material = ChangeMaterial;
            }

            //動かせれる空オブジェクトの子オブジェクトを取得
           for (int i = 0; i < MoveObject.transform.childCount; i++)
           {
                MoveObjectChildren[i] = MoveObject.transform.GetChild(i);
                MoveObjectChildren[i].GetComponent<MeshRenderer>().material = ChangeMoveMaterial;
            }

            
        }
    }
}
