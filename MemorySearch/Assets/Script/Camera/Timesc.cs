using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timesc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
    }

    public void AnimStart()
    {
        Time.timeScale = 0;

    }

    public void AnimEnd()
    {
        Time.timeScale = 1;

    }
}
