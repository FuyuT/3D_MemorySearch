using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeMoveObjectCamera : MonoBehaviour
{
    //�v���C���[�i�[�p
    [SerializeField] GameObject Player;

    //���C���J�����i�[�p
    [SerializeField] GameObject MainCamera;

    //FPS�J�����i�[�p 
    [SerializeField] GameObject MoveObjectCamera;

    //�\������e�L�X�g�i�[�p
    [SerializeField] GameObject ShowText;

    //�J�����ύX�t���O      
    public bool ChangFlg;

    int s;

    // Start is called before the first frame update
    void Start()
    {
        ChangFlg = false;

        //�T�u�J�������A�N�e�B�u�ɂ���
        MoveObjectCamera.SetActive(false);

        s = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //�X�y�[�X�L�[���������тɁA�J������؂�ւ���
        if (s == 1)
        {
            if (Input.GetKeyDown("c"))
            {
                if (MainCamera.activeSelf)
                {
                    //�T�u�J�������A�N�e�B�u�ɐݒ�
                    MainCamera.SetActive(false);
                    MoveObjectCamera.SetActive(true);
                    ChangFlg = true;
                    
                }
                else
                {
                    //���C���J�������A�N�e�B�u�ɐݒ�
                    MoveObjectCamera.SetActive(false);
                    MainCamera.SetActive(true);
                    ChangFlg = false;
                }
            }
        }
    }

    //���u�ɋ߂Â��ƕ\��
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Player")
        {
            ShowText.SetActive(true);
            s = 1;
        }


    }

    //���u���痣���Ɣ�\��
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ShowText.SetActive(false);
            s = 0;
        }
    }
}
