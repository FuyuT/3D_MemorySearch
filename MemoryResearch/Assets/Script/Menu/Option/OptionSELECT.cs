using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionSELECT : MonoBehaviour
{
    //���j���[�p�l��
    [SerializeField] GameObject MenuPanel;

    //�I�v�V�����p�l��
    [SerializeField] GameObject OptionPanel;
    //���j���[�p�l���̕\���p
    bool show;

    //���j���[�p�l���ŃL�[���͂ł̃J�[�\��
    int select;

    // Start is called before the first frame update
    void Start()
    {
        select = 0;
      
        OptionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("down"))
        {
            select += 1;
        }

        if (select > 2)
        {
            select = 0;
        }
        else if (select < 0)
        {
            select = 2;
        }


        if (select == 0)
        {
            if (Input.GetKeyDown("z"))
            {
               
            }
        }

        if (select == 1)
        {
            if (Input.GetKeyDown("z"))
            {
                
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

    //�Q�[���ɖ߂�i���j���[�����j�֐�
    public void SelectReturnButton()
    {

        OptionPanel.SetActive(false);
        MenuPanel.SetActive(true);
        show = false;
    }

   
    
}
