using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SEOnOff : MonoBehaviour
{
    [SerializeField] GameObject Button;

    [SerializeField]
    AudioSource aud;

    //[SerializeField]
    public Slider slider;//���ʒ����p�X���C�_�[

    [SerializeField]
    bool m_isInput;//�L�[���͂Œ����o�[�𓮂�����悤�ɂ��邩
    [SerializeField]
    float m_ScroolSpeed = 1;//�L�[���͂Œ����o�[�𓮂����X�s�[�h

    bool SeOnOff;

    // Start is called before the first frame update
    void Start()
    {
        SeOnOff = true;
      
        slider.onValueChanged.AddListener(value => this.aud.volume = value);
        slider.value = aud.GetComponent<AudioSource>().volume;
    }

    // Update is called once per frame
    //�L�[���͂ɂ�鑀��@����Ȃ��Ȃ�폜���Ă�OK
    void Update()
    {
        if (SeOnOff)
        {
            aud.volume = slider.GetComponent<Slider>().normalizedValue;
        }
        else
        {
            aud.volume = 0;
        }
    }

    public void OnButton()
    {
        if (SeOnOff)
        {
            Button.GetComponent<UnityEngine.UI.Image>().enabled = false;
            SeOnOff = false;
        }
        else
        {
            Button.GetComponent<UnityEngine.UI.Image>().enabled = true;
            SeOnOff = true;
        }

    }
}
