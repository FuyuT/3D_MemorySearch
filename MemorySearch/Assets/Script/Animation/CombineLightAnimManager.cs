using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineLightAnimManager : MonoBehaviour
{
    /*******************************
    * private
    *******************************/
    [SerializeField] MemoryUI[] combineMaterial;
    [SerializeField] MemoryUI   combineResult;

    [SerializeField] Animator  UpperRightLight;
    [SerializeField] Animator  UpperLeftLight;
    [SerializeField] Animator  CenterLight;
    [SerializeField] Animator  LowerRightLight;
    [SerializeField] Animator  LowerLeftLight;

    void UpperRightLightUpdate()
    {
        if (combineMaterial[1].GetMemoryType() == MemoryType.None
            || combineMaterial[2].GetMemoryType() == MemoryType.None)
        {
            UpperRightLight.ResetTrigger("Play");
            UpperRightLight.SetTrigger("Stop");
        }
        else
        {
            UpperRightLight.ResetTrigger("Stop");
            UpperRightLight.SetTrigger("Play");
        }
    }
    void UpperLeftLightUpdate()
    {
        if (combineMaterial[0].GetMemoryType() == MemoryType.None
            || combineMaterial[2].GetMemoryType() == MemoryType.None)
        {
            UpperLeftLight.ResetTrigger("Play");
            UpperLeftLight.SetTrigger("Stop");
        }
        else
        {
            UpperLeftLight.ResetTrigger("Stop");
            UpperLeftLight.SetTrigger("Play");
        }
    }

    void CenterLightUpdate()
    {
        if (combineMaterial[0].GetMemoryType() == MemoryType.None
            || combineMaterial[1].GetMemoryType() == MemoryType.None)
        {
            CenterLight.ResetTrigger("Play");
            CenterLight.SetTrigger("Stop");
        }
        else
        {
            CenterLight.ResetTrigger("Stop");
            CenterLight.SetTrigger("Play");
        }
    }

    void LowerRightLightUpdate()
    {
        if(combineMaterial[1].GetMemoryType() == MemoryType.None
            || combineResult.GetMemoryType() == MemoryType.None)
        {
            LowerRightLight.ResetTrigger("Play");
            LowerRightLight.SetTrigger("Stop");
        }
        else
        {
            LowerRightLight.ResetTrigger("Stop");
            LowerRightLight.SetTrigger("Play");
        }
    }

    void LowerLeftLightUpdate()
    {
        if (combineMaterial[0].GetMemoryType() == MemoryType.None
            || combineResult.GetMemoryType() == MemoryType.None)
        {
            LowerLeftLight.ResetTrigger("Play");
            LowerLeftLight.SetTrigger("Stop");
        }
        else
        {
            LowerLeftLight.ResetTrigger("Stop");
            LowerLeftLight.SetTrigger("Play");
        }
    }

    /*******************************
    * public
    *******************************/
    public void LightChange()
    {
        UpperRightLightUpdate();
        UpperLeftLightUpdate();
        CenterLightUpdate();
        LowerRightLightUpdate();
        LowerLeftLightUpdate();
    }
}
