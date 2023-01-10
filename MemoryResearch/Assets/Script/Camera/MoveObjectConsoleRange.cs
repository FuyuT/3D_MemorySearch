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
    GameObject GuideUI;

    //非表示UI類///////////////////////
    [SerializeField]
    GameObject HPUI;

    [SerializeField]
    GameObject EquipmentUi;
    /////////////////////////////////////////

    //神獣UI
    [SerializeField]
    GameObject SinzyuUi;

    [SerializeField]
    GameObject ReturnUi;

    //フェード用
    [SerializeField]
    Animator FadeAnimator;

    SinzyuUI sinzyu;


    //範囲に入ったか
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

    void Update()
    {
        //神獣アニメーションスタート
        if (SinzyuUi.activeSelf) return;


        if (Input.GetKeyDown("r") && InRange)
        {
            if (CameraManager.instance.GetCurrentCameraType() != (int)CameraManager.CameraType.Controller)
            {
                a = true;
                BehaviorAnimation.UpdateTrigger(ref FadeAnimator, "FadeOut");
                GuideUI.SetActive(false);
                HPUI.SetActive(false);
                EquipmentUi.SetActive(false);

            }
            else
            {
                CameraManager.instance.ToTpsCamera();
                GuideUI.SetActive(true);
                HPUI.SetActive(true);
                EquipmentUi.SetActive(true);
                ReturnUi.SetActive(false);
            }
        }

        if (!a) return;

        if (BehaviorAnimation.IsPlayEnd(ref FadeAnimator, "FadeOut"))
        {
            SinzyuUi.SetActive(true);
            a = false;
        }
    }

    //装置に近づくと表示
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
