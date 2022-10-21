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

    [SerializeField]
    Button StartButton;

     bool Show;

    //���ڑI���̐��l
    int  Select;

    //���ڃp�l�����\�����Ă��瑀��ł���܂ŗp�̎���
    public float  ShowTime;

    PanelofIn InScript;

    // Start is called before the first frame update
    void Start()
    {
        Show = false;

        Select = 0;

        InScript = SelectPanel.GetComponent<PanelofIn>();

        //�e�p�l���̃X�^�[�g���̕\����//
        TitlePanel.SetActive(true);
        SelectPanel.SetActive(false);
        OptionPanel.SetActive(false);

      
          
        //�{�^�����I�����ꂽ��ԂɂȂ�
        StartButton.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("return") || Input.GetMouseButtonDown(0))
        {
            TitlePanel.SetActive(false);
            SelectPanel.SetActive(true);
            // InScript.IsFadeIn = true;
            Show = true;

            StartButton.Select();

        }

        if(Show)
        {
            ShowTime -= Time.deltaTime;
        }
      
            if (ShowTime <= 0)
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

                if (CustomInput.Interval_InputKeydown(KeyCode.Return, 2))
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

    //���ڐV�����n�߂��I�񂾏ꍇ
   public void SelectGameStart()
   {
     FadeManager.Instance.LoadScene("SampleScene", 1.0f);
   }

   //���ڑ��������I�񂾏ꍇ
   public void SelectContinued()
   {

   }

   //���ڃI�v�V������I�񂾏ꍇ
   public void SelectOption()
   {
      // SelectPanel.SetActive(false);
       //OptionPanel.SetActive(true);
   }
}
