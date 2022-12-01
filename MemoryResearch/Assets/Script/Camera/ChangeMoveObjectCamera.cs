using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomInputKey;


public class ChangeMoveObjectCamera : MonoBehaviour
{
    //プレイヤー格納用
    [SerializeField] GameObject Player;

    //表示するテキスト格納用
    [SerializeField] GameObject ShowText;

    //カメラ変更フラグ      
    public bool ChangFlg;

    public CollisionObject colObj { get; private set; }

    public enum CollisionObject
    {
        None,
        Player,
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangFlg = false;
        colObj = CollisionObject.None;
    }

    // Update is called once per frame
    void Update()
    { 
        //if (ChangFlg = true)
        //{
           
        //}
        //}

        //if (CustomInput.Interval_InputKeydown(KeyCode.V, 2))
        //{

        //    //メインカメラをアクティブに設定
        //    MoveObjectCamera.SetActive(false);
        //    MainCamera.SetActive(true);
        //    ChangFlg = false;

        //}

    }

    //装置に近づくと表示
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShowText.SetActive(true);
            colObj = CollisionObject.Player;
            ChangFlg = true;     
            if (Input.GetKeyDown("v"))
            {
                ChangFlg = true;
                Debug.Log("通った");
            }
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
          
            colObj = CollisionObject.None;
            ChangFlg = false;
        }
        colObj = CollisionObject.None;
    }
}
