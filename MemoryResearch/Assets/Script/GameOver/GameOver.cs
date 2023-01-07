using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomInputKey;

public class GameOver : MonoBehaviour
{
    //�Q�[���I�[�o�[�p�l��
    [SerializeField]
    GameObject GameOverImage;

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
        GameOverImage.SetActive(false);
        select = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameOverImage.activeSelf)
        {
            return;
        }
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

    public void OnGameOver()
    {
        GameOverImage.SetActive(true);
    }

    public void OffGameOver()
    {
        GameOverImage.SetActive(false);
    }
}

