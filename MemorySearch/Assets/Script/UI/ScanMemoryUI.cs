using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanMemoryUI : MonoBehaviour
{
    [SerializeField]
    GameObject SearchMemory;

    [SerializeField]
    SearchMemory search;

    // Start is called before the first frame update
    void Start()
    {
        SearchMemory.SetActive(false);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowUI()
    {
        if (search.isScan)
        {
            SearchMemory.SetActive(true);
        }
        else
        {
            SearchMemory.SetActive(false);
        }
    }

    void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
