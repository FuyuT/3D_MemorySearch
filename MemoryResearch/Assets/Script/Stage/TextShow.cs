using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextShow : MonoBehaviour
{

    //テキスト
    [SerializeField]
    GameObject textOn;

    [SerializeField]
    GameObject bottun;


    // Start is called before the first frame update
    void Start()
    {
        textOn.SetActive(false);
        bottun.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //USBに近づくと表示
    private void OnTriggerEnter(Collider other)//エレベーターの中に入ったら
    {
      

        if (other.gameObject.tag == "Player")
        {
           
            textOn.SetActive(true);

            bottun.SetActive(true);
           
        }


    }

    //USBから離れると非表示
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            textOn.SetActive(false);
            bottun.SetActive(false);
        }
    }
}
