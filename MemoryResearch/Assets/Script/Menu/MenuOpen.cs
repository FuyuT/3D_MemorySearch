using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuOpen : MonoBehaviour
{
    //メニューパネル
    GameObject menu;

    //オプションパネル
    GameObject option;

    //メニューパネルの表示用
    bool show;

    // Start is called before the first frame update
    void Start()
    {

        menu = GameObject.Find("Menu");
        menu.SetActive(false);

        option = GameObject.Find("OptionPanel");
        // option.SetActive(false);

        show = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!show)
        {
            if (Input.GetKey("o"))
            {
                OnMenu();
            }
        }
    }

    public void OnMenu()
    {
        menu.SetActive(true);
        show = true;
    }

    public void OffMnenu()
    {
        menu.SetActive(false);
        show = false;
    }
}
