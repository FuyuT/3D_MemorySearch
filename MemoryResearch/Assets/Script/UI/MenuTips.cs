using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTips : MonoBehaviour
{
    //表示させたいTips//////////////////
    [SerializeField]
    GameObject tips;

    [SerializeField]
    GameObject tips2;

    [SerializeField]
    GameObject tips3;

    [SerializeField]
    GameObject tips4;
    ///////////////////////////////////

    //Tipsのカウント
    int TipsCount;

    // Start is called before the first frame update
    void Start()
    {
        TipsCount = 0;
        tips.SetActive(true);
        tips2.SetActive(false);
        tips3.SetActive(false);
        tips4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            TipsCount += 1;
        }
        DipsUpdate();
    }

    void DipsUpdate()
    {
        if (TipsCount == 0)
        {
            tips.SetActive(true);
        }
        else if (TipsCount == 1)
        {
            tips2.SetActive(true);
        }
        else if (TipsCount == 2)
        {
            tips3.SetActive(true);
        }
        else if (TipsCount == 3)
        {
            tips4.SetActive(true);
        }
        else
        {
            TipsCount = 0;
            tips2.SetActive(false);
            tips3.SetActive(false);
            tips4.SetActive(false);
        }
     
    }

    public  void ButtonCount()
    {
        TipsCount += 1;
    }
}
