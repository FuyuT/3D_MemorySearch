using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleGame : MonoBehaviour
{
    [SerializeField]
    GameObject TitlePanel;

    [SerializeField]
    GameObject SelectPanel;

    [SerializeField]
    GameObject ContinuedPanel;

    [SerializeField]
    GameObject OptionPanel;

    //[SerializeField]
    //GameObject TitlePanel;

    //[SerializeField]
    //GameObject TitlePanel;

    bool Show;
    // Start is called before the first frame update
    void Start()
    {

        Show = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            TitlePanel.SetActive(false);
            SelectPanel.SetActive(true);
            Show = false;
        }

        //if(Show)
        //{
        //    if (Input.GetKeyDown("down"))
        //    {
        //        select += 1;
        //    }

        //    if (select > 2)
        //    {
        //        select = 0;
        //    }
        //    else if (select < 0)
        //    {
        //        select = 2;
        //    }

        //    if (select == 0)
        //    {
        //        if (Input.GetKeyDown("z"))
        //        {
        //            SelectOptionButton();
        //        }
        //    }

        //    if (select == 1)
        //    {
        //        if (Input.GetKeyDown("z"))
        //        {
        //            SelectTitleButton();
        //        }
        //    }

        //    if (select == 2)
        //    {
        //        if (Input.GetKeyDown("z"))
        //        {
        //            SelectReturnButton();
        //        }
        //    }
        //}
        
    }

   public void ButtonStartClick()
   {
     FadeManager.Instance.LoadScene("SampleScene", 1.0f);
   }

    public void ContinuedClick()
    {

    }

    public void OptionClick()
    {

    }
}
