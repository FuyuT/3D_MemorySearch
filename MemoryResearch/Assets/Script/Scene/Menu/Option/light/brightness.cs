using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class brightness : MonoBehaviour
{
    [Header("ライト")]
    [SerializeField]
    Light light;

    [Header("音量調整用スライダー")]
    //[SerializeField]
    public Slider slider;

    [Header("キー入力で調整バーを動かせるようにするか")]
    [SerializeField]
    bool m_isInput;

    [SerializeField]
    float m_ScroolSpeed = 1;//キー入力で調整バーを動かすスピード

    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(value => this.light.intensity = value);
        slider.value = light.GetComponent<Light>().intensity;
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
        light.intensity = slider.GetComponent<Slider>().normalizedValue;
    }
}
