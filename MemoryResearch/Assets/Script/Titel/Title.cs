using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CustomInputKey;

public class Title : MonoBehaviour
{
    //�p�l����///////////////
    [Header("�^�C�g���p�l��")]
    [SerializeField]
    GameObject TitlePanel;

    [Header("���j���[�p�l��")]
    [SerializeField]
    GameObject MenuPanel;

    [Header("�I�v�V�����p�l��")]
    [SerializeField]
    GameObject OptionPanel;
    ///////////////////////////

    //���j���[�p�l���֘A
    public MenuSELECT Menuselect;
    [SerializeField] GameObject Menu;


    StateMachine<Title> stateMachine;
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
        ChangePanel();
    }

    void StateMachineInit()
    {
        stateMachine = new StateMachine<Title>(this);

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
}
