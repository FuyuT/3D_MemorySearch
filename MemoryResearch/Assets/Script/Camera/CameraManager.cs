using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("FPSカメラ")]
    [SerializeField] GameObject FPSCamera;

    [Header("TPSカメラ")]
    [SerializeField] GameObject TPSCamera;

    [Header("フロアカメラ")]
    [SerializeField] GameObject FloorCamera;

    [Header("フロアカメラ2")]
    [SerializeField] GameObject FloorCamera2;

    [Header("フロアカメラ3")]
    [SerializeField] GameObject FloorCamera3;

    [Header("フロアカメラ4")]
    [SerializeField] GameObject FloorCamera4;

    [Header("コントロールカメラ")]
    [SerializeField] GameObject ControllerCamera;

    //エリアカメラ関連
    public ObjectCollider       FloorCameraArea;
    [SerializeField] GameObject Area;

    //エリアカメラ関連
    public ObjectCollider1      FloorCameraArea2;
    [SerializeField] GameObject Area2;

    //エリアカメラ関連
    public ObjectCollider2 FloorCameraArea3;
    [SerializeField] GameObject Area3;

    //エリアカメラ関連
    public ObjectCollider3 FloorCameraArea4;
    [SerializeField] GameObject Area4;

    //コントロールカメラ関連
    public ChangeMoveObjectCamera MoveObjCamScript;
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
        Floor,
        Controller
    }

    CameraType nowCamera;
    CameraType nextCamera;

    // Start is called before the first frame update
    void Start()
    {

        FloorCameraArea = Area.GetComponent<ObjectCollider>();
        FloorCameraArea2 = Area2.GetComponent<ObjectCollider1>();
        FloorCameraArea3 = Area3.GetComponent<ObjectCollider2>();
        FloorCameraArea4 = Area4.GetComponent<ObjectCollider3>();

        MoveObjCamScript = Operation.GetComponent<ChangeMoveObjectCamera>();

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
        stateMachine.AddAnyTransition<StateCameraFloor>((int)CameraType.Floor);
        stateMachine.Start(stateMachine.GetOrAddState<StateCameraTPS>());
        stateMachine.currentStateKey = (int)CameraType.TPS;
    }

    void Update()
    {
        stateMachine.Update();
        ChangeMainCamara();
        nowCamera = CameraType.None;
        nextCamera = CameraType.None;
        ChangeMainCamara();
        SelectNextCamera();

        FloorCameraArea = Area.GetComponent<ObjectCollider>();
        FloorCameraArea2 = Area2.GetComponent<ObjectCollider1>();
        FloorCameraArea3 = Area3.GetComponent<ObjectCollider2>();
        FloorCameraArea4 = Area4.GetComponent<ObjectCollider3>();


        MoveObjCamScript = Operation.GetComponent<ChangeMoveObjectCamera>();
    }

   

    void SelectNextCamera()
    {
        if (Input.GetKeyDown("space"))
        {
          
            nextCamera = CameraType.FPS;
            
        }

        //フロアカメラ
        if (FloorCameraArea.inArea || FloorCameraArea2.inArea2 || FloorCameraArea3.inArea3 || FloorCameraArea4.inArea4)
        {
            nextCamera = CameraType.Floor;
        }
        //else if ()
        //{
        //    nextCamera = CameraType.Floor;
        //}
        else
        {
            nextCamera = CameraType.TPS;
        }

        //神獣? ギミック
        if (MoveObjCamScript.ChangFlg)
        {
            nextCamera = CameraType.Controller;
        }
        else
        {
            nextCamera = CameraType.Floor;
        }
    }

    void ChangeMainCamara()
    {
        FPSCamera.SetActive(false);
        TPSCamera.SetActive(false);

        //フロアカメラ
        //TODO後で一つにまとめる
        FloorCamera.SetActive(false);
        FloorCamera2.SetActive(false);
        FloorCamera3.SetActive(false);

        ControllerCamera.SetActive(false);


        switch (stateMachine.currentStateKey)
        {
            case (int)CameraType.FPS:
                FPSCamera.SetActive(true);
                break;

            case (int)CameraType.TPS:
                TPSCamera.SetActive(true);
                break;

            case (int)CameraType.Floor:
                SelectFloorCamera();
                break;

            case (int)CameraType.Controller:
                ControllerCamera.SetActive(true);
                break;
        }
        if (nowCamera == nextCamera) return;

        FPSCamera.SetActive(false);
        TPSCamera.SetActive(false);
        FloorCamera.SetActive(false);
        ControllerCamera.SetActive(false);

        switch (nextCamera)
        {
            case CameraType.FPS:
                FPSCamera.SetActive(true);
                break;

            case CameraType.TPS:
                TPSCamera.SetActive(true);
                break;

            case CameraType.Floor:
                FloorCamera.SetActive(true);
                break;

            case CameraType.Controller:
                ControllerCamera.SetActive(true);
                break;

            default:
                break;
        }

    }

    void SelectFloorCamera()
    {
        if(FloorCameraArea.inArea)
        {
            floorNo = 1;
        }

        if (FloorCameraArea2.inArea2)
        {
            floorNo = 2;
        }

        if (FloorCameraArea3.inArea3)
        {
            floorNo = 3;
        }

        if (FloorCameraArea4.inArea4)
        {
            floorNo = 4;
        }


        switch (floorNo)
        {

            case 1:
                FloorCamera.SetActive(true);
                break;
            case 2:
                FloorCamera2.SetActive(true);
                break;
            case 3:
                FloorCamera3.SetActive(true);
                break;
            case 4:
                FloorCamera4.SetActive(true);
                break;

            default:
                break;
        }

    }
}
