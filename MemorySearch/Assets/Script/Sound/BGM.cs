using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    [SerializeField]
    AudioClip StageClip;

    [SerializeField]
    AudioClip BossClip;

    [SerializeField]
    GameObject Player;

    void Start()
    {
        SoundManager.instance.PlayBgm(StageClip);
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.PlayBgm(BossClip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.PlayBgm(StageClip);
        }
    }
}
