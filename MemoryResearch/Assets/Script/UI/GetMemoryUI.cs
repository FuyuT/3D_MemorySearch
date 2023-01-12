using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMemoryUI : MonoBehaviour
{
    [SerializeField] Animator ScanImgAnim;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
     {
        if (ScanImgAnim.GetCurrentAnimatorStateInfo(0).normalizedTime == 1)
        {
            Stop();
        }

    }

     public void Play()
     {
        //アニメーションを再生する
        gameObject.SetActive(true);
        ScanImgAnim.Play("ScanSuccessText");
     }

     public void Stop()
     {
        //アニメーションを非表示にする
        gameObject.SetActive(false);
     }
    
    
}
