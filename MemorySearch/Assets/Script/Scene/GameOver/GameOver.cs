using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomInputKey;

public class GameOver : MonoBehaviour
{

    //�{�^����///////////////
    [Header("������")]
    [SerializeField]
    GameObject RetryButton;

    [Header("��߂�")]
    [SerializeField]
    GameObject NoRetryButton;
   
    //���j���[�p�l���ŃL�[���͂ł̃J�[�\��
    int select;

    void Start()
    {
        select = 0;
    }

    void Update()
    {
        MenuSelect();
        SelectProject();
    }

    void MenuSelect()
    {
        //�L�[���͂ō��ڑI�� 
        if (Input.GetKeyDown("left"))
        {
            select -= 1;
        }
        if (Input.GetKeyDown("right"))
        {
            select += 1;
        }

        if (select > 1)
        {
            select = 0;
        }
        else if (select < 0)
        {
            select = 1;
        }
    }

    void SelectProject()
    {
        if (Input.GetKeyDown("return") && select == 0)
        {
            Retry();
        }
        else if (Input.GetKeyDown("return") && select == 1)
        {
            NoRetry();
        }
    }

    public void Retry()
    {
        FadeManager.Instance.LoadScene("Game", 1.0f);
    }

    public void NoRetry()
    {
        FadeManager.Instance.LoadScene("Titel", 1.0f);

    }
}

