using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PopUpType
{ 
    None,
    Inventory,
    Memoty_Cobine,
    System,
}

public class PopUpManager : MonoBehaviour
{
    PopUpType nowPopUp;
    [SerializeField] PopUpType nextPopUp;

    [SerializeField] GameObject Inventory;
    [SerializeField] GameObject MemoryCobine;
    [SerializeField] GameObject System;

    //////////////////////////////
    /// setter
    //////////////////////////////

    public void ToInventory()
    {
        nextPopUp = PopUpType.Inventory;
    }

    public void ToMemoryConbine()
    {
        nextPopUp = PopUpType.Memoty_Cobine;
    }

    public void ToSystem()
    {
        nextPopUp = PopUpType.System;
    }

    public void ToEnd()
    {
        nextPopUp = PopUpType.None;
    }

    //////////////////////////////
    /// main
    //////////////////////////////

    private void Awake()
    {
    }

    private void Update()
    {
        ChangePopUp();
    }

    void ChangePopUp()
    {
        if (nowPopUp == nextPopUp) return;

        Inventory.SetActive(false);
        MemoryCobine.SetActive(false);
        System.SetActive(false);

        Debug.Log("’Ê‚Á‚½");

        switch (nextPopUp)
        {
            case PopUpType.Inventory:
                Inventory.SetActive(true);
                break;
            case PopUpType.Memoty_Cobine:
                MemoryCobine.SetActive(true);
                break;
            case PopUpType.System:
                System.SetActive(true);
                break;
            default:
                break;
        }

        nowPopUp = nextPopUp;
    }
}
