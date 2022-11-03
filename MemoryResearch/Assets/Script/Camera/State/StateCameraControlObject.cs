using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<CameraManager>;

public class StateCameraControlObject : State
{
    protected override void OnEnter(State prevState)
    {
    }
    protected override void OnUpdate()
    {
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //spaceを押したらFPSカメラ
        if (Input.GetKeyDown("v"))
        {
            stateMachine.Dispatch((int)CameraManager.CameraType.Floor);
        }
    }
}
