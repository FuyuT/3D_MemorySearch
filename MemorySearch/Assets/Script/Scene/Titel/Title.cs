using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CustomInputKey;


//����
//space�����܂Ń{�^���͔�\��
//��������{�^���\�����ăA�j���[�V�����J�n

public class Title : MonoBehaviour
{
  
    [Header("�v�b�V�����S")]
    [SerializeField]
    GameObject PushLogo;

    [Header("�{�^����")]
    [SerializeField]
    GameObject[] Buttons;

    [Header("�Q�[���I���p�l��")]
    [SerializeField]
    GameObject GameExitPanel;

    ////BGM�ESE�֘A
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
