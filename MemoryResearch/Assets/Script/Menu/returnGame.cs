using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnGame : MonoBehaviour
{
    [SerializeField]
    GameObject MenuPanel;

    [SerializeField]
    GameObject OptionPanel;


    //public GameObject showPanel;
    //bool                 inMenu;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    inMenu = false;

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (inMenu)
    //    {
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            FindObjectOfType<MenuOpen>().OffMnenu();
    //        }
    //    }
    //}

    //void FixedUpdate()
    //{

    //}

    //private void OnCollisionEnter(Collision col)
    //{
    //    if(col.gameObject==showPanel)
    //    {
    //        inMenu = true;
    //            Debug.Log("a");
    //            Update();

    //    }
    //    else
    //    {
    //        inMenu = false;
    //    }
    //}

    //public void ButtonClicked()
    //{
    //    //MenuPanel = GetComponent<MenuOpen>(true).OffMnenu();
    //    FindObjectOfType<MenuOpen>().OffMnenu();
    //}

    //public void ButtonClicked2()
    //{
    //    OptionPanel.SetActive(false);
    //    MenuPanel.SetActive(true);
    //    FindObjectOfType<MenuOpen>().OnMenu();
    //}
}
