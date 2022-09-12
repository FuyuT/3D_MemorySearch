using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CustomInputKey;

public class TitleGame : MonoBehaviour
{

    [SerializeField]
    GameObject TitlePanel;

    [SerializeField]
    GameObject SelectPanel;

    //[SerializeField]
    //GameObject ContinuedPanel;

    [SerializeField]
    GameObject OptionPanel;


    bool Show;

    //項目選択の数値
    int  Select;

    //項目パネルが表示してから操作できるまで用の時間
    float  ShowTime;

    // Start is called before the first frame update
    void Start()
    {
        Show = true;

        Select = 0;

        ShowTime = 4;

        //各パネルのスタート時の表示状況//
        TitlePanel.SetActive(true);
        SelectPanel.SetActive(false);
        OptionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("return") || Input.GetMouseButtonDown(0))
        {
            TitlePanel.SetActive(false);
            SelectPanel.SetActive(true);

            ShowTime -= Time.deltaTime;
           
        }

      
        if (Show)
        {
            if (ShowTime >= 0)
            {
 
                if (Input.GetKeyDown("down"))
                {
                    Select += 1;
                }
                else if (Input.GetKeyDown("up"))
                {
                    Select -= 1;
                }

                if (Select > 2)
                {
                    Select = 0;
                }
                else if (Select < 0)
                {
                    Select = 2;
                }

                if (CustomInput.Interval_InputKeydown(KeyCode.Return, 3))
                {


                    if (Select == 0)
                    {
                        
                            SelectGameStart();
                        
                    }

                    if (Select == 1)
                    {
                        
                            SelectContinued();
                        
                    }

                    if (Select == 2)
                    {
                        
                            SelectOption();
                        
                    }
                }
            }
        }

    }

    //項目新しく始めるを選んだ場合
   public void SelectGameStart()
   {
     FadeManager.Instance.LoadScene("SampleScene", 1.0f);
   }

   //項目続きからを選んだ場合
   public void SelectContinued()
   {

   }

   //項目オプションを選んだ場合
   public void SelectOption()
   {
       SelectPanel.SetActive(false);
       OptionPanel.SetActive(true);
   }
}
