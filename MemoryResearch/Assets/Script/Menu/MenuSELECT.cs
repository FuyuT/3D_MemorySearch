using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSELECT : MonoBehaviour
{
    ////メニューパネル
    //[SerializeField] GameObject   MenuPanel;

    ////オプションパネル
    //[SerializeField] GameObject OptionPanel;

    //ボタン類///////////////
    [Header("アドベンチャーボタン")]
    [SerializeField]
    GameObject AdventureBuuton;

    [Header("チュートリアルボタン")]
    [SerializeField]
    GameObject TutorialBuuton;

    [Header("オプションボタン")]
    [SerializeField]
    GameObject OptionBuuton;

    [Header("戻るボタン")]
    [SerializeField]
    GameObject BackBuuton;
    //////////////////////////////

    //メニューパネルの表示用
    bool show;

    //メニューパネルでキー入力でのカーソル
    int select;

    //タイトル画面に戻る関連
    float StandbyTime;
    public bool Titelreturn;

    //オプション
    public bool OptionIn;

    // Start is called before the first frame update
    void Start()
    {
        select = 0;

        StandbyTime = 60f;
        Titelreturn = false;
        OptionIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        MenuSelect();
        SelectProject();

        Debug.Log(select);
        StandbyTime -= Time.deltaTime;
        if(StandbyTime<0)
        {
            Titelreturn = true;
        }
    }

    void MenuSelect()
    {
        //キー入力で項目選択
        if(Input.GetKeyDown("up"))
        {
            select = 0;
        }
        if (Input.GetKeyDown("down"))
        {
            select = 3;
        }
        if (Input.GetKeyDown("left"))
        {
            select -= 1;
        }
        if (Input.GetKeyDown("right"))
        {
            select += 1;
        }

        if (select > 3)
        {
            select = 0;
        }
        else if (select < 0)
        {
            select = 3;
        }
    }

    void SelectProject()
    {
        if(Input.GetKeyDown("return") && select == 0)
        {
            FadeManager.Instance.LoadScene("Game", 1.0f);
        }
        else if (Input.GetKeyDown("return") && select == 1)
        {
            //FadeManager.Instance.LoadScene("Game", 1.0f);
        }
        else if (Input.GetKeyDown("return") && select == 2)
        {
            OptionIn = true;
        }
        else if (Input.GetKeyDown("return") && select == 3)
        {
            Titelreturn = true;
        }
    }
}
