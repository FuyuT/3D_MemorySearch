using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effekseer;

[System.Serializable]
public class EffectPlayer : MonoBehaviour
{
    /*******************************
    * protected
    *******************************/
    [SerializeField] protected Effekseer.EffekseerEmitter[] effects;

    /*******************************
    * public
    *******************************/
    public void SetEffects(Effekseer.EffekseerEmitter[] effects)
    {
        this.effects = effects;
    }

    /// <summary>
    /// エフェクトを再生
    /// </summary>
    /// <param name="no">再生するエフェクトの配列番号　初期値は0</param>
    public void PlayEffect(int no = 0)
    {
        if (effects.Length < no) return;

        effects[no].Play();
    }
    /// <summary>
    /// エフェクトを再生
    /// </summary>
    /// <param name="pos">再生する座標</param>
    /// <param name="no">再生するエフェクトの配列番号　初期値は0</param>
    public void PlayEffect(Vector3 pos, int no = 0)
    {
        effects[no].gameObject.transform.position = pos;
        PlayEffect(no);
    }
    /// <summary>
    /// エフェクトを停止
    /// </summary>
    /// <param name="no">再生するエフェクトの配列番号　初期値は0</param>
    public void StopEffect(int no = 0)
    {
        if (effects.Length < no) return;

        effects[no].Stop();
    }
}
