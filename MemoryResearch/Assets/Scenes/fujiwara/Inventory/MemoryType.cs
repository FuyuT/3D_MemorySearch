using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MemoryTypeEnum
{
    None,
    Jump,
    Dush,
    DowbleJump,
    AirDush,
}

public class MemoryType : MonoBehaviour
{
    //////////////////////////////
    /// private
    //////////////////////////////

    [SerializeField] MemoryTypeEnum memoryType;


    //////////////////////////////
    /// public
    //////////////////////////////

    public MemoryTypeEnum GetMemoryType()
    {
        return memoryType;
    }
}
