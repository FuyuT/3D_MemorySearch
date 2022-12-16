using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Title>;
public class StateTitel : State
{
    protected override void OnUpdate()
    {
        SelectNextState();
    }

    protected override void SelectNextState()
    {
       if(Input.GetButtonDown("space"))
       {
          stateMachine.Dispatch((int)Title.PanelType.Menu);
       }
    }
}
