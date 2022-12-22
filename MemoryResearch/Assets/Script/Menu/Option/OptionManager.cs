using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{

    [Header("オートセーブボタン")]
    [SerializeField] GameObject AutoSaveButton;
    [Header("BGMミュートボタン")]
    [SerializeField] GameObject BGMButton;
    [Header("SEミュートボタン")]
    [SerializeField] GameObject SEButton;
    [Header("AIMX反転ボタン")]
    [SerializeField] GameObject AimXButton;
    [Header("AIMY反転ボタン")]
    [SerializeField] GameObject AimYButton;
    [Header("戻るボタン")]
    [SerializeField] GameObject ReturnButton;
    [Header("リセットボタン")]
    [SerializeField] GameObject ResetButton;

    [Header("BGMスライダー")]
    [SerializeField] Slider BGMslider;
    [Header("SEスライダー")]
    [SerializeField] Slider SEslider;
    [Header("明るさスライダー")]
    [SerializeField] Slider Lightslider;
    [Header("AIMXスライダー")]
    [SerializeField] Slider AIMXslider;
    [Header("AIMYスライダー")]
    [SerializeField] Slider AIMYslider;


    [Header("//キー入力で調整バーを動かすスピード")]
    [SerializeField]
    float m_ScroolSpeed = 1;

    [Header("BGMオーディオ")]
    [SerializeField]
    AudioSource BGMAud;

    [Header("SEオーディオ")]
    [SerializeField]
    AudioSource SEAud;

    [Header("ライト")]
    [SerializeField]
    Light light;

    [Header("FPSカメラ")]
    [SerializeField]
    Camera cam;

    bool BgmOnOff;
    bool SeOnOff;
    public bool XOnOff;
    public bool YOnOff;

    //FPSカメラから回転スピードをもらう
    Chapter2 chapter;

    // Start is called before the first frame update
    void Start()
    {
        BGM();
        SE();
        Light();
        AimX();
        AimY();
    }

    // Update is called once per frame
    void Update()
    {
        UpBGM();
        UpSE();
        UpLight();
    }
    //Startで呼び出す
    public void BGM()
    {
        BgmOnOff = true;
        BGMslider.onValueChanged.AddListener(value => this.BGMAud.volume = value);
        BGMslider.value = BGMAud.GetComponent<AudioSource>().volume;
    }

    public void SE()
    {
        SeOnOff = true;
        SEslider.onValueChanged.AddListener(value => this.SEAud.volume = value);
        SEslider.value = SEAud.GetComponent<AudioSource>().volume;
    }

    public void Light()
    {
        Lightslider.onValueChanged.AddListener(value => this.light.intensity = value);
        Lightslider.value = light.GetComponent<Light>().intensity;
    }

    public void AimX()
    {
        XOnOff = true;
    }

    public void AimY()
    {
        YOnOff = true;
    }
    ///////////////////////////////////

    //各Updateで呼び出す処理
    void UpBGM()
    {
        if (BgmOnOff)
        {
            BGMAud.volume = BGMslider.GetComponent<Slider>().normalizedValue;
        }
        else
        {
            BGMAud.volume = 0;
        }
    }

    void UpSE()
    {
        if (SeOnOff)
        {
            SEAud.volume = SEslider.GetComponent<Slider>().normalizedValue;
        }
        else
        {
            SEAud.volume = 0;
        }
    }

    void UpLight()
    {
        light.intensity = Lightslider.GetComponent<Slider>().normalizedValue;
    }


    ///////////////////////////////////

    //各ボタンのクリックした時の処理////////
    public void OnBGMButton()
    {
        if (BgmOnOff)
        {
            BGMButton.GetComponent<UnityEngine.UI.Image>().enabled = false;
            BgmOnOff = false;
        }
        else
        {
            BGMButton.GetComponent<UnityEngine.UI.Image>().enabled = true;
            BgmOnOff = true;
        }

    }

    public void OnSEButton()
    {
        if (SeOnOff)
        {
            SEButton.GetComponent<UnityEngine.UI.Image>().enabled = false;
            SeOnOff = false;
        }
        else
        {
            SEButton.GetComponent<UnityEngine.UI.Image>().enabled = true;
            SeOnOff = true;
        }

    }

    public void OnAimXButton()
    {
        if (XOnOff)
        {
            AimXButton.GetComponent<UnityEngine.UI.Image>().enabled = false;
            XOnOff = false;
        }
        else
        {
            AimXButton.GetComponent<UnityEngine.UI.Image>().enabled = true;
            XOnOff = true;
        }

    }

    public void OnAimYButton()
    {
        if (YOnOff)
        {
            AimYButton.GetComponent<UnityEngine.UI.Image>().enabled = false;
            YOnOff = false;
        }
        else
        {
            AimYButton.GetComponent<UnityEngine.UI.Image>().enabled = true;
            YOnOff = true;
        }

    }
    //////////////////////////////////////
}
