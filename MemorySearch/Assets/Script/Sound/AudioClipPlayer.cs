using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipPlayer : MonoBehaviour
{
    [SerializeField] public AudioClip[] Audios;

    public void Play(int audioNo)
    {
        SoundManager.instance.PlaySe(Audios[audioNo], transform.position);
    }
}
