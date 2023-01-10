using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameClear : MonoBehaviour
{
    [SerializeField]
    AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayBgm(clip);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.ToTitle();
            SoundManager.instance.StopBgm(clip);
        }
    }
}
