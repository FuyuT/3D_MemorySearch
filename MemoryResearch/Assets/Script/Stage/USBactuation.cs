using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class USBactuation : MonoBehaviour
{

    //プレイヤー
    private GameObject chara;
    //テキスト
    private GameObject textOn;
    //消したい壁
    public GameObject Wal;
    //表示したいオブジェクト
    public GameObject ShowObj;

    public int n;

    int s;

    // Start is called before the first frame update
    void Start()
    {
        chara = GameObject.Find("Player");
        textOn = GameObject.Find("ButtonText");

        Wal.SetActive(true);
        if (ShowObj != null)
        {
            ShowObj.SetActive(false);
        }
        textOn.SetActive(false);

        s = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
            if (s == 1)
            {
                if (Input.GetKeyDown("c"))
                {
                   if (n == 1)
                   {
                       //Debug.Log("a");
                        Wal.SetActive(false);
                        ShowObj.SetActive(true);
                        Destroy(this.textOn);
                   }
                   else if (n == 2)
                    {
                        Wal.SetActive(false);
                        ShowObj = null;
                    }
                }
            }
    }

    //USBに近づくと表示
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            textOn.SetActive(true);
            s = 1;
        }


    }

    //USBから離れると非表示
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            textOn.SetActive(false);
            s = 0;
        }
    }

}

