using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sideMoveBlock : MonoBehaviour
{
   public float x;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("Swith2", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = new Vector3(x, 0, 0);
        transform.Translate(p);
    }
    void Swith()
    {
        x = x;
        Invoke("Swith2", 2.0f);
    }
    void Swith2()
    {
        x =-x;
        Invoke("Swith", 2.0f);
    }
}
