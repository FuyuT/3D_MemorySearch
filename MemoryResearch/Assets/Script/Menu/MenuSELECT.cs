using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSELECT : MonoBehaviour
{
    //メニューパネル
    [SerializeField] GameObject   MenuPanel;

    //オプションパネル
    [SerializeField] GameObject OptionPanel;
    //メニューパネルの表示用
    bool                               show;

    //メニューパネルでキー入力でのカーソル
    int                              select;
  
    // Start is called before the first frame update
    void Start()
    {
        select = 0;
        show = false;

        MenuPanel.SetActive(false);
        OptionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //メニューを開く
        if (!show)
        {
            if (Input.GetKey("o"))
            {
                OnMenu();
            }
        }

        if (Input.GetKeyDown("down"))
        {
            select += 1;
        }

        if(select>2)
        {
            select = 0;
        }
        else if(select<0)
        {
            select = 2;
        }

        if (select == 0)
        {
            if (Input.GetKeyDown("z"))
            {
                SelectOptionButton();
            }
        }

        if (select == 1)
        {
            if (Input.GetKeyDown("z"))
            {
                SelectTitleButton();
            }
        }

        if (select == 2)
        {
            if (Input.GetKeyDown("z"))
            {
                SelectReturnButton();
            }
        }
    }

    //オプションパネルを開く関数
    public void SelectOptionButton()
    {
        MenuPanel.SetActive(false);
        OptionPanel.SetActive(true);
        select = 0;
    }

    //タイトル画面に戻る関数
    public void SelectTitleButton()
    {
        //MenuPanel.SetActive(false);
        FadeManager.Instance.LoadScene("Titel", 1.0f);
    }

    //ゲームに戻る（メニューを閉じる）関数
    public void SelectReturnButton()
    {
        MenuPanel.SetActive(false);
        show = false;
    }

    //メニューを開く
    public void OnMenu()
    {
        MenuPanel.SetActive(true);
        show = true;
    }
}
