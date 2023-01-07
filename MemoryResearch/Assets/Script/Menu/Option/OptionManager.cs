using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    //�{�^����///////////////////////////////////
    [Header("�I�[�g�Z�[�u�{�^��")]
    [SerializeField] GameObject AutoSaveButton;
    [Header("BGM�~���[�g�{�^��")]
    [SerializeField] GameObject BGMButton;
    [Header("SE�~���[�g�{�^��")]
    [SerializeField] GameObject SEButton;
    [Header("AIMX���]�{�^��")]
    [SerializeField] GameObject AimXButton;
    [Header("AIMY���]�{�^��")]
    [SerializeField] GameObject AimYButton;
    [Header("�߂�{�^��")]
    [SerializeField] GameObject ReturnButton;
    [Header("���Z�b�g�{�^��")]
    [SerializeField] GameObject ResetButton;
    //////////////////////////////////////////////

    //�X���C�_�[��///////////////////////////////////
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
    //////////////////////////////////////////////

    [Header("//�L�[���͂Œ����o�[�𓮂����X�s�[�h")]
    [SerializeField]
    float m_ScroolSpeed = 1;

    [Header("BGM�I�[�f�B�I")]
    [SerializeField]
    AudioSource BGMAudio;

    [Header("SE�I�[�f�B�I")]
    [SerializeField]
    AudioSource SEAudio;

    [Header("�p�l��")]
    [SerializeField]
    Image LightPanel;

    [Header("FPS�J����")]
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

    //Start�ŌĂяo��
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

    //�eUpdate�ŌĂяo������
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

    //�e�{�^���̃N���b�N�������̏���////////
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
