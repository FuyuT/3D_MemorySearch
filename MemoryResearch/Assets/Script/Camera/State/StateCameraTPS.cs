using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<CameraManager>;

public class StateCameraTPS : State
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
        if (!Owner.FloorCameraArea.inArea)
        {
            stateMachine.Dispatch((int)CameraManager.CameraType.Floor);
        }

        //Object移動カメラ
        if (Owner.MoveObjCamScript.ChangFlg)
        {
            stateMachine.Dispatch((int)CameraManager.CameraType.Controller);
        }
    }
}
