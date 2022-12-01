using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextShow : MonoBehaviour
{

    //テキスト
    [SerializeField]
    GameObject textOn;
  

    // Start is called before the first frame update
    void Start()
    {
        textOn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //USBに近づくと表示
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
           
            textOn.SetActive(true);
           
        }


    }

    //USBから離れると非表示
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            textOn.SetActive(false);
           
        }
    }
}
