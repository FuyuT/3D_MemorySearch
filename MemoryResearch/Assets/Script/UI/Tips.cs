using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{
    //Tips�̐e�I�u�W�F�N�g
    [SerializeField]
    GameObject UITips;

    //�\����������Tips//////////////////
    [SerializeField]
    GameObject tips;

    [SerializeField]
    GameObject tips2;

    [SerializeField]
    GameObject tips3;

    [SerializeField]
    GameObject tips4;
    ///////////////////////////////////

    //Tips�̃J�E���g
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
        if(Input.GetKeyUp(KeyCode.Return))
        {
            Debug.Log("a");
            TipsCount +=1;
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
            UITips.SetActive(false);
        }
    }
}
