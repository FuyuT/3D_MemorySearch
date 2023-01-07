using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    //ボタン類///////////////////////////////////
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
    //////////////////////////////////////////////

    //スライダー類///////////////////////////////////
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
    //////////////////////////////////////////////

    [Header("//キー入力で調整バーを動かすスピード")]
    [SerializeField]
    float m_ScroolSpeed = 1;

    [Header("BGMオーディオ")]
    [SerializeField]
    AudioSource BGMAudio;

    [Header("SEオーディオ")]
    [SerializeField]
    AudioSource SEAudio;

    [Header("パネル")]
    [SerializeField]
    Image LightPanel;

    [Header("FPSカメラ")]
    [SerializeField]
    Camera cam;

    bool BgmOnOff;
    bool SeOnOff;
   
    public bool isAimReverseX;
    public bool isAimReverseY;

    void Start()
    {
        BGM();
        SE();
        Light();
        AimX();
        AimY();
    }

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
        BGMslider.onValueChanged.AddListener(value => this.BGMAudio.volume = value);
        BGMslider.value = BGMAudio.GetComponent<AudioSource>().volume;
    }

    public void SE()
    {
        SeOnOff = true;
        SEslider.onValueChanged.AddListener(value => this.SEAudio.volume = value);
        SEslider.value = SEAudio.GetComponent<AudioSource>().volume;
    }

    public void Light()
    {
        Lightslider.maxValue = 0.5f;
        Lightslider.onValueChanged.AddListener(value => this.LightPanel.color = new Color(0, 0, 0, value));
        Lightslider.value = LightPanel.color.a;
    }

    public void AimX()
    {
        isAimReverseX = false;
    }

    public void AimY()
    {
        isAimReverseY = false;
    }
    ///////////////////////////////////

    //各Updateで呼び出す処理
    void UpBGM()
    {
        if (BgmOnOff)
        {
            BGMAudio.volume = BGMslider.GetComponent<Slider>().normalizedValue;
        }
        else
        {
            BGMAudio.volume = 0;
        }
    }

    void UpSE()
    {
        if (SeOnOff)
        {
            SEAudio.volume = SEslider.GetComponent<Slider>().normalizedValue;
        }
        else
        {
            SEAudio.volume = 0;
        }
    }

    void UpLight()
    {
        if (LightPanel.color.a >= 100)
        {
            LightPanel.color = new Color(0, 0, 0, Lightslider.GetComponent<Slider>().normalizedValue);
        }
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

    public void ReverseAimX()
    {
        if (isAimReverseX)
        {
            AimXButton.GetComponent<UnityEngine.UI.Image>().enabled = false;
            isAimReverseX = false;
        }
        else
        {
            AimXButton.GetComponent<UnityEngine.UI.Image>().enabled = true;
            isAimReverseX = true;
        }

    }

    public void ReverseAimY()
    {
        if (isAimReverseY)
        {
            AimYButton.GetComponent<UnityEngine.UI.Image>().enabled = false;
            isAimReverseY = false;
        }
        else
        {
            AimYButton.GetComponent<UnityEngine.UI.Image>().enabled = true;
            isAimReverseY = true;
        }

    }
    //////////////////////////////////////
}
