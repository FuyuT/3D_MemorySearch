using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ShowUI : MonoBehaviour
{
    [SerializeField]
    GameObject ScanImg;

    [SerializeField]
    GameObject FPSCam;

    void Start()
    {
        ScanImg.SetActive(false);
    }

    void Update()
    {
        if(FPSCam.activeSelf)
        {
            ScanImg.SetActive(true);
        }
        else
        {
            ScanImg.SetActive(false);
        }

        //ScanImg.transform.rotation = FPSCam.transform.rotation;

    }
}
