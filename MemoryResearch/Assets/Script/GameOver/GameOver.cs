using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    //�Q�[���I�[�o�[�p�l��
    [SerializeField]
    GameObject GameOverPanel;

    //�Q�[���I�[�o�[�A�C�e��
    [SerializeField]
    GameObject GameOverItem;

    //�p�l���̕\���p
    bool show;

    //�p�l���ŃL�[���͂ł̃J�[�\��
    int select;

    //Out�X�N���v�g
    Out script;                   

    // Start is called before the first frame update
    void Start()
    {
        select = 0;
        show = false;

        script = GameOverPanel.GetComponent<Out>();

       GameOverItem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //�Q�[���I�[�o�[�\��
        if (!show)
        {
            if (Input.GetKey("f1"))
            {
                script.IsFadeOut = true;
                GameOverItem.SetActive(true);
                show = true;
            }
        }

        if (show)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                select += 1;
            }
            else if (Input.GetKeyDown("left"))
            {
                select -= 1;
            }

            if (select > 1)
            {
                select = 0;
            }
            else if (select < 0)
            {
                select = 1;
            }

            if (select == 0)
            {
                if (Input.GetKeyDown("z"))
                {
                    SelectTitleButton();
                }
            }

            if (select == 1)
            {
                if (Input.GetKeyDown("z"))
                {
                    SelectRetry();
                }
            }
        }
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

}
