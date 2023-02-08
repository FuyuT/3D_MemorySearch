using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomInputKey;


/// <summary>
/// オブジェクトを動かせるカメラを起動させる
/// コンソールの当たり判定に入っているかを検知する
/// </summary>
public class MoveObjectConsoleRange : MyUtil.SingletonMonoBehavior<MoveObjectConsoleRange>
{
    /*******************************
    * private
    *******************************/
    //案内UI
    [SerializeField]
    GameObject IsUseGuideUI;
    //非表示にするUI
    [SerializeField]
    GameObject GameUI;

    //ステージ読み込みアニメーション
    [SerializeField]
    GameObject StageLoadAnim;
    //コントロールカメラ
    [SerializeField]
    GameObject ReturnUi;
    //ガイド矢印UI
    [SerializeField]
    GameObject[] ArrowDragUi;

    //フェード用
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
                //コントローラーカメラからTPSに戻す
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

                //コントローラーカメラに遷移する為の準備
                BehaviorAnimation.UpdateTrigger(ref FadeAnimator, "FadeOut");
                IsUseGuideUI.SetActive(false);
                GameUI.SetActive(false);
                isUseControleCamera = true;
                break;
        }
    }

    void ChangeCameraUpdate()
    {
        //フェードアウトが終わっていなければ処理終了
        if (!BehaviorAnimation.IsPlayEnd(ref FadeAnimator, "FadeOut")) return;

        //カメラがコントロールカメラなら処理終了
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
    * 衝突判定
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
