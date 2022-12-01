using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    SoundManager soundManager;
    [SerializeField]
    AudioClip clip;
    void Start()
    {
        soundManager.PlayBgm(clip);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
