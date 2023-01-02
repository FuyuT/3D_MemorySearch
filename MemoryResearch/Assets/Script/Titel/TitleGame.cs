using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CustomInputKey;

public class TitleGame : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            FadeManager.Instance.LoadScene("Game", 1.0f);
        }
    }
}
