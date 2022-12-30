using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MemoryUI : MonoBehaviour
{
    //////////////////////////////
    /// protected
    [SerializeField] protected MemorySelectManager.MoveType moveType;

    [SerializeField] protected MemoryType memoryType;

    protected DataManager dataManager;

    void Awake()
    {
        dataManager = GameObject.FindWithTag("DataManager").GetComponent<DataManager>();
    }

    //////////////////////////////
    /// public

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
        GetComponent<Image>().sprite = dataManager.GetMemorySprite(memoryType);
        dataManager.testSave();
    }

}
