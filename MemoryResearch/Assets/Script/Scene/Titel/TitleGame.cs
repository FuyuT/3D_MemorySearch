using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CustomInputKey;

public class TitleGame : MonoBehaviour
{
    [SerializeField]
    SoundManager soundManager;
    [SerializeField]
    AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        soundManager.PlayBgm(clip);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.ToGame();
            soundManager.StopBgm(clip);
        }
    }
}
