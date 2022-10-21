using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Enemy>;

public class StateEnemyAttack : State
{
    protected override void OnEnter(State prevState)
    {
        //æ“¾‚Å‚«‚éƒƒ‚ƒŠ‚ğİ’è
        Owner.param.Set((int)Enemy.ParamKey.PossesionMemory, (int)Player.Event.Attack_Punch);
    }

    protected override void OnUpdate()
    {
        //UŒ‚
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //todo:“GUŒ‚‚ªI‚í‚Á‚½‚çˆÚ“®‚Ö
        stateMachine.Dispatch((int)Enemy.Event.Move);
    }

    protected override void OnExit(State nextState)
    {
    }
}
