using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextShow : MonoBehaviour
{

    //�e�L�X�g
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

    //USB�ɋ߂Â��ƕ\��
    private void OnTriggerEnter(Collider other)//�G���x�[�^�[�̒��ɓ�������
    {
      

        if (other.gameObject.tag == "Player")
        {
           
            textOn.SetActive(true);

            bottun.SetActive(true);
           
        }


    }

    //USB���痣���Ɣ�\��
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            textOn.SetActive(false);
            bottun.SetActive(false);
        }
    }
}
