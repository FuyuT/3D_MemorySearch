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

    [Header("�I�v�V�����p�l��")]
    [SerializeField]
    GameObject OptionPanel;

    //���j���[�p�l���֘A
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

    // Start is called before the first frame update
    void Start()
    {
        //Menuselect = Menu.GetComponent<MenuSELECT>();
        StateMachineInit();
        //ChangePanel();

        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].SetActive(false);
        }
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
        stateMachine.Update();
        //ChangePanel();
        PushSpase();
    }

    //void AllPanelInit()
    //{
    //    TitlePanel.SetActive(false);
    //    MenuPanel.SetActive(false);
    //    OptionPanel.SetActive(false);
    //}

    //void ChangePanel()
    //{
    //    if ((int)nowPanel == stateMachine.currentStateKey)
    //        return;

    //    AllPanelInit();

    //    switch (stateMachine.currentStateKey)
    //    {
    //        case (int)PanelType.Titel:
    //            TitlePanel.SetActive(true);
    //            break;

    //        case (int)PanelType.Menu:
    //            MenuPanel.SetActive(true);
    //            break;

    //        case (int)PanelType.Option:
    //          OptionPanel.SetActive(true);
    //            break;
    //    }
    //    nowPanel = (PanelType)stateMachine.currentStateKey;
    //}

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
}
