using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeMoveObjectCamera : MonoBehaviour
{
    //プレイヤー格納用
    [SerializeField] GameObject Player;

    //メインカメラ格納用
    [SerializeField] GameObject MainCamera;

    //FPSカメラ格納用 
    [SerializeField] GameObject MoveObjectCamera;

    //表示するテキスト格納用
    [SerializeField] GameObject ShowText;

    //カメラ変更フラグ      
    public bool ChangFlg;

    int s;

    // Start is called before the first frame update
    void Start()
    {
        ChangFlg = false;

        //サブカメラを非アクティブにする
        MoveObjectCamera.SetActive(false);

        s = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //スペースキーが押すたびに、カメラを切り替える
        if (s == 1)
        {
            if (Input.GetKeyDown("c"))
            {
                if (MainCamera.activeSelf)
                {
                    //サブカメラをアクティブに設定
                    MainCamera.SetActive(false);
                    MoveObjectCamera.SetActive(true);
                    ChangFlg = true;
                    
                }
                else
                {
                    //メインカメラをアクティブに設定
                    MoveObjectCamera.SetActive(false);
                    MainCamera.SetActive(true);
                    ChangFlg = false;
                }
            }
        }
    }

    //装置に近づくと表示
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Player")
        {
            ShowText.SetActive(true);
            s = 1;
        }


    }

    //装置から離れると非表示
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ShowText.SetActive(false);
            s = 0;
        }
    }
}
