using UnityEditor;
using UnityEngine;

//public class PlayerData
//{
//    const int EquipmentMemoryMax = 5;

//    //////////////////////////////
//    /// private

//    MemoryType[] eqipmentMemory;
    
//    //////////////////////////////
//    /// public
//    public PlayerData()
//    {
//        eqipmentMemory = new MemoryType[Global.EquipmentMemoryMax];
//    }

//    public void Load(JsonNode data)
//    {
//        foreach (JsonNode note in data["EquipmentMemories"])
//        {
//            for(int n =0; n < Global.EquipmentMemoryMax; n++)
//            {
//                //todo:データをメモリータイプに変更
//                eqipmentMemory[n] = data["Memory_" + n++].Get<MemoryType>();
//            }
//        }
//    }
//}

public class PlayerData
{

}

public class SaveData
{
    //////////////////////////////
    /// private

    //////////////////////////////
    /// public
    
    public SaveData()
    {
    }

    public void Load(TextAsset dataText)
    {
        Debug.Log(Application.persistentDataPath + "/Data/PlayerData.txt");
    }
}
