using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomInputKey;


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
       
        if (s == 1)
        {
            if (Input.GetKeyDown("v"))
            {
                if (MainCamera.activeSelf)
                {
                    //�T�u�J�������A�N�e�B�u�ɐݒ�
                    MainCamera.SetActive(false);
                    MoveObjectCamera.SetActive(true);
                    ChangFlg = true;
                    
                }
               
            }
        }

        if (CustomInput.Interval_InputKeydown(KeyCode.V, 2))
        {

            //���C���J�������A�N�e�B�u�ɐݒ�
            MoveObjectCamera.SetActive(false);
            MainCamera.SetActive(true);
            ChangFlg = false;

        }

    }

    //���u�ɋ߂Â��ƕ\��
    private void OnCollisionEnter(Collision other)
    {
        if (s == 0)
        {
            if (other.gameObject.tag == "Player")
            {
                ShowText.SetActive(true);
                s = 1;
            }
        }

    }
}
