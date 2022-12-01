using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<CameraManager>;



public class StateCameraFloor : State
{
    //private GameObject player;

    protected override void OnEnter(State prevState)
    {
        //player = GameObject.Find("Player");
        //Owner.transform.LookAt(player.transform);
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
        if (!Owner.FloorCameraArea.inArea && !Owner.FloorCameraArea2.inArea2 && !Owner.FloorCameraArea3.inArea3 && !Owner.FloorCameraArea4.inArea4)
        {
            stateMachine.Dispatch((int)CameraManager.CameraType.TPS);
        }
        //else if (!Owner.FloorCameraArea2.inArea2)
        //{
        //    stateMachine.Dispatch((int)CameraManager.CameraType.TPS);
        //}

        if (Input.GetKeyDown("v") && Owner.MoveObjCamScript.ChangFlg)
        {
            stateMachine.Dispatch((int)CameraManager.CameraType.Controller);
            
        }
    }
}
