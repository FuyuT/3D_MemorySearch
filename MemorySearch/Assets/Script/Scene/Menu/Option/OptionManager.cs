using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    /*******************************
    * private
    *******************************/
    [Header("オートセーブボタン")]
    [SerializeField] GameObject AutoSaveButton;
    [Header("BGMミュートボタン")]
    [SerializeField] GameObject MuteButtonBGM;
    [Header("SEミュートボタン")]
    [SerializeField] GameObject MuteButtonSE;
    [Header("AIMX反転ボタン")]
    [SerializeField] GameObject AimXButton;
    [Header("AIMY反転ボタン")]
    [SerializeField] GameObject AimYButton;
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

    [Header("ライト")]
    [SerializeField]
    Light light;

    //サウンドマネージャー
    SoundManager soundManager;

    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        Init();
    }

    void Update()
    {
        UpdateBGM();
        UpdateSE();
        UpdateLight();
        UpdateAimSensitivity();
    }

    private void Init()
    {
        //音量
        InitSound();

        //明るさ
        InitBrightness();

        //視点操作感度
        InitAimSensitivity();

        //オートセーブ
        InitAutoSave();
    }

    void InitSound()
    {
        SoundOption soundOption = DataManager.instance.IOptionData().GetSoundOption();
        //BGM
        //音量
        BGMslider.value = soundOption.bgmVolume;
        //ミュート
        MuteButtonBGM.GetComponent<UnityEngine.UI.Image>().enabled = soundOption.isMuteBGM;
        //SE
        //音量
        SEslider.value = soundOption.seVolume;
        //ミュート
        MuteButtonSE.GetComponent<UnityEngine.UI.Image>().enabled = soundOption.isMuteSE;
    }

    void InitBrightness()
    {
        float brightness = DataManager.instance.IOptionData().GetBrightness();
        Lightslider.value = brightness;
        light.GetComponent<Light>().intensity = brightness;
    }

    void InitAimSensitivity()
    {
        AimOption aimOption = DataManager.instance.IOptionData().GetAimOption();
        //Xの感度
        AIMXslider.value = aimOption.sensitivity.x;
        AimXButton.GetComponent<UnityEngine.UI.Image>().enabled = aimOption.isReverseX;

        //Yの感度
        AIMYslider.value = aimOption.sensitivity.y;
        AimYButton.GetComponent<UnityEngine.UI.Image>().enabled = aimOption.isReverseY;
    }

    void InitAutoSave()
    {
        AutoSaveButton.GetComponent<UnityEngine.UI.Image>().enabled
            = DataManager.instance.IOptionData().IsAutoSave();
        //Debug.Log(DataManager.instance.IOptionData().IsAutoSave());
    }

    void UpdateBGM()
    {
        //スライダーの値をデータに保存
        float volume = BGMslider.value;
        DataManager.instance.IOptionData().SetVolumeBGM(volume);

        //実際に音量を変更
        bool isMute = MuteButtonBGM.GetComponent<UnityEngine.UI.Image>().enabled;
        volume = isMute ? 0 : volume;
        soundManager.BgmVolume = volume;
    }

    void UpdateSE()
    {
        //スライダーの値をデータに保存
        float volume = SEslider.value;
        DataManager.instance.IOptionData().SetVolumeSE(volume);

        //実際に音量を変更
        bool isMute = MuteButtonSE.GetComponent<UnityEngine.UI.Image>().enabled;
        volume = isMute ? 0 : volume;
        soundManager.SeVolume = volume;
    }

    void UpdateLight()
    {
        float brightness = Lightslider.value;
        DataManager.instance.IOptionData().SetBrightness(brightness);
        light.intensity = brightness;
    }

    void UpdateAimSensitivity()
    {
        //Xの感度
        float sensitivity = AIMXslider.value;
        DataManager.instance.IOptionData().SetAimSensitivityX(sensitivity);
        //todo:感度を実際にセット

        //Yの感度
        sensitivity = AIMYslider.value;
        DataManager.instance.IOptionData().SetAimSensitivityY(sensitivity);
    }

    /*******************************
    * public
    *******************************/

    //各ボタンをクリックした時の処理
    public void OnBGMButton()
    {
        bool isMute = MuteButtonBGM.GetComponent<UnityEngine.UI.Image>().enabled;

        DataManager.instance.IOptionData().SetMuteBGM(!isMute);
        MuteButtonBGM.GetComponent<UnityEngine.UI.Image>().enabled = !isMute;
    }

    public void OnSEButton()
    {
        bool isMute = MuteButtonSE.GetComponent<UnityEngine.UI.Image>().enabled;

        DataManager.instance.IOptionData().SetMuteSE(!isMute);
        MuteButtonSE.GetComponent<UnityEngine.UI.Image>().enabled = !isMute;
    }

    public void ReverseAimX()
    {
        bool isReverse = AimXButton.GetComponent<UnityEngine.UI.Image>().enabled;

        DataManager.instance.IOptionData().SetAimIsReverseX(!isReverse);
        AimXButton.GetComponent<UnityEngine.UI.Image>().enabled = !isReverse;
    }

    public void ReverseAimY()
    {
        bool isReverse = AimYButton.GetComponent<UnityEngine.UI.Image>().enabled;

        DataManager.instance.IOptionData().SetAimIsReverseX(!isReverse);
        AimYButton.GetComponent<UnityEngine.UI.Image>().enabled = !isReverse;
    }

    public void OnAutoSaveButton()
    {
        bool isAutoSave = DataManager.instance.IOptionData().IsAutoSave();
        AutoSaveButton.GetComponent<UnityEngine.UI.Image>().enabled = !isAutoSave;
        DataManager.instance.IOptionData().SetIsAutoSave(!isAutoSave);
    }

    //オプションの値をリセットする
    public void Reset()
    {
        //サウンド
        BGMslider.value = BGMslider.maxValue * 0.5f;
        SEslider.value = SEslider.maxValue * 0.5f;
        //明るさ
        Lightslider.value = Lightslider.maxValue * 0.5f;
        //視点操作感度
        AIMXslider.value = AIMXslider.maxValue * 0.5f;
        AIMYslider.value = AIMYslider.maxValue * 0.5f;
    }
}
