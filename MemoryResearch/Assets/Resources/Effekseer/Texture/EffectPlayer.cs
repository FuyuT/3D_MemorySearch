using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effekseer;

public class EffectPlayer : MonoBehaviour
{
    [SerializeField] Effekseer.EffekseerEmitter effect;

    private void Update()
    {
        //�Đ����I����Ă���΃G�t�F�N�g���I������
        if (!effect.exists)
        {
            effect.Stop();
        }
    }

    private void OnEnable()
    {
        effect.Play();
    }

    private void OnDisable()
    {
        effect.Stop();
    }
}
