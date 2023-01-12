using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinzyuUI : MonoBehaviour
{

    [SerializeField]
    Animator Fade;
    [SerializeField]
    GameObject img;

    //アニメータ類////////////////////
    [SerializeField]
    Animator GimmickStageRoteAnimator;
    [SerializeField]
    Animator GimmickLordAnimator;
    //////////////////////////////////

    [SerializeField]
    GameObject ReturnUi;

    public bool  IsStart;
    public bool SinzyuAnimEnd;


    // Start is called before the first frame update
    void Start()
    {
        IsStart = true;
        SinzyuAnimEnd = false;
        ReturnUi.SetActive(false);
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
            ReturnUi.SetActive(true);
        }
      

    }
    void OnEnable()
    {
        BehaviorAnimation.UpdateTrigger(ref Fade, "FadeIn");
        IsStart = true;
        SinzyuAnimEnd = false;
    }

}
