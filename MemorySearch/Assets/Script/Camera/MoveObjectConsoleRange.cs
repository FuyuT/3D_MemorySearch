using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomInputKey;


/// <summary>
/// オブジェクトを動かせるカメラを起動させる
/// コンソールの当たり判定に入っているかを検知する
/// </summary>
public class MoveObjectConsoleRange : MonoBehaviour
{
    //案内UI
    [SerializeField]
    GameObject IsUseGuideUI;

    //非表示UI類///////////////////////
    [SerializeField]
    GameObject GameUI;

    /////////////////////////////////////////

    //神獣UI
    [SerializeField]
    GameObject StageLoadAnim;

    [SerializeField]
    GameObject ReturnUi;

    //フェード用
    [SerializeField]
    Animator FadeAnimator;

    SinzyuUI sinzyu;

    //範囲に入ったか
    public bool InRange;

    bool isFirstControl;

    void Awake()
    {
    }

    void Start()
    {
        InRange = false;
        IsUseGuideUI.SetActive(false);
        ReturnUi.SetActive(false);
        isFirstControl = true;
    }

 
    void Update()
    {
        //神獣アニメーションスタート
        //StageLoadAnimはステージ読み込みアニメーション
        if (StageLoadAnim.activeSelf) return;
       
        if (Input.GetKeyDown("r") && InRange)
        {
            switch (CameraManager.instance.GetCurrentCameraType())
            {
                case (int)CameraManager.CameraType.Controller:
                    //コントローラーカメラからTPSに戻す
                    CameraManager.instance.ToTpsCamera();
                    IsUseGuideUI.SetActive(true);
                    GameUI.SetActive(true);
                    ReturnUi.SetActive(false);
                    break;
                default:
                    //コントローラーカメラに遷移する為の準備
                    BehaviorAnimation.UpdateTrigger(ref FadeAnimator, "FadeOut");
                    IsUseGuideUI.SetActive(false);
                    GameUI.SetActive(false);
                    break;
            }
        }

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
            }
        }
    }

    //装置に近づくと表示
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
