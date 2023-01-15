using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CustomInputKey;

public class TitleGame : MonoBehaviour
{
    //BGM�ESE�֘A
    [SerializeField]
    AudioClip clip;
    [SerializeField]
    AudioClip BotannSE;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayBgm(clip);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.ToGame();
            SoundManager.instance.StopBgm(clip);
            SoundManager.instance.PlaySe(BotannSE, transform.position);
        }
    }
}
