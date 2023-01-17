using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSE : MonoBehaviour
{
    [SerializeField] public AudioClip WalkSE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
    }

    public void PlayFootstepSound()
    {
        SoundManager.instance.PlaySe(WalkSE, transform.position);
    }
}
