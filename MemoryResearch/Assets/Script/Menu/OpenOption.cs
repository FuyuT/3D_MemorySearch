using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOption : MonoBehaviour
{
    //オプションパネル
    [SerializeField]
    GameObject MenuOption;

    //メニューパネル
    [SerializeField]
    GameObject MenuPanel;

    //// Start is called before the first frame update
    void Start()
    {
        
        MenuOption.SetActive(false);

    }

    // Update is called once per frame
    //void Update()
    //{

    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        MenuOption.SetActive(true);
    //        MenuPanel.SetActive(false);
    //    }
    //}

    public void ButtonClicked()
    {
        MenuOption.SetActive(true);
        MenuPanel.SetActive(false);
    }

    //public void returnclick()
    //{
    //    MenuOption.SetActive(false);
    //    MenuPanel.SetActive(true);
    //}

}
