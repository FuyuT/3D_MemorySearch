using UnityEngine;

public class SoundManager : MonoBehaviour
{
    /*******************************
    * private
    *******************************/

    [SerializeField]
    AudioSource bgmAudioSource;
    [SerializeField]
    AudioSource seAudioSource;

    void Awake()
    {
        //�T�E���h�}�l�[�W���[�����ɂȂ����m�F���A����ꍇ�͍폜����
        //�i�O��̃V�[����������p���ł������̂Ȃǁj
        GameObject soundManager = GameObject.FindGameObjectWithTag("SoundManager");
        bool checkResult = soundManager != null && soundManager != gameObject;
        if (checkResult)
        {
            Destroy(gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /*******************************
    * public
    *******************************/

    [SerializeField]
    static public SoundManager instance;

    //����
    public float BgmVolume
    {
        get
        {
            return bgmAudioSource.volume;
        }
        set
        {
            bgmAudioSource.volume = Mathf.Clamp01(value);
        }
    }
    public float SeVolume
    {
        get
        {
            return seAudioSource.volume;
        }
        set
        {
            seAudioSource.volume = Mathf.Clamp01(value);
        }
    }

    //�Đ�
    public void PlayBgm(AudioClip clip)
    {
        bgmAudioSource.clip = clip;
        if (clip == null) return;
        bgmAudioSource.Play();
    }
    public void PlaySe(AudioClip clip,Vector3 pos)
    {
        seAudioSource.clip = clip;
        if (clip == null) return;
       // seAudioSource.PlayOneShot(clip);
        seAudioSource.Play();


        seAudioSource.transform.position = pos;
    }

    //��~
    public void StopBgm(AudioClip clip)
    {
        bgmAudioSource.clip = clip;
        if (clip == null) return;
        bgmAudioSource.Stop();
    }
    public void StopSe(AudioClip clip)
    {
        Debug.Log(seAudioSource);
        seAudioSource.clip = clip;
        if (clip == null) return;
        seAudioSource.Stop();
    }

    public bool IsPlayingBgm(AudioClip clip)
    {
        bgmAudioSource.clip = clip;
        if (clip == null) return false;
        return bgmAudioSource.isPlaying;
    }

    public bool IsPlayingSe(AudioClip clip)
    {
        seAudioSource.clip = clip;
        if (clip == null) return false;
        return seAudioSource.isPlaying;
    }
}
