using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AimX : MonoBehaviour
{
    [Header("反転ボタン")]
    [SerializeField] GameObject Button;

    [Header("カメラ")]
    [SerializeField]
    Camera cam;

    [Header("音量調整用スライダー")]
    public Slider slider;

    [Header("キー入力での変更操作を行うか")]
    [SerializeField]
    bool m_isInput;

    [Header("//キー入力で調整バーを動かすスピード")]
    [SerializeField]
    float m_ScroolSpeed = 1;

  public  bool XOnOff;

    //FPSカメラから回転スピードをもらう
    SearchMemory chapter;



    // Start is called before the first frame update
    void Start()
    {
        XOnOff = true;
        //aud = GetComponent<AudioSource>();
        //slider.onValueChanged.AddListener(value => this.chapter.rotate_speed = value);
        //slider.value = chapter.GetComponent<>().volume;
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
       //chapter.rotate_speed = slider.GetComponent<Slider>().normalizedValue;
    }

    public void OnButton()
    {
        if (XOnOff)
        {
            Button.GetComponent<UnityEngine.UI.Image>().enabled = false;
            XOnOff = false;
        }
        else
        {
            Button.GetComponent<UnityEngine.UI.Image>().enabled = true;
            XOnOff = true;
        }

    }
}
