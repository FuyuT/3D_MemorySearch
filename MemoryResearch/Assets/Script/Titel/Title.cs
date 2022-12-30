using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CustomInputKey;

public class Title : MonoBehaviour
{
    //パネル類///////////////
    [Header("タイトルパネル")]
    [SerializeField]
    GameObject TitlePanel;

    [Header("メニューパネル")]
    [SerializeField]
    GameObject MenuPanel;

    [Header("オプションパネル")]
    [SerializeField]
    GameObject OptionPanel;
    ///////////////////////////

    //メニューパネル関連
    public MenuSELECT Menuselect;
    [SerializeField] GameObject Menu;


    MyUtil.StateMachine<Title> stateMachine;
    public int GetCurrentPanelType() { return stateMachine.currentStateKey; }

    public enum PanelType
    {
        None,
        Titel,
        Menu,
        Option
    }
    PanelType nowPanel;


    //bool Show;

    ////項目選択の数値
    //int Select;

    ////項目パネルが表示してから操作できるまで用の時間
    //public float ShowTime;

    //PanelofIn InScript;

    // Start is called before the first frame update
    void Start()
    {
        //Menuselect = Menu.GetComponent<MenuSELECT>();
        StateMachineInit();
        ChangePanel();
    }

    void StateMachineInit()
    {
        stateMachine = new MyUtil.StateMachine<Title>(this);

        stateMachine.AddAnyTransition<StateTitel>((int)PanelType.Titel);
        stateMachine.AddAnyTransition<StateMenu>((int)PanelType.Menu);
        stateMachine.AddAnyTransition<StateOption>((int)PanelType.Option);

        stateMachine.Start(stateMachine.GetOrAddState<StateTitel>());
        stateMachine.currentStateKey = (int)PanelType.Titel;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        ChangePanel();

        //if (Input.GetKeyDown("return") || Input.GetMouseButtonDown(0))
        //{
        //    TitlePanel.SetActive(false);
        //    MenuPanel.SetActive(true);
        //    // InScript.IsFadeIn = true;
        //    Show = true;

        //    //StartButton.Select();

        //}

        //if (Show)
        //{
        //    ShowTime -= Time.deltaTime;
        //}

        //if (ShowTime <= 0)
        //{

        //    if (Input.GetKeyDown("down"))
        //    {
        //        Select += 1;

        //    }
        //    else if (Input.GetKeyDown("up"))
        //    {
        //        Select -= 1;

        //    }

        //    if (Select > 2)
        //    {
        //        Select = 0;
        //    }
        //    else if (Select < 0)
        //    {
        //        Select = 2;
        //    }

        //    if (CustomInput.Interval_InputKeydown(KeyCode.Return, 2))
        //    {
        //        if (Select == 0)
        //        {
        //            SelectGameStart();
        //        }

        //        if (Select == 1)
        //        {
        //            SelectContinued();
        //        }

        //        if (Select == 2)
        //        {
        //            SelectOption();
        //        }
        //    }
        //}
    }

    void AllPanelInit()
    {
        TitlePanel.SetActive(false);
        MenuPanel.SetActive(false);
        OptionPanel.SetActive(false);
    }

    void ChangePanel()
    {
        if ((int)nowPanel == stateMachine.currentStateKey)
            return;

        AllPanelInit();

        switch (stateMachine.currentStateKey)
        {
            case (int)PanelType.Titel:
                TitlePanel.SetActive(true);
                break;

            case (int)PanelType.Menu:
                MenuPanel.SetActive(true);
                break;

            case (int)PanelType.Option:
              OptionPanel.SetActive(true);
                break;
        }
        nowPanel = (PanelType)stateMachine.currentStateKey;
    }

    ////項目新しく始めるを選んだ場合
    //public void SelectGameStart()
    //{
    //    FadeManager.Instance.LoadScene("Game", 1.0f);
    //}

    ////項目続きからを選んだ場合
    //public void SelectContinued()
    //{

    //}

    ////項目オプションを選んだ場合
    //public void SelectOption()
    //{
    //    // SelectPanel.SetActive(false);
    //    //OptionPanel.SetActive(true);
    //}
}
