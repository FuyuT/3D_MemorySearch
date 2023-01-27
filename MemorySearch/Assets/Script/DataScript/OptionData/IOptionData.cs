using System.Collections;
using System.Collections.Generic;

public interface IOptionData
{
    //サウンド
    public SoundOption GetSoundOption();

    //BGM
    public void SetVolumeBGM(float volume);
    public void SetMuteBGM(bool isMute);
    //SE
    public void SetVolumeSE(float volume);
    public void SetMuteSE(bool isMute);
    //明るさ
    public float GetBrightness();
    public void SetBrightness(float brightness);
    //視点操作感度
    public AimOption GetAimOption();
    public void SetAimSensitivityX(float sensitivity);
    public void SetAimSensitivityY(float sensitivity);
    public void SetAimIsReverseX(bool isReverse);
    public void SetAimIsReverseY(bool isReverse);
    //オートセーブ
    public bool IsAutoSave();
    public void SetIsAutoSave(bool isAutoSave);
}
