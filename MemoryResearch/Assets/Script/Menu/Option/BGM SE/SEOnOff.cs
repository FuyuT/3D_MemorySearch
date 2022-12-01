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

    bool SeOnOff;

    // Start is called before the first frame update
    void Start()
    {
        SeOnOff = true;
      
        slider.onValueChanged.AddListener(value => this.aud.volume = value);
        slider.value = aud.GetComponent<AudioSource>().volume;
    }

    // Update is called once per frame
    //キー入力による操作　いらないなら削除してもOK
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
