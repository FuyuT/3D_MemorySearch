using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOption
{
    public float bgmVolume;
    public bool isMuteBGM;

    public float seVolume;
    public bool  isMuteSE;
}

public class AimOption
{
    public Vector2 sensitivity;
    public bool    isReverseX;
    public bool    isReverseY;
}

public class OptionData : IOptionData
{
    /*******************************
    * public
    *******************************/
    public SoundOption soundOption;
    public float brightness;
    public AimOption aimOption;
    public bool isAutoSave;

    public OptionData()
    {
        soundOption = new SoundOption();
        brightness = 0;
        aimOption = new AimOption();
        isAutoSave = false;
    }

    //サウンド
    public SoundOption GetSoundOption()
    {
        return soundOption;
    }

    //BGM
    public void SetVolumeBGM(float volume)
    {
        soundOption.bgmVolume = volume;
    }
    public void SetMuteBGM(bool isMute)
    {
        soundOption.isMuteBGM = isMute;
    }
    //SE
    public void SetVolumeSE(float volume)
    {
        soundOption.seVolume = volume;
    }
    public void SetMuteSE(bool isMute)
    {
        soundOption.isMuteSE = isMute;
    }
    //明るさ
    public float GetBrightness()
    {
       return brightness;
    }

    public void SetBrightness(float brightness)
    {
        this.brightness = brightness;
    }

    //視点操作感度
    public AimOption GetAimOption()
    {
        return aimOption;
    }

    public void SetAimSensitivityX(float sensitivity)
    {
        aimOption.sensitivity.x = sensitivity;
    }
    public void SetAimSensitivityY(float sensitivity)
    {
        aimOption.sensitivity.y = sensitivity;
    }

    public void SetAimIsReverseX(bool isReverse)
    {
        aimOption.isReverseX = isReverse;
    }
    public void SetAimIsReverseY(bool isReverse)
    {
        aimOption.isReverseY = isReverse;
    }

    //オートセーブ
    public bool IsAutoSave()
    {
        return isAutoSave;
    }

    public void SetIsAutoSave(bool isAutoSave)
    {
        this.isAutoSave = isAutoSave;
    }

}