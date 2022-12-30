using UnityEngine;

public class MemoryEquipmentUI : MonoBehaviour
{
    /*******************************
    * private
    *******************************/
    [SerializeField] MemoryUI[] MemoriesUI;

    private void Update()
    {
        for (int n = 0; n < MemoriesUI.Length; n++)
        {
            //装備が変わっていれば、データに反映
            if (DataManager.instance.IPlayerData().GetEquipmentMemory(n) != MemoriesUI[n].GetMemoryType())
            {
                DataManager.instance.IPlayerData().SetEquipmentMemory(n, MemoriesUI[n].GetMemoryType());
            }
        }
    }

    private void OnEnable()
    {
        //Activeになった時に、装備しているメモリをUIに反映させる
        SetEquipmentMemoriesUI();
    }

    private void SetEquipmentMemoriesUI()
    {
        //装備データをUIに反映
        for (int n = 0; n < MemoriesUI.Length; n++)
        {
            MemoriesUI[n].ChangeMemoryType(DataManager.instance.IPlayerData().GetEquipmentMemory(n));
        }
    }
}
