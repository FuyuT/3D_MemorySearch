using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    [SerializeField] GameObject player;

    //メインカメラ格納用
    [SerializeField] GameObject MainCamera;

    //FPSカメラ格納用 
    [SerializeField] GameObject ChapterCamera;

    //カメラ変更フラグ      
    public bool ChangFlg;

    // Start is called before the first frame update
    void Start()
    {
        ChangFlg = false;

        //サブカメラを非アクティブにする
        ChapterCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //スペースキーが押すたびに、カメラを切り替える
        if (Input.GetKeyDown("space"))
        {
            if (MainCamera.activeSelf)
            {
                //サブカメラをアクティブに設定
                MainCamera.SetActive(false);
                ChapterCamera.SetActive(true);
                ChangFlg = true;

                //プレイヤーの角度に合わせる
                ChapterCamera.transform.rotation = player.transform.rotation;
            }
            else
            {
                //メインカメラをアクティブに設定
                ChapterCamera.SetActive(false);
                MainCamera.SetActive(true);
                ChangFlg = false;
            }
        }

    }


    void FixedUpdate()
    {

    }
}