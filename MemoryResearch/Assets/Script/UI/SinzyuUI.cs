using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinzyuUI : MonoBehaviour
{
    //メモ//////////////////////////////////////////////////
    //神獣ギミックスタート時にパネルを暗くし
    //ロードとステージ回転アニメーションスタート
    //ロードが満タンになったら画面が明るくし神獣カメラを移動

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
