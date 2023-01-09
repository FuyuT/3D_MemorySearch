using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    [SerializeField]
    AudioClip clip;
    void Start()
    {
        SoundManager.instance.PlayBgm(clip);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
