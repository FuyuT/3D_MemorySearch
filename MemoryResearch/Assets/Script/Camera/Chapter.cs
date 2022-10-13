using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter : MonoBehaviour
{
    private Image img;

    void Start()
    {
        img = GetComponent<Image>();
        img.color = Color.clear;
    }

    void Update()
    {
        //�{�^�����͂ŃV���b�^�[������
        if (Input.GetKeyDown("return"))
        {
            img.color = new Color(1, 1, 1, 1);
        }
        else
        {
            img.color = Color.Lerp(img.color, Color.clear, Time.deltaTime);
        }
    }
}
