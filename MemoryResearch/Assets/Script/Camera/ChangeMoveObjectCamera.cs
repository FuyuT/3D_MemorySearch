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

    //�͈͂ɓ�������
    public bool RangeInFlg;

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
        RangeInFlg = false;
        colObj = CollisionObject.None;
    }

    // Update is called once per frame
    void Update()
    { 

    }

    //���u�ɋ߂Â��ƕ\��
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShowText.SetActive(true);
            colObj = CollisionObject.Player;
            RangeInFlg = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShowText.SetActive(false);
            colObj = CollisionObject.None;
            RangeInFlg = false;
        }
        colObj = CollisionObject.None;
    }
}
