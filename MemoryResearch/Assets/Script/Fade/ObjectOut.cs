using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectOut : MonoBehaviour
{
    Image Ob;

    public bool IsFadeOut = false;
    float Speed = 0.02f;
    float Red, Green, Blue, Alpha;


    // Start is called before the first frame update
    void Start()
    {
        Ob = GetComponent<Image>();
        Red = Ob.color.r;
        Green = Ob.color.g;
        Blue = Ob.color.b;
        Alpha = Ob.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        Alpha += Speed;
        Ob.color = new Color(Red, Green, Blue, Alpha);
        if (Alpha >= 1)
        {
            IsFadeOut = false;
        }
    }

    void Fade()
    {
       
    }
}
