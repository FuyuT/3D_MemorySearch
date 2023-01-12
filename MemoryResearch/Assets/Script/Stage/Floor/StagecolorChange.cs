using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagecolorChange : MonoBehaviour
{
    //動かせないオブジェクト
    [SerializeField]
    GameObject NotMoveObject;

    //動かせれるオブジェクト
    [SerializeField]
    GameObject MoveObject;

    public Material ChangeNotMoveAfterMaterial;
    public Material ChangeMoveAfterMaterial;

    //動かせないオブジェクトの元マテリアル
    Material[] NotMoveObjectChildrenBeforeMaterial;

    //動かせれるオブジェクトの元マテリアル
    Material[] MoveObjectChildrenBeforeMaterial;


    // Start is called before the first frame update
    void Awake()
    {
        //要素分メモリ確保
        NotMoveObjectChildrenBeforeMaterial = new Material[NotMoveObject.transform.childCount];
        MoveObjectChildrenBeforeMaterial = new Material[MoveObject.transform.childCount];
        //子オブジェクトを入れてあげる
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
       
        //動かせれない子オブジェクトのマテリアルを変更
        for (int i = 0; i < NotMoveObject.transform.childCount; i++)
        {
            NotMoveObject.transform.GetChild(i).GetComponent<MeshRenderer>().material = ChangeNotMoveAfterMaterial;
        }

        //動かせれる子オブジェクトのマテリアルを変更
        for (int i = 0; i < MoveObject.transform.childCount; i++)
        {
            MoveObject.transform.GetChild(i).GetComponent<MeshRenderer>().material = ChangeMoveAfterMaterial;
        }
    }

    public void ChangeBeforeMaterial()
    {
        //動かせれない子オブジェクトのマテリアルを元に戻す
        for (int i = 0; i < NotMoveObject.transform.childCount; i++)
        {
            NotMoveObject.transform.GetChild(i).GetComponent<MeshRenderer>().material = NotMoveObjectChildrenBeforeMaterial[i];
        }

        //動かせれる子オブジェクトのマテリアルを元に戻す
        for (int i = 0; i < MoveObject.transform.childCount; i++)
        {
            MoveObject.transform.GetChild(i).GetComponent<MeshRenderer>().material = MoveObjectChildrenBeforeMaterial[i];
        }
    }
}

