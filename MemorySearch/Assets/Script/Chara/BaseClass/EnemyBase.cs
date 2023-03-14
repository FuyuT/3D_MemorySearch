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

    //死亡処理
    public void DropBattery(EnemyType enemyType)
    {
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;

        var batteryItem = (GameObject)Resources.Load("Item/Item_Battery");
        const int AdjustPosY = 3;
        var pos = animTransform.position + new Vector3(0, AdjustPosY, 0);

        float batteryValue = 0.0f;

        switch (enemyType)
        {
            case EnemyType.Flog:
                batteryValue = DataManager.instance.IEnemyData().GetFlogData().dropBattery;
                break;
            case EnemyType.Cow:
                batteryValue = DataManager.instance.IEnemyData().GetCowData().dropBattery;
                break;
            case EnemyType.Crab:
                batteryValue = DataManager.instance.IEnemyData().GetCrabData().dropBattery;
                break;
            case EnemyType.Gorilla:
                batteryValue = DataManager.instance.IEnemyData().GetGorillaData().dropBattery;
                break;
            case EnemyType.Fox:
                batteryValue = DataManager.instance.IEnemyData().GetFoxData().dropBattery;
                break;
        }

        batteryItem.GetComponent<BatteryItem>().Create(pos, batteryValue);
    }
}
