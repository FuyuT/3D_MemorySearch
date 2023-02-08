using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomInputKey;


/// <summary>
/// �I�u�W�F�N�g�𓮂�����J�������N��������
/// �R���\�[���̓����蔻��ɓ����Ă��邩�����m����
/// </summary>
public class MoveObjectConsoleRange : MyUtil.SingletonMonoBehavior<MoveObjectConsoleRange>
{
    /*******************************
    * private
    *******************************/
    //�ē�UI
    [SerializeField]
    GameObject IsUseGuideUI;
    //��\���ɂ���UI
    [SerializeField]
    GameObject GameUI;

    //�X�e�[�W�ǂݍ��݃A�j���[�V����
    [SerializeField]
    GameObject StageLoadAnim;
    //�R���g���[���J����
    [SerializeField]
    GameObject ReturnUi;
    //�K�C�h���UI
    [SerializeField]
    GameObject[] ArrowDragUi;

    //�t�F�[�h�p
    [SerializeField]
    Animator FadeAnimator;

    SinzyuUI sinzyu;

    bool InRange;
    bool isUseControleCamera;
    bool isFirstControl;

    void Awake()
    {
    }

    void Start()
    {
        InRange = false;
        isUseControleCamera = false;
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
                //�R���g���[���[�J��������TPS�ɖ߂�
                CameraManager.instance.ToTpsCamera();
                GameUI.SetActive(true);
                ReturnUi.SetActive(false);
                isUseControleCamera = false;
                for (int i = 0; i < ArrowDragUi.Length; i++)
                {
                    ArrowDragUi[i].SetActive(false);
                }
                break;
            default:
                if (!InRange) break;

                //�R���g���[���[�J�����ɑJ�ڂ���ׂ̏���
                BehaviorAnimation.UpdateTrigger(ref FadeAnimator, "FadeOut");
                IsUseGuideUI.SetActive(false);
                GameUI.SetActive(false);
                isUseControleCamera = true;
                break;
        }
    }

    void ChangeCameraUpdate()
    {
        //�t�F�[�h�A�E�g���I����Ă��Ȃ���Ώ����I��
        if (!BehaviorAnimation.IsPlayEnd(ref FadeAnimator, "FadeOut")) return;

        //�J�������R���g���[���J�����Ȃ珈���I��
        if (CameraManager.instance.GetCurrentCameraType() == (int)CameraManager.CameraType.Controller) return;

        if (isFirstControl)
        {
            StageLoadAnim.SetActive(true);
            isFirstControl = false;
        }
        else
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
    /*******************************
    * protected
    *******************************/
    protected override bool dontDestroyOnLoad { get { return false; } }

    /*******************************
    * public
    *******************************/
    public bool IsUseControleCamera()
    {
        return isUseControleCamera;
    }

    /*******************************
    * �Փ˔���
    *******************************/
    private void OnTriggerStay(Collider other)
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
