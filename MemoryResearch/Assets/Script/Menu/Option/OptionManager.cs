using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{

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

    [Header("BGM�I�[�f�B�I")]
    [SerializeField]
    AudioSource BGMAud;

    [Header("SE�I�[�f�B�I")]
    [SerializeField]
    AudioSource SEAud;

    [Header("���C�g")]
    [SerializeField]
    Light light;

    [Header("FPS�J����")]
    [SerializeField]
    Camera cam;

    bool BgmOnOff;
    bool SeOnOff;
    public bool XOnOff;
    public bool YOnOff;

    //FPS�J���������]�X�s�[�h�����炤
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
    //Start�ŌĂяo��
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

    //�eUpdate�ŌĂяo������
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
