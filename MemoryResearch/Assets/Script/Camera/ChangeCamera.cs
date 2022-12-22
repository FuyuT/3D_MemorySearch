using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    [SerializeField] GameObject player;

    //���C���J�����i�[�p
    [SerializeField] GameObject MainCamera;

    //FPS�J�����i�[�p 
    [SerializeField] GameObject ChapterCamera;

    //FPS�����蔻��i�[�p 
    [SerializeField] GameObject FPSVisbility;

    //�J�����ύX�t���O      
    public bool ChangFlg;

    // Start is called before the first frame update
    void Start()
    {
        ChangFlg = false;

        //�T�u�J�������A�N�e�B�u�ɂ���
        ChapterCamera.SetActive(false);

        FPSVisbility.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        

    }


    void FixedUpdate()
    {
        //�X�y�[�X�L�[���������тɁA�J������؂�ւ���
        if (Input.GetKeyDown("space"))
        {
            if (MainCamera.activeSelf)
            {
                //�T�u�J�������A�N�e�B�u�ɐݒ�
                MainCamera.SetActive(false);
                ChapterCamera.SetActive(true);
                FPSVisbility.SetActive(true);
                ChangFlg = true;

                //�v���C���[�̊p�x�ɍ��킹��
                ChapterCamera.transform.rotation = player.transform.rotation;
            }
            else
            {
                //���C���J�������A�N�e�B�u�ɐݒ�
                ChapterCamera.SetActive(false);
                MainCamera.SetActive(true);
                FPSVisbility.SetActive(false);
                ChangFlg = false;
            }
        }
    }
}