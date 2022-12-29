using System.Collections.Generic;
using UnityEngine;
using CustomInputKey;

public class OpenOption : MonoBehaviour
{
    //オプションパネル
    [SerializeField]GameObject option;


    // Start is called before the first frame update
    void Start()
    {
        option.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.O))
        {
            if(!option.activeSelf)
            {
                OnOption();
            }
            else
            {
                OffOption();
            }
        }
    }

    public void OnOption()
    {
        option.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OffOption()
    {
        option.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
