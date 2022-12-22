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

    //FPS当たり判定格納用 
    [SerializeField] GameObject SearchCollisionRange;

    //カメラ変更フラグ      
    public bool ChangFlg;

    void Start()
    {
        ChangFlg = false;

        SearchCollisionRange.SetActive(false);
    }

    void Update()
    {
        

    }


    void FixedUpdate()
    {
        //スペースキーが押すたびに、カメラを切り替える
        if (Input.GetKeyDown("space"))
        {
            if (MainCamera.activeSelf)
            {
                //サブカメラをアクティブに設定
                MainCamera.SetActive(false);
                ChapterCamera.SetActive(true);
                SearchCollisionRange.SetActive(true);
                ChangFlg = true;

                //プレイヤーの角度に合わせる
                ChapterCamera.transform.rotation = player.transform.rotation;
            }
            else
            {
                //メインカメラをアクティブに設定
                ChapterCamera.SetActive(false);
                MainCamera.SetActive(true);
                SearchCollisionRange.SetActive(false);
                ChangFlg = false;
            }
        }
    }
}