using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effekseer;

public class EffectPlayer : MonoBehaviour
{
    [SerializeField] Effekseer.EffekseerEmitter[] effect;

    private void Awake()
    {
    }

    public void PlayEffect(int no)
    {
        if (effect.Length < no) return;

        effect[no].Play();
    }

    public void StopEffect(int no)
    {
        if (effect.Length < no) return;

        effect[no].Stop();
    }
}
