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
    /// �G�t�F�N�g���Đ�
    /// </summary>
    /// <param name="no">�Đ�����G�t�F�N�g�̔z��ԍ��@�����l��0</param>
    public void PlayEffect(int no = 0)
    {
        if (effects.Length < no) return;

        effects[no].Play();
    }
    /// <summary>
    /// �G�t�F�N�g���Đ�
    /// </summary>
    /// <param name="pos">�Đ�������W</param>
    /// <param name="no">�Đ�����G�t�F�N�g�̔z��ԍ��@�����l��0</param>
    public void PlayEffect(Vector3 pos, int no = 0)
    {
        effects[no].gameObject.transform.position = pos;
        PlayEffect(no);
    }
    /// <summary>
    /// �G�t�F�N�g���~
    /// </summary>
    /// <param name="no">�Đ�����G�t�F�N�g�̔z��ԍ��@�����l��0</param>
    public void StopEffect(int no = 0)
    {
        if (effects.Length < no) return;

        effects[no].Stop();
    }
}
