using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextShow : MonoBehaviour
{

    //�e�L�X�g
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

    //USB�ɋ߂Â��ƕ\��
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
           
            textOn.SetActive(true);
           
        }


    }

    //USB���痣���Ɣ�\��
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            textOn.SetActive(false);
           
        }
    }
}
