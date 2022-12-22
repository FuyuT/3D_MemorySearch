using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomInputKey;

public class GameOver : MonoBehaviour
{
    //�Q�[���I�[�o�[�p�l��
    [SerializeField]
    GameObject GameOverImage;

    ////�Q�[���I�[�o�[�A�C�e��
    //[SerializeField]
    //GameObject GameOverItemPabel;

    ////�p�l���̕\���p
    //bool show;

    ////�p�l���ŃL�[���͂ł̃J�[�\��
    //int select;

    ////Out�X�N���v�g
    //PanelOut OutScript;

    //PanelIn  InScript;

    // Start is called before the first frame update
    void Start()
    {
        //select = 0;
        //show = false;

        //OutScript = GameOverPanel.GetComponent<PanelOut>();

        //InScript = GameOverItemPabel.GetComponent<PanelIn>();

        GameOverImage.SetActive(false);

        //GameOverItemPabel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CustomInput.Interval_InputKeydown(KeyCode.F1, 1))
        {
            if (!GameOverImage.activeSelf)
            {
                OnGameOver();
            }
            else
            {
                OffGameOver();
            }
        }

        //if (show)
        //{
        //    if (Input.GetKeyDown(KeyCode.RightArrow))
        //    {
        //        select += 1;
        //    }
        //    else if (Input.GetKeyDown("left"))
        //    {
        //        select -= 1;
        //    }

        //    if (select > 1)
        //    {
        //        select = 0;
        //    }
        //    else if (select < 0)
        //    {
        //        select = 1;
        //    }

        //    if (select == 0)
        //    {
        //        if (Input.GetKeyDown("z"))
        //        {
        //            SelectTitleButton();
        //        }
        //    }

        //    if (select == 1)
        //    {
        //        if (Input.GetKeyDown("z"))
        //        {
        //            SelectRetry();
        //        }
        //    }
        //}
    }

    ////�Q�[���I�[�o�[�\���̊֐�
    //public void OnGameOver()
    //{
    //    fadeout = true;

    //    alpha += 0.01f;
    //    fadealpha.color = new Color(0, 0, 0, alpha);
    //    if (alpha >= 1)
    //    {
    //        GameOverPanel.SetActive(true);
    //        show = true;
    //        fadeout = false;
    //    }
    //}

    //�^�C�g����ʂɖ߂�֐�
    public void SelectTitleButton()
    {
        //MenuPanel.SetActive(false);
        FadeManager.Instance.LoadScene("Titel", 1.0f);
    }

    //�^�C�g����ʂɖ߂�֐�
    public void SelectRetry()
    {
        //MenuPanel.SetActive(false);
        FadeManager.Instance.LoadScene("SampleScene", 1.0f);
    }

    public void OnGameOver()
    {
        GameOverImage.SetActive(true);
    }

    public void OffGameOver()
    {
        GameOverImage.SetActive(false);
    }

}
