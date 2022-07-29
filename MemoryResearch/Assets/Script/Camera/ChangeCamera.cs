using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{

    private GameObject MainCamera;      //���C���J�����i�[�p
    private GameObject ChapterCamera;   //FPS�J�����i�[�p 
    bool               ChangFlg;        //�J�����ύX�t���O       

    // Start is called before the first frame update
    void Start()
    {
        MainCamera    = GameObject.Find("Main Camera");
        ChapterCamera = GameObject.Find("ChapterCamera");
        ChangFlg      =false;

        //�T�u�J�������A�N�e�B�u�ɂ���
        ChapterCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ////�X�y�[�X�L�[���������тɁA�J������؂�ւ���
        if (Input.GetKeyDown("space"))
        {
            if (MainCamera.activeSelf)
            {
                //�T�u�J�������A�N�e�B�u�ɐݒ�
                MainCamera.SetActive(false);
                ChapterCamera.SetActive(true);

            }
            else
            {
                //���C���J�������A�N�e�B�u�ɐݒ�
                ChapterCamera.SetActive(false);
                MainCamera.SetActive(true);
            }
        }

    }


    void FixedUpdate()
    {
      
    }
}
