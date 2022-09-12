using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    //ゲームオーバーパネル
    [SerializeField]
    GameObject GameOverPanel;

    //パネルの表示用
    bool show;

    //パネルでキー入力でのカーソル
    int select;

    private float alpha;           //パネルのalpha値取得変数

    private bool fadeout;          //フェードアウトのフラグ変数

    Image fadealpha;               //フェードパネルのイメージ取得変数


    // Start is called before the first frame update
    void Start()
    {
        select = 0;
        show = false;

      GameOverPanel.SetActive(false);

        fadealpha = GameOverPanel.GetComponent<Image>();
        alpha = fadealpha.color.a;
        fadeout = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ゲームオーバー表示
        if (!show)
        {
            if (Input.GetKey("f1"))
            {
                OnGameOver();
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            select += 1;
        }
        else if(Input.GetKeyDown("left"))
        {
            select -= 1;
        }

        if (select > 1)
        {
            select = 0;
        }
        else if (select < 0)
        {
            select = 1;
        }

        if (select == 0)
        {
            if (Input.GetKeyDown("z"))
            {
                SelectTitleButton();
            }
        }

        if (select == 1)
        {
            if (Input.GetKeyDown("z"))
            {
                SelectRetry();
            }
        }
    }


    //ゲームオーバー表示の関数
    public void OnGameOver()
    {
        fadeout = true;

        alpha += 0.01f;
        fadealpha.color = new Color(0, 0, 0, alpha);
        if (alpha >= 1)
        {
            GameOverPanel.SetActive(true);
            show = true;
            fadeout = false;
        }
    }

    //タイトル画面に戻る関数
    public void SelectTitleButton()
    {
        //MenuPanel.SetActive(false);
        FadeManager.Instance.LoadScene("Titel", 1.0f);
    }

    //タイトル画面に戻る関数
    public void SelectRetry()
    {
        //MenuPanel.SetActive(false);
        FadeManager.Instance.LoadScene("SampleScene", 1.0f);
    }


}
