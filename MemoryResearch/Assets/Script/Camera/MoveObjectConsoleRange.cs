using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomInputKey;


/// <summary>
/// �I�u�W�F�N�g�𓮂�����J�������N��������
/// �R���\�[���̓����蔻��ɓ����Ă��邩�����m����
/// </summary>
public class MoveObjectConsoleRange : MonoBehaviour
{
    //�ē�UI
    [SerializeField]
    GameObject GuideUI;

    //��\��UI��///////////////////////
    [SerializeField]
    GameObject GameUI;

    /////////////////////////////////////////

    //�_�bUI
    [SerializeField]
    GameObject SinzyuUi;

    [SerializeField]
    GameObject ReturnUi;

    //�t�F�[�h�p
    [SerializeField]
    Animator FadeAnimator;

    SinzyuUI sinzyu;

    //�͈͂ɓ�������
    public bool InRange;

    bool a;

    void Awake()
    {
    }

    void Start()
    {
        InRange = false;
        GuideUI.SetActive(false);
        ReturnUi.SetActive(false);
        a = false;
    }

 
    void FixedUpdate()
    {
        //�_�b�A�j���[�V�����X�^�[�g
        if (SinzyuUi.activeSelf) return;
       

        if (Input.GetKeyDown("r") && InRange)
        {
            switch(CameraManager.instance.GetCurrentCameraType())
            {
                case (int)CameraManager.CameraType.Controller:
                    //�R���g���[���[�J��������TPS�ɖ߂�
                    CameraManager.instance.ToTpsCamera();
                    GuideUI.SetActive(true);
                    GameUI.SetActive(true);
                    ReturnUi.SetActive(false);
                    break;
                default:
                    //�R���g���[���[�J�����ɑJ�ڂ���ׂ̏���
                    a = true;
                    BehaviorAnimation.UpdateTrigger(ref FadeAnimator, "FadeOut");
                    GuideUI.SetActive(false);
                    GameUI.SetActive(false);
                    break;
            }
        }

        if (!a) return;

        if (BehaviorAnimation.IsPlayEnd(ref FadeAnimator, "FadeOut"))
        {
            SinzyuUi.SetActive(true);
            a = false;
        }
    }

    //���u�ɋ߂Â��ƕ\��
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GuideUI.SetActive(true);
            InRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GuideUI.SetActive(false);
            InRange = false;
        }
    }
}
