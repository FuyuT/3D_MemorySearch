using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class AimY : MonoBehaviour
{
    [Header("���]�{�^��")]
    [SerializeField] GameObject Button;

    [Header("�J����")]
    [SerializeField]
    Camera cam;

    [Header("���ʒ����p�X���C�_�[")]
    public Slider slider;

    [Header("�L�[���͂ł̕ύX������s����")]
    [SerializeField]
    bool m_isInput;

    [Header("//�L�[���͂Œ����o�[�𓮂����X�s�[�h")]
    [SerializeField]
    float m_ScroolSpeed = 1;

   public bool YOnOff;

    //FPS�J���������]�X�s�[�h�����炤
    Chapter2 chapter;



    // Start is called before the first frame update
    void Start()
    {
        YOnOff = true;
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
        //chapter.rotate_speed = slider.GetComponent<Slider>().normalizedValue;
    }

    public void OnButton()
    {
        if (YOnOff)
        {
            Button.GetComponent<UnityEngine.UI.Image>().enabled = false;
            YOnOff = false;
        }
        else
        {
            Button.GetComponent<UnityEngine.UI.Image>().enabled = true;
            YOnOff = true;
        }

    }
}
