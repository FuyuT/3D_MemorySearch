using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.State<Title>;
public class StateOption : State
{
    protected override void OnUpdate()
    {
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //int  
    }
}
