using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CustomInputKey;


//メモ
//space押すまでボタンは非表示
//押したらボタン表示してアニメーション開始

public class Title : MonoBehaviour
{
  
    [Header("プッシュロゴ")]
    [SerializeField]
    GameObject PushLogo;

    [Header("ボタン類")]
    [SerializeField]
    GameObject[] Buttons;

    [Header("ゲーム終了パネル")]
    [SerializeField]
    GameObject GameExitPanel;

    ////BGM・SE関連
    //[SerializeField]
    //AudioClip clip;
    //[SerializeField]
    //AudioClip BotannSE;


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

    // Start is called before the first frame update
    void Start()
    {
        StateMachineInit();

        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].SetActive(false);
        }

        GameExitPanel.SetActive(false);

        Cursor.visible = true;
    }

    void StateMachineInit()
    {
        stateMachine = new MyUtil.StateMachine<Title>(this);

        stateMachine.AddAnyTransition<StateTitel>((int)PanelType.Titel);
        stateMachine.AddAnyTransition<StateMenu>((int)PanelType.Menu);

        stateMachine.Start(stateMachine.GetOrAddState<StateTitel>());
        stateMachine.currentStateKey = (int)PanelType.Titel;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 1.0f;
        stateMachine.Update();
        PushSpase();

        if(Input.GetKeyDown(KeyCode.Backspace) 
            && Input.GetKey(KeyCode.LeftControl))
        {
            DataManager.instance.IniPossesiontMemoryData();
            DataManager.instance.Save();
        }
    }

    void PushSpase()
    {
        if (!PushLogo.activeSelf) return;
        
        if (Input.GetKeyDown("space"))
        {
            PushLogo.SetActive(false);
            for(int i=0;i< Buttons.Length;i++)
            {
                Buttons[i].SetActive(true);
            }
        }
    }

    public void ExitPanel()
    {
        if(!GameExitPanel.activeSelf)
        {
            GameExitPanel.SetActive(true);
        }
    }

   
}
