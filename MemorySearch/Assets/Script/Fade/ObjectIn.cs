using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectIn : MonoBehaviour
{

    public Image B;
    

    public bool IsFadeIn = false;
    float Speed = 0.05f;
    float Red, Green, Blue, Alpha;
  

    // Start is called before the first frame update
    void Start()
    {
        B = GetComponent<Image>();
        Red = B.color.r;
        Green = B.color.g;
        Blue = B.color.b;
        Alpha = B.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        //if (IsFadeIn)
        //{
        //    FadeIn();
        //}

        Alpha += Speed;
        B.color = new Color(Red, Green, Blue, Alpha);
    
        if (Alpha <= 255 )
        {
            IsFadeIn = false;
        }
    }

    //void FadeIn()
    //{
    //    Color color = B.color;
    //    color.a = color.a <= 0 ? 1 : color.a - Speed;
    //    B.color = color;
    //}

}
