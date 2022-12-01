using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomInputKey;


public class ChangeMoveObjectCamera : MonoBehaviour
{
    //�v���C���[�i�[�p
    [SerializeField] GameObject Player;

    //�\������e�L�X�g�i�[�p
    [SerializeField] GameObject ShowText;

    //�J�����ύX�t���O      
    public bool ChangFlg;

    public CollisionObject colObj { get; private set; }

    public enum CollisionObject
    {
        None,
        Player,
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangFlg = false;
        colObj = CollisionObject.None;
    }

    // Update is called once per frame
    void Update()
    { 
        //if (ChangFlg = true)
        //{
           
        //}
        //}

        //if (CustomInput.Interval_InputKeydown(KeyCode.V, 2))
        //{

        //    //���C���J�������A�N�e�B�u�ɐݒ�
        //    MoveObjectCamera.SetActive(false);
        //    MainCamera.SetActive(true);
        //    ChangFlg = false;

        //}

    }

    //���u�ɋ߂Â��ƕ\��
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShowText.SetActive(true);
            colObj = CollisionObject.Player;
            ChangFlg = true;     
            if (Input.GetKeyDown("v"))
            {
                ChangFlg = true;
                Debug.Log("�ʂ���");
            }
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
          
            colObj = CollisionObject.None;
            ChangFlg = false;
        }
        colObj = CollisionObject.None;
    }
}
