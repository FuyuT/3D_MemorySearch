using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BGMOnOff : MonoBehaviour
{
    [Header("ミュートボタン")]
    [SerializeField] GameObject Button;

    [Header("オーディオ")]
    [SerializeField]
    AudioSource aud;

    [Header("音量調整用スライダー")]
    public Slider slider;

    [Header("キー入力での変更操作を行うか")]
    [SerializeField]
    bool m_isInput;

    [Header("//キー入力で調整バーを動かすスピード")]
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
