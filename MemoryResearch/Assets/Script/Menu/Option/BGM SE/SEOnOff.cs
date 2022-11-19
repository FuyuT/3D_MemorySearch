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

    bool OnOff;

    // Start is called before the first frame update
    void Start()
    {
        OnOff = true;
      
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

    public void OnButton()
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
