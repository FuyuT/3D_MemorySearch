﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<CameraManager>;

public class StateCameraTPS : State
{
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
        if (Owner.FloorCameraArea.inArea || Owner.FloorCameraArea2.inArea2 || Owner.FloorCameraArea3.inArea3 || Owner.FloorCameraArea4.inArea4)
        {
            stateMachine.Dispatch((int)CameraManager.CameraType.Floor);
        }
        ////フロアカメラ
        //else if (Owner.FloorCameraArea2.inArea2)
        //{
        //    stateMachine.Dispatch((int)CameraManager.CameraType.Floor);
        //}

        //Object移動カメラ
        if (Owner.MoveObjCamScript.ChangFlg)
        {
            stateMachine.Dispatch((int)CameraManager.CameraType.Controller);
        }
    }
}
