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

    bool OnOff;

    // Start is called before the first frame update
    void Start()
    {
        OnOff = true;
        //aud = GetComponent<AudioSource>();
        slider.onValueChanged.AddListener(value => this.aud.volume = value);
        slider.value = aud.GetComponent<AudioSource>().volume;
    }

    // Update is called once per frame
    //�L�[���͂ɂ�鑀��@����Ȃ��Ȃ�폜���Ă�OK
    void Update()
    {
        //float v = m_Slider.value;
        //if (m_isInput)
        //{
        //    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        //    {
        //        v -= m_ScroolSpeed * Time.deltaTime;
        //    }
        //    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.RightArrow))
        //    {
        //        v += m_ScroolSpeed * Time.deltaTime;
        //    }
        //}
        //v = Mathf.Clamp(v, 0, 1);
        //m_Slider.value = v;
        //ClickOnOff();
      aud.volume = slider.GetComponent<Slider>().normalizedValue;
    }

    public  void OnButton()
    {
        if (OnOff)
        {
            Button.GetComponent<UnityEngine.UI.Image>().enabled = false;
            OnOff = false;
        }
        else
        {
            Button.GetComponent<UnityEngine.UI.Image>().enabled = true;
            OnOff = true;
        }

    }
}
