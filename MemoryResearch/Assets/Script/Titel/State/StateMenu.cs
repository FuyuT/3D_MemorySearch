using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Title>;
public class StateMenu : State
{
    protected override void OnUpdate()
    {
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        if (Owner.Menuselect.OptionIn)
        {
            stateMachine.Dispatch((int)Title.PanelType.Option);
        }

        if (Owner.Menuselect.Titelreturn)
        {
            stateMachine.Dispatch((int)Title.PanelType.Titel);
            Owner.Menuselect.Titelreturn = false;
        }
    }
}



