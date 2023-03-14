using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CustomInputKey;

public class TitleGame : MonoBehaviour
{
    //BGMÅESEä÷òA
    [SerializeField]
    AudioClip clip;
    [SerializeField]
    AudioClip BotannSE;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    void Update()
    {
        
    }

    public void OnBGM()
    {
        SoundManager.instance.PlayBgm(clip);

    }

    public void StopBGM()
    {
        SoundManager.instance.StopBgm();
    }

    public void OnSE()
    {
        SoundManager.instance.PlaySe(BotannSE, transform.position);

    }
}
