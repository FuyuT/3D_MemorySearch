using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanOKUI : MonoBehaviour
{
    [SerializeField]
    GameObject OKUI;

    [SerializeField]
    SearchMemory search;
    // Start is called before the first frame update
    void Start()
    {
        OKUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowUI()
    {
        if(search.isScan)
        {
            OKUI.SetActive(true);
        }
        else
        {
            OKUI.SetActive(false);
        }
    }

    void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }


}
