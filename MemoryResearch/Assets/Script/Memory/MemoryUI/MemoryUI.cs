using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MemoryUI : MonoBehaviour
{
    /*******************************
    * protected
    *******************************/
    [SerializeField] protected MemorySelectManager.MoveType moveType;

    [SerializeField] protected MemoryType memoryType;

    /*******************************
    * public
    *******************************/

    //getter

    public MemorySelectManager.MoveType GetMoveType()
    {
        return moveType;
    }

    public bool IsTypeNone()
    {
        return memoryType == MemoryType.None ? true : false;
    }

    public MemoryType GetMemoryType()
    {
        return memoryType;
    }

    //setter
    public void ChangeMemoryType(MemoryType type)
    {
        memoryType = type;
        //âÊëúÇïœçX
        GetComponent<Image>().sprite = DataManager.instance.GetMemorySprite(memoryType);

    }

}
