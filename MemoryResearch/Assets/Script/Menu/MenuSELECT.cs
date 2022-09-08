using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSELECT : MonoBehaviour
{
    //���j���[�p�l��
    [SerializeField] GameObject   MenuPanel;

    //�I�v�V�����p�l��
    [SerializeField] GameObject OptionPanel;
    //���j���[�p�l���̕\���p
    bool                               show;

    //���j���[�p�l���ŃL�[���͂ł̃J�[�\��
    int                              select;
  
    // Start is called before the first frame update
    void Start()
    {
        select = 0;
        show = false;

        MenuPanel.SetActive(false);
        OptionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //���j���[���J��
        if (!show)
        {
            if (Input.GetKey("o"))
            {
                OnMenu();
            }
        }

        if (Input.GetKeyDown("down"))
        {
            select += 1;
        }

        if(select>2)
        {
            select = 0;
        }
        else if(select<0)
        {
            select = 2;
        }

        if (select == 0)
        {
            if (Input.GetKeyDown("z"))
            {
                SelectOptionButton();
            }
        }

        if (select == 1)
        {
            if (Input.GetKeyDown("z"))
            {
                SelectTitleButton();
            }
        }

        if (select == 2)
        {
            if (Input.GetKeyDown("z"))
            {
                SelectReturnButton();
            }
        }
    }

    //�I�v�V�����p�l�����J���֐�
    public void SelectOptionButton()
    {
        MenuPanel.SetActive(false);
        OptionPanel.SetActive(true);
        select = 0;
    }

    //�^�C�g����ʂɖ߂�֐�
    public void SelectTitleButton()
    {
        //MenuPanel.SetActive(false);
        FadeManager.Instance.LoadScene("Titel", 1.0f);
    }

    //�Q�[���ɖ߂�i���j���[�����j�֐�
    public void SelectReturnButton()
    {
        MenuPanel.SetActive(false);
        show = false;
    }

    //���j���[���J��
    public void OnMenu()
    {
        MenuPanel.SetActive(true);
        show = true;
    }
}
