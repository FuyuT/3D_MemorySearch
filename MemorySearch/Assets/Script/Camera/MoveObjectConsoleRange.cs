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

    //戻るUI
    [SerializeField]
    GameObject ReturnUi;

    //神獣矢印UI
    [SerializeField]
    GameObject[] ArrowDragUi;

    //フェード用
    [SerializeField]
    Animator FadeAnimator;

    SinzyuUI sinzyu;

    //範囲に入ったか
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
        //StageLoadAnimが起動している時は処理を終了
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
                //コントローラーカメラからTPSに戻す
                CameraManager.instance.ToTpsCamera();
                IsUseGuideUI.SetActive(true);
                GameUI.SetActive(true);
                ReturnUi.SetActive(false);
                for (int i = 0; i < ArrowDragUi.Length; i++)
                {
                    ArrowDragUi[i].SetActive(false);
                }
                Debug.Log("TPSへ");
                break;
            default:
                //コントローラーカメラに遷移する為の準備
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
