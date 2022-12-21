using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSELECT : MonoBehaviour
{
    ////���j���[�p�l��
    //[SerializeField] GameObject   MenuPanel;

    ////�I�v�V�����p�l��
    //[SerializeField] GameObject OptionPanel;

    //�{�^����///////////////
    [Header("�A�h�x���`���[�{�^��")]
    [SerializeField]
    GameObject AdventureBuuton;

    [Header("�`���[�g���A���{�^��")]
    [SerializeField]
    GameObject TutorialBuuton;

    [Header("�I�v�V�����{�^��")]
    [SerializeField]
    GameObject OptionBuuton;

    [Header("�߂�{�^��")]
    [SerializeField]
    GameObject BackBuuton;
    //////////////////////////////

    //���j���[�p�l���̕\���p
    bool show;

    //���j���[�p�l���ŃL�[���͂ł̃J�[�\��
    int select;

    //�^�C�g����ʂɖ߂�֘A
    float StandbyTime;
    public bool Titelreturn;

    //�I�v�V����
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
        //�L�[���͂ō��ڑI��
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
