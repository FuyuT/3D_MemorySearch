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
    public Slider slider;//音量調整用スライダー

    [SerializeField]
    bool m_isInput;//キー入力で調整バーを動かせるようにするか
    [SerializeField]
    float m_ScroolSpeed = 1;//キー入力で調整バーを動かすスピード

    bool OnOff;

    // Start is called before the first frame update
    void Start()
    {
        OnOff = true;
      
        slider.onValueChanged.AddListener(value => this.aud.volume = value);
        slider.value = aud.GetComponent<AudioSource>().volume;
    }

    // Update is called once per frame
    //キー入力による操作　いらないなら削除してもOK
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
