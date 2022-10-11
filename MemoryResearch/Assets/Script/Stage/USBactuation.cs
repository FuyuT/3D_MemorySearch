using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class USBactuation : MonoBehaviour
{

    //�v���C���[
    private GameObject chara;
    //�e�L�X�g
    private GameObject textOn;
    //����������
    public GameObject Wal;
    //�\���������I�u�W�F�N�g
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

    //USB�ɋ߂Â��ƕ\��
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            textOn.SetActive(true);
            s = 1;
        }


    }

    //USB���痣���Ɣ�\��
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            textOn.SetActive(false);
            s = 0;
        }
    }

}

