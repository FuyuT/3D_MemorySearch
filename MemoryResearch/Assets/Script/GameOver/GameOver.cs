using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomInputKey;

public class GameOver : MonoBehaviour
{
    //ゲームオーバーパネル
    [SerializeField]
    GameObject GameOverImage;

    //ボタン類///////////////
    [Header("続ける")]
    [SerializeField]
    GameObject RetryButton;

    [Header("やめる")]
    [SerializeField]
    GameObject NoRetryButton;

    //メニューパネルでキー入力でのカーソル
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
        //キー入力で項目選択
       
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

