using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BGMOnOff : MonoBehaviour
{
    [Header("�~���[�g�{�^��")]
    [SerializeField] GameObject Button;

    [Header("�I�[�f�B�I")]
    [SerializeField]
    AudioSource aud;

    [Header("���ʒ����p�X���C�_�[")]
    public Slider slider;

    [Header("�L�[���͂ł̕ύX������s����")]
    [SerializeField]
    bool m_isInput;

    [Header("//�L�[���͂Œ����o�[�𓮂����X�s�[�h")]
    [SerializeField]
    float m_ScroolSpeed = 1;

    bool BgmOnOff;

    // Start is called before the first frame update
    void Start()
    {
        BgmOnOff = true;
        slider.onValueChanged.AddListener(value => this.aud.volume = value);
        slider.value = aud.GetComponent<AudioSource>().volume;
    }

    // Update is called once per frame
    //�L�[���͂ɂ�鑀��@����Ȃ��Ȃ�폜���Ă�OK
    void Update()
    {
        if (BgmOnOff)
        {
            aud.volume = slider.GetComponent<Slider>().normalizedValue;
        }
        else
        {
            aud.volume = 0;
        }
    }

    public  void OnButton()
    {
        if (BgmOnOff)
        {
            Button.GetComponent<UnityEngine.UI.Image>().enabled = false;
            BgmOnOff = false;
        }
        else
        {
            Button.GetComponent<UnityEngine.UI.Image>().enabled = true;
            BgmOnOff = true;
        }

    }
}
