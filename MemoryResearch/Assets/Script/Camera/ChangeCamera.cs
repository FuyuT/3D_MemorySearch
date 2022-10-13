using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{

    private GameObject MainCamera;      //メインカメラ格納用
    private GameObject ChapterCamera;   //FPSカメラ格納用 
    bool               ChangFlg;        //カメラ変更フラグ       

    // Start is called before the first frame update
    void Start()
    {
        MainCamera    = GameObject.Find("Main Camera");
        ChapterCamera = GameObject.Find("ChapterCamera");
        ChangFlg      =false;

        //サブカメラを非アクティブにする
        ChapterCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ////スペースキーが押すたびに、カメラを切り替える
        if (Input.GetKeyDown("space"))
        {
            if (MainCamera.activeSelf)
            {
                //サブカメラをアクティブに設定
                MainCamera.SetActive(false);
                ChapterCamera.SetActive(true);

            }
            else
            {
                //メインカメラをアクティブに設定
                ChapterCamera.SetActive(false);
                MainCamera.SetActive(true);
            }
        }

    }


    void FixedUpdate()
    {
      
    }
}
