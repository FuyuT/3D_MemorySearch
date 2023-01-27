using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    /*******************************
    * private
    *******************************/

    [SerializeField] Light[] lights;

    void Awake()
    {
        SetBrightness(DataManager.instance.IOptionData().GetBrightness());
    }

    /*******************************
    * public
    *******************************/
    void SetBrightness(float brightness)
    {
        for(int n = 0; n < lights.Length; n++)
        {
            lights[n].intensity = brightness;
        }
    }
}
