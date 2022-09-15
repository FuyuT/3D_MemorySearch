using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{

    //���C���J�����i�[�p
    [SerializeField] GameObject MainCamera;

    //FPS�J�����i�[�p 
    [SerializeField] GameObject ChapterCamera;

    //�J�����ύX�t���O      
    public bool ChangFlg;

    // Start is called before the first frame update
    void Start()
    {
        ChangFlg =false;

        //�T�u�J�������A�N�e�B�u�ɂ���
        ChapterCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //�X�y�[�X�L�[���������тɁA�J������؂�ւ���
        if (Input.GetKeyDown("space"))
        {
            if (MainCamera.activeSelf)
            {
                //�T�u�J�������A�N�e�B�u�ɐݒ�
                MainCamera.SetActive(false);
                ChapterCamera.SetActive(true);
                ChangFlg = true;
            }
            else
            {
                //���C���J�������A�N�e�B�u�ɐݒ�
                ChapterCamera.SetActive(false);
                MainCamera.SetActive(true);
                ChangFlg = false;
            }
        }

    }


    void FixedUpdate()
    {
      
    }
}
