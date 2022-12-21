using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("FPSカメラ")]
    [SerializeField] GameObject FPSCamera;

    [Header("TPSカメラ")]
    [SerializeField] GameObject TPSCamera;

    //[Header("フロアカメラ")]
    //[SerializeField] public GameObject FloorCamera;

    //[Header("フロアカメラ2")]
    //[SerializeField] public GameObject FloorCamera2;

    //[Header("フロアカメラ3")]
    //[SerializeField] public GameObject FloorCamera3;

    //[Header("フロアカメラ4")]
    //[SerializeField] public GameObject FloorCamera4;

    [Header("コントロールカメラ")]
    [SerializeField] public GameObject ControllerCamera;

    //エリアカメラ関連
    //public ObjectCollider FloorCameraArea;
    //[SerializeField] GameObject Area;

    //エリアカメラ関連
    //public ObjectCollider1 FloorCameraArea2;
    //[SerializeField] GameObject Area2;

    //エリアカメラ関連
    //public ObjectCollider2 FloorCameraArea3;
    //[SerializeField] GameObject Area3;

    //エリアカメラ関連
    //public ObjectCollider3 FloorCameraArea4;
    //[SerializeField] GameObject Area4;

    //コントロールカメラ関連
    public ChangeMoveObjectCamera MoveObjCamScript;
    public StageGimmick stageGimmick;
    //TODO
    [SerializeField] GameObject Operation;

    int floorNo;

    StateMachine<CameraManager> stateMachine;
    public int GetCurrentCameraType() {return stateMachine.currentStateKey; }
    [SerializeField] public StagecolorChange colorChange;

    public enum CameraType
    {
        None,
        FPS,
        TPS,
       // Floor,
        Controller
    }

    CameraType nowCamera;

    // Start is called before the first frame update
    void Start()
    {

        //FloorCameraArea = Area.GetComponent<ObjectCollider>();
        //FloorCameraArea2 = Area2.GetComponent<ObjectCollider1>();
        //FloorCameraArea3 = Area3.GetComponent<ObjectCollider2>();
        //FloorCameraArea4 = Area4.GetComponent<ObjectCollider3>();

        MoveObjCamScript = Operation.GetComponent<ChangeMoveObjectCamera>();
        stageGimmick = Operation.GetComponent<StageGimmick>();

        StateMachineInit();

        ChangeMainCamara();

        floorNo = 1;
    }

    void StateMachineInit()
    {
        stateMachine = new StateMachine<CameraManager>(this);

        stateMachine.AddAnyTransition<StateCameraTPS>((int)CameraType.TPS);
        stateMachine.AddAnyTransition<StateCameraFPS>((int)CameraType.FPS);
        stateMachine.AddAnyTransition<StateCameraControlObject>((int)CameraType.Controller);
        //stateMachine.AddAnyTransition<StateCameraFloor>((int)CameraType.Floor);
        stateMachine.Start(stateMachine.GetOrAddState<StateCameraTPS>());
        stateMachine.currentStateKey = (int)CameraType.TPS;
    }

    void Update()
    {
        stateMachine.Update();
        ChangeMainCamara();
    }

    void AllCameraInit()
    {
        FPSCamera.SetActive(false);
        TPSCamera.SetActive(false);
        ControllerCamera.SetActive(false);
        //FloorCameraInit();
    }

    //void FloorCameraInit()
    //{
    //    FloorCamera.SetActive(false);
    //    FloorCamera2.SetActive(false);
    //    FloorCamera3.SetActive(false);
    //    FloorCamera4.SetActive(false);
    //}

    void ChangeMainCamara()
    {
        if ((int)nowCamera == stateMachine.currentStateKey)
            return;

        AllCameraInit();

        switch (stateMachine.currentStateKey)
        {
            case (int)CameraType.FPS:
                FPSCamera.SetActive(true);
                break;

            case (int)CameraType.TPS:
                TPSCamera.SetActive(true);
                break;

            //case (int)CameraType.Floor:
            //    SelectFloorCamera();
            //    break;

            case (int)CameraType.Controller:
                ControllerCamera.SetActive(true);
                MoveObjCamScript.ChangFlg = true;
                break;
        }
        nowCamera = (CameraType)stateMachine.currentStateKey;
    }

    //void SelectFloorCamera()
    //{
    //    if(FloorCameraArea.inArea)
    //    {
    //        floorNo = 1;
    //    }

    //    if (FloorCameraArea2.inArea2)
    //    {
    //        floorNo = 2;
    //    }

    //    if (FloorCameraArea3.inArea3)
    //    {
    //        floorNo = 3;
    //    }

    //    if (FloorCameraArea4.inArea4)
    //    {
    //        floorNo = 4;
    //    }


    //    switch (floorNo)
    //    {

    //        case 1:
    //            FloorCamera.SetActive(true);
    //            break;
    //        case 2:
    //            FloorCamera2.SetActive(true);
    //            break;
    //        case 3:
    //            FloorCamera3.SetActive(true);
    //            break;
    //        case 4:
    //            FloorCamera4.SetActive(true);
    //            break;

    //        default:
    //            break;
    //    }

    //}
}
