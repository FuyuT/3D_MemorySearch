using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    /*******************************
    * private
    *******************************/
    [Header("�I�[�g�Z�[�u�{�^��")]
    [SerializeField] GameObject AutoSaveButton;
    [Header("BGM�~���[�g�{�^��")]
    [SerializeField] GameObject MuteButtonBGM;
    [Header("SE�~���[�g�{�^��")]
    [SerializeField] GameObject MuteButtonSE;
    [Header("AIMX���]�{�^��")]
    [SerializeField] GameObject AimXButton;
    [Header("AIMY���]�{�^��")]
    [SerializeField] GameObject AimYButton;
    [Header("���Z�b�g�{�^��")]
    [SerializeField] GameObject ResetButton;

    [Header("BGM�X���C�_�[")]
    [SerializeField] Slider BGMslider;
    [Header("SE�X���C�_�[")]
    [SerializeField] Slider SEslider;
    [Header("���邳�X���C�_�[")]
    [SerializeField] Slider Lightslider;
    [Header("AIMX�X���C�_�[")]
    [SerializeField] Slider AIMXslider;
    [Header("AIMY�X���C�_�[")]
    [SerializeField] Slider AIMYslider;


    [Header("//�L�[���͂Œ����o�[�𓮂����X�s�[�h")]
    [SerializeField]
    float m_ScroolSpeed = 1;

    [Header("���C�g")]
    [SerializeField]
    Light light;

    //�T�E���h�}�l�[�W���[
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
        //����
        InitSound();

        //���邳
        InitBrightness();

        //���_���슴�x
        InitAimSensitivity();

        //�I�[�g�Z�[�u
        InitAutoSave();
    }

    void InitSound()
    {
        SoundOption soundOption = DataManager.instance.IOptionData().GetSoundOption();
        //BGM
        //����
        BGMslider.value = soundOption.bgmVolume;
        //�~���[�g
        MuteButtonBGM.GetComponent<UnityEngine.UI.Image>().enabled = soundOption.isMuteBGM;
        //SE
        //����
        SEslider.value = soundOption.seVolume;
        //�~���[�g
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
        //X�̊��x
        AIMXslider.value = aimOption.sensitivity.x;
        AimXButton.GetComponent<UnityEngine.UI.Image>().enabled = aimOption.isReverseX;

        //Y�̊��x
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
        //�X���C�_�[�̒l���f�[�^�ɕۑ�
        float volume = BGMslider.value;
        DataManager.instance.IOptionData().SetVolumeBGM(volume);

        //���ۂɉ��ʂ�ύX
        bool isMute = MuteButtonBGM.GetComponent<UnityEngine.UI.Image>().enabled;
        volume = isMute ? 0 : volume;
        soundManager.BgmVolume = volume;
    }

    void UpdateSE()
    {
        //�X���C�_�[�̒l���f�[�^�ɕۑ�
        float volume = SEslider.value;
        DataManager.instance.IOptionData().SetVolumeSE(volume);

        //���ۂɉ��ʂ�ύX
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
        //X�̊��x
        float sensitivity = AIMXslider.value;
        DataManager.instance.IOptionData().SetAimSensitivityX(sensitivity);
        //todo:���x�����ۂɃZ�b�g

        //Y�̊��x
        sensitivity = AIMYslider.value;
        DataManager.instance.IOptionData().SetAimSensitivityY(sensitivity);
    }

    /*******************************
    * public
    *******************************/

    //�e�{�^�����N���b�N�������̏���
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

    //�I�v�V�����̒l�����Z�b�g����
    public void Reset()
    {
        //�T�E���h
        BGMslider.value = BGMslider.maxValue * 0.5f;
        SEslider.value = SEslider.maxValue * 0.5f;
        //���邳
        Lightslider.value = Lightslider.maxValue * 0.5f;
        //���_���슴�x
        AIMXslider.value = AIMXslider.maxValue * 0.5f;
        AIMYslider.value = AIMYslider.maxValue * 0.5f;
    }
}
