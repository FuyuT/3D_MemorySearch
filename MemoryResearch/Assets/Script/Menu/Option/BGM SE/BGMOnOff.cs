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

    bool BgmOnOff;

    // Start is called before the first frame update
    void Start()
    {
        BgmOnOff = true;
        slider.onValueChanged.AddListener(value => this.aud.volume = value);
        slider.value = aud.GetComponent<AudioSource>().volume;
    }

    // Update is called once per frame
    //キー入力による操作　いらないなら削除してもOK
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
