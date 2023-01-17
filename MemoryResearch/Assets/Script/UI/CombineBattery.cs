using UnityEngine;
public class CombineBattery
{
    /*******************************
    * private
    *******************************/
    const float Charge_Max = 1.0f;

    float chargeValue;    //バッテリーのチャージの値
    int   batteryCount;   //バッテリーの数

    /*******************************
    * public
    *******************************/

    public CombineBattery()
    {
        chargeValue = 0.0f;
        batteryCount = 10;
    }

    public float GetChargeValue()
    {
        return chargeValue;
    }

    public int GetBatteryCount()
    {
        return batteryCount;
    }

    //バッテリーのチャージ
    public void Charge(float addValue)
    {
        chargeValue += addValue;

        if(chargeValue >= Charge_Max)
        {
            chargeValue = 0;
            batteryCount++;
        }
    }

    //バッテリーを使用
    public void UseBattery(int useCount)
    {
        batteryCount -= useCount;
    }
}
