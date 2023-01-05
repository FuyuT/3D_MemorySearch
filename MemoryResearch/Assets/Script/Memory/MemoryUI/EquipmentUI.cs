using UnityEngine;

public class EquipmentUI : MemoryUI
{
    /*******************************
    * private
    *******************************/

    private void Awake()
    {
        SetEquipmentMemoriesUI();
    }

    private void Update()
    {
        //使えない時は暗くする

        //使える時は明るくする
    }

    /*******************************
    * protected
    *******************************/
    [SerializeField] protected MemoryUI[] MemoriesUI;

    /*******************************
    * public
    *******************************/
    public void SetEquipmentMemoriesUI()
    {
        //装備データをUIに反映
        for (int n = 0; n < MemoriesUI.Length; n++)
        {
            MemoriesUI[n].ChangeMemoryType(DataManager.instance.IPlayerData().GetEquipmentMemory(n));
        }
    }
}
