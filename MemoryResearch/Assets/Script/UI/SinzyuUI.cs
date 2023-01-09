using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinzyuUI : MonoBehaviour
{
    //����//////////////////////////////////////////////////
    //�_�b�M�~�b�N�X�^�[�g���Ƀp�l�����Â���
    //���[�h�ƃX�e�[�W��]�A�j���[�V�����X�^�[�g
    //���[�h�����^���ɂȂ������ʂ����邭���_�b�J�������ړ�

    [SerializeField]
    Animator Fade;
    [SerializeField]
    GameObject img;

    //�A�j���[�^��////////////////////
    [SerializeField]
    Animator GimmickStageRoteAnimator;
    [SerializeField]
    Animator GimmickLordAnimator;
    //////////////////////////////////

    public bool  IsStart;
    public bool SinzyuAnimEnd;


    // Start is called before the first frame update
    void Start()
    {
        IsStart = true;
        SinzyuAnimEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (BehaviorAnimation.IsPlayEnd(ref Fade, "FadeIn") )
        {
            BehaviorAnimation.UpdateTrigger(ref GimmickStageRoteAnimator, "StageRotetion");
            BehaviorAnimation.UpdateTrigger(ref GimmickLordAnimator, "Gauge");
            IsStart = false;
        }

        if(IsStart)return;
        
        if (BehaviorAnimation.IsPlayEnd(ref GimmickLordAnimator, "Gauge"))
        {
            BehaviorAnimation.UpdateTrigger(ref Fade, "FadeOut");
        }

        if (BehaviorAnimation.IsPlayEnd(ref Fade, "FadeOut"))
        {
            BehaviorAnimation.UpdateTrigger(ref Fade, "FadeIn");
            this.gameObject.SetActive(false);
            CameraManager.instance.ToControllCamera();
        }
      

    }
    void OnEnable()
    {
        BehaviorAnimation.UpdateTrigger(ref Fade, "FadeIn");
        IsStart = true;
        SinzyuAnimEnd = false;
    }

}
