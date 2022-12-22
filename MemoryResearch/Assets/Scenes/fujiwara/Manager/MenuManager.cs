using UnityEngine;

public class MenuManager : MonoBehaviour
{
    ///******************************
    /// private

    MenuType nowMenu;
    MenuType nextMenu;

    [SerializeField] GameObject Inventory;
    [SerializeField] GameObject MemoryCobine;
    [SerializeField] GameObject System;
    [SerializeField] GameObject Option;

    bool isOpen;

    MenuManager()
    {
        nowMenu = MenuType.None;
        nextMenu = MenuType.Inventory;

        isOpen = false;
    }

    void Start()
    {
        Option.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isOpen)
            {
                CloseMenu();
            }
            else
            {
                nowMenu = MenuType.None;
                nextMenu = MenuType.Inventory;
                //Time.timeScale = 0f;
            }
            isOpen = !isOpen;
        }

        //メニューを開いていなければ終了
        if (!isOpen) return;

        ChangeMenu();
    }

    void ChangeMenu()
    {
        if (nowMenu == nextMenu) return;

        Inventory.SetActive(false);
        MemoryCobine.SetActive(false);
        System.SetActive(false);

        switch (nextMenu)
        {
            case MenuType.Inventory:
                Inventory.SetActive(true);
                break;
            case MenuType.Memoty_Cobine:
                MemoryCobine.SetActive(true);
                break;
            case MenuType.System:
                System.SetActive(true);
                break;
            default:
                break;
        }

        nowMenu = nextMenu;
    }

    void CloseMenu()
    {
        Inventory.SetActive(false);
        MemoryCobine.SetActive(false);
        System.SetActive(false);
        Option.SetActive(false);
        Time.timeScale = 1.0f;
    }

    ///******************************
    /// public

    public enum MenuType
    {
        None,
        Inventory,
        Memoty_Cobine,
        System,
    }

    //ボタンを押した時の処理
    public void ToInventory()
    {
        nextMenu = MenuType.Inventory;
    }

    public void ToMemoryConbine()
    {
        nextMenu = MenuType.Memoty_Cobine;
    }

    public void ToSystem()
    {
        nextMenu = MenuType.System;
    }

    public void ToEnd()
    {
        CloseMenu();
        isOpen = false;
    }

    public void OpenOption()
    {
        if (!Option.activeSelf)
        {
            Option.SetActive(true);
        }
        else
        {
            Option.SetActive(false);
        }
    }
}
