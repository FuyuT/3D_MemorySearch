using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGetMemory : MonoBehaviour
{
    [SerializeField] Animator GetMemmoryImg;

    void Update()
    {
        if (GetMemmoryImg.GetCurrentAnimatorStateInfo(0).normalizedTime == 1)
        {
            Stop();
        }

    }

    public void Stop()
    {
        //�A�j���[�V�������\���ɂ���
        gameObject.SetActive(false);
    }

}
