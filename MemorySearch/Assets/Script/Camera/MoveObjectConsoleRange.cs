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
    GameObject IsUseGuideUI;

    //��\��UI��///////////////////////
    [SerializeField]
    GameObject GameUI;

    /////////////////////////////////////////

    //�_�bUI
    [SerializeField]
    GameObject StageLoadAnim;

    //�߂�UI
    [SerializeField]
    GameObject ReturnUi;

    //�_�b���UI
    [SerializeField]
    GameObject[] ArrowDragUi;

    //�t�F�[�h�p
    [SerializeField]
    Animator FadeAnimator;

    SinzyuUI sinzyu;

    //�͈͂ɓ�������
    public bool InRange;

    bool isFirstControl;

    void Start()
    {
        InRange = false;
        IsUseGuideUI.SetActive(false);
        ReturnUi.SetActive(false);
        for (int i = 0; i < ArrowDragUi.Length; i++)
        {
            ArrowDragUi[i].SetActive(false);
        }
        isFirstControl = true;
    }


    void Update()
    {
        //StageLoadAnim���N�����Ă��鎞�͏������I��
        if (StageLoadAnim.activeSelf) return;

        ChangeCamera();
        ChangeCameraUpdate();
    }

    void ChangeCamera()
    {
        if (!Input.GetKeyDown(KeyCode.R)) return;

        switch (CameraManager.instance.GetCurrentCameraType())
        {
            case (int)CameraManager.CameraType.Controller:
                if (!InRange) break;
                //�R���g���[���[�J��������TPS�ɖ߂�
                CameraManager.instance.ToTpsCamera();
                IsUseGuideUI.SetActive(true);
                GameUI.SetActive(true);
                ReturnUi.SetActive(false);
                for (int i = 0; i < ArrowDragUi.Length; i++)
                {
                    ArrowDragUi[i].SetActive(false);
                }
                Debug.Log("TPS��");
                break;
            default:
                //�R���g���[���[�J�����ɑJ�ڂ���ׂ̏���
                BehaviorAnimation.UpdateTrigger(ref FadeAnimator, "FadeOut");
                IsUseGuideUI.SetActive(false);
                GameUI.SetActive(false);
                break;
        }
    }

    void ChangeCameraUpdate()
    {
        if (isFirstControl)
        {
            if (BehaviorAnimation.IsPlayEnd(ref FadeAnimator, "FadeOut"))
            {
                StageLoadAnim.SetActive(true);
                isFirstControl = false;
            }
        }
        else
        {
            if (BehaviorAnimation.IsPlayEnd(ref FadeAnimator, "FadeOut"))
            {
                BehaviorAnimation.UpdateTrigger(ref FadeAnimator, "FadeIn");
                CameraManager.instance.ToControllCamera();
                ReturnUi.SetActive(true);
                for (int i = 0; i < ArrowDragUi.Length; i++)
                {
                    ArrowDragUi[i].SetActive(true);
                }
            }
        }
    }

    //���u�ɋ߂Â��ƕ\��
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IsUseGuideUI.SetActive(true);
            InRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IsUseGuideUI.SetActive(false);
            InRange = false;
        }
    }
}
