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
        //サウンドマネージャーが他にないか確認し、ある場合は削除する
        //（前回のシーンから引き継いできたものなど）
        GameObject soundManager = GameObject.FindGameObjectWithTag("SoundManager");
        bool checkResult = soundManager != null && soundManager != gameObject;
        if (checkResult)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /*******************************
    * public
    *******************************/

    [SerializeField]
    static public SoundManager instance;

    //音量
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

    //再生
    public void PlayBgm(AudioClip clip)
    {
        bgmAudioSource.clip = clip;
        if (clip == null) return;
        bgmAudioSource.Play();
    }
    public void PlaySe(AudioClip clip,Vector3 pos)
    {
        if (clip == null) return;
        seAudioSource.transform.position = pos;
        seAudioSource.PlayOneShot(clip);
    }
 
    //停止
    public void StopBgm()
    {
        bgmAudioSource.Stop();
    }
    public void StopSe()
    {
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
