using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextIn : MonoBehaviour
{
    public Text T;
    public bool IsFadeIn = false;
    float Speed = 0.02f;
    float Red, Green, Blue, Alpha;

    // Start is called before the first frame update
    void Start()
    {
       
        T = GetComponent<Text>();
        Red = T.color.r;
        Green = T.color.g;
        Blue = T.color.b;
        Alpha = T.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        Alpha += Speed;
        T.color = new Color(Red, Green, Blue, Alpha);


        if (Alpha <= 255)
        {
            IsFadeIn = false;
        }
    }
}
