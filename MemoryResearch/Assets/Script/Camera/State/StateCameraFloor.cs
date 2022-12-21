//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//using State = State<CameraManager>;

//public class StateCameraFloor : State
//{
//    //private GameObject player;

//    protected override void OnEnter(State prevState)
//    {
//        //player = GameObject.Find("Player");
//        //Owner.transform.LookAt(player.transform);
//    }

//    protected override void OnUpdate()
//    {
//        SelectNextState();
//    }

//    protected override void SelectNextState()
//    {
//        //spaceを押したらFPSカメラ
//        if (Input.GetKeyDown("space"))
//        {
//            stateMachine.Dispatch((int)CameraManager.CameraType.FPS);
//        }

//        //フロアカメラ
//        if (!Owner.FloorCameraArea.inArea && !Owner.FloorCameraArea2.inArea2 && !Owner.FloorCameraArea3.inArea3 && !Owner.FloorCameraArea4.inArea4)
//        {
//            stateMachine.Dispatch((int)CameraManager.CameraType.TPS);
//        }
//        else
//        {
//            ChangeFloorCamera();
//        }

//        if (Input.GetKeyDown("v") && Owner.MoveObjCamScript.RangeInFlg)
//        {
//            stateMachine.Dispatch((int)CameraManager.CameraType.Controller);            
//        }
//    }


//    //void ChangeFloorCamera()
//    //{
//    //    if (Owner.FloorCameraArea.inArea)
//    //    {
//    //        Owner.FloorCamera.SetActive(true);
//    //    }

//    //    if (Owner.FloorCameraArea2.inArea2)
//    //    {
//    //        Owner.FloorCamera2.SetActive(true);
//    //    }

//    //    if (Owner.FloorCameraArea3.inArea3)
//    //    {
//    //        Owner.FloorCamera3.SetActive(true);
//    //    }

//    //    if (Owner.FloorCameraArea4.inArea4)
//    //    {
//    //        Owner.FloorCamera4.SetActive(true);
//    //    }
//    //}
//}
