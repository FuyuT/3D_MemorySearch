using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionSELECT : MonoBehaviour
{
    //メニューパネル
    [SerializeField] GameObject MenuPanel;

    //オプションパネル
    [SerializeField] GameObject OptionPanel;
    //メニューパネルの表示用
    bool show;

    //メニューパネルでキー入力でのカーソル
    int select;

    // Start is called before the first frame update
    void Start()
    {
        select = 0;
      
        OptionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("down"))
        {
            select += 1;
        }

        if (select > 2)
        {
            select = 0;
        }
        else if (select < 0)
        {
            select = 2;
        }


        if (select == 0)
        {
            if (Input.GetKeyDown("z"))
            {
               
            }
        }

        if (select == 1)
        {
            if (Input.GetKeyDown("z"))
            {
                
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

    //ゲームに戻る（メニューを閉じる）関数
    public void SelectReturnButton()
    {

        OptionPanel.SetActive(false);
        MenuPanel.SetActive(true);
        show = false;
    }

   
    
}
