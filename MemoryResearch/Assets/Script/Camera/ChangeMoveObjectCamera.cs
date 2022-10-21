using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomInputKey;


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
       
        if (s == 1)
        {
            if (Input.GetKeyDown("v"))
            {
                if (MainCamera.activeSelf)
                {
                    //サブカメラをアクティブに設定
                    MainCamera.SetActive(false);
                    MoveObjectCamera.SetActive(true);
                    ChangFlg = true;
                    
                }
               
            }
        }

        if (CustomInput.Interval_InputKeydown(KeyCode.V, 2))
        {

            //メインカメラをアクティブに設定
            MoveObjectCamera.SetActive(false);
            MainCamera.SetActive(true);
            ChangFlg = false;

        }

    }

    //装置に近づくと表示
    private void OnCollisionEnter(Collision other)
    {
        if (s == 0)
        {
            if (other.gameObject.tag == "Player")
            {
                ShowText.SetActive(true);
                s = 1;
            }
        }

    }
}
