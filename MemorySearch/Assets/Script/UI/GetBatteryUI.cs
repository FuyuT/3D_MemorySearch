using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetBatteryUI : MonoBehaviour
{
    [SerializeField]
    BatteryCountUI BatteryAddtUI;

    [SerializeField]
    public Animator Startanimator;

    public void AddBatteu(float addCount)
    {
        BatteryAddtUI.SetBatteryCount(addCount);
    }

    //void PlayGetAnim()
    //{
    //    BehaviorAnimation.UpdateTrigger(ref animator, "");
    //}

    //void StopGetAnim()
    //{
    //    BehaviorAnimation.UpdateTrigger(ref animator, "");
    //}
}
