using UnityEngine;

public class EnemyBase : CharaBase
{
    /*******************************
    * protected
    *******************************/
    protected MemoryType mainMemory;
    protected MemoryType subMemory;

    /*******************************
    * public
    *******************************/

    public EnemyBase()
    {
        mainMemory = new MemoryType();
        subMemory  = new MemoryType();
    }

    public void InitSubMemory()
    {
        subMemory = MemoryType.None;
    }

    public MemoryType GetMemory()
    {
        if(subMemory == MemoryType.None)
        {
            return mainMemory;
        }
        return subMemory;
    }

    public void SetSubMemory(MemoryType memory)
    {
        this.subMemory = memory;
    }
}
