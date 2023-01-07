using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomInputKey;

public class MenuOpen : MonoBehaviour
{
    [Header("メニューパネル")]
    [SerializeField] GameObject MenuPanel;

    //メニューパネルの表示用
    bool show;

    // Start is called before the first frame update
    void Start()
    {
        MenuPanel.SetActive(false);
        show = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CustomInput.Interval_InputKeydown(KeyCode.O, 1))
        {
            if (!MenuPanel.activeSelf)
            {
                OnMenu();
            }
            else
            {
                OffMenu();
            }
        }
    }

    public void OnMenu()
    {
        MenuPanel.SetActive(true);
    }

    public void OffMenu()
    {
        MenuPanel.SetActive(false);
    }
}
