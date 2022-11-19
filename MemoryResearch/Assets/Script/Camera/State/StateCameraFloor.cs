using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<CameraManager>;

public class StateCameraFloor : State
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
        if (Input.GetKeyDown("space"))
        {
            stateMachine.Dispatch((int)CameraManager.CameraType.FPS);
        }

        //フロアカメラ
        if (Owner.FloorCameraArea.inArea)
        {
            stateMachine.Dispatch((int)CameraManager.CameraType.TPS);
        }

        if (Input.GetKeyDown("v") && Owner.MoveObjCamScript.ChangFlg)
        {
            stateMachine.Dispatch((int)CameraManager.CameraType.Controller);
            
        }
    }
}
