using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("FPS�J����")]
    [SerializeField] GameObject FPSCamera;

    [Header("TPS�J����")]
    [SerializeField] GameObject TPSCamera;

    [Header("�t���A�J����")]
    [SerializeField] GameObject FloorCamera;

    [Header("�t���A�J����2")]
    [SerializeField] GameObject FloorCamera2;

    [Header("�R���g���[���J����")]
    [SerializeField] GameObject ControllerCamera;

    //�G���A�J�����֘A
    public ObjectCollider       FloorCameraArea;
    [SerializeField] GameObject Area;

    //�G���A�J�����֘A
    public ObjectCollider1      FloorCameraArea2;
    [SerializeField] GameObject Area2;

    //�R���g���[���J�����֘A
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
        //Debug.Log("���݂̃J����:" + stateMachine.currentStateKey
        //    + "�O��̃J����:" + stateMachine.beforeStateKey);

        stateMachine.Update();
        ChangeMainCamara();
        nowCamera = CameraType.None;
        nextCamera = CameraType.None;
        ChangeMainCamara();
        SelectNextCamera();
        FloorCameraArea = Area.GetComponent<ObjectCollider>();
        MoveObjCamScript = Operation.GetComponent<ChangeMoveObjectCamera>();
    }

   

    void SelectNextCamera()
    {
        if (Input.GetKeyDown("space"))
        {
          
            nextCamera = CameraType.FPS;
            
            //if(FPSCamera.activeSelf)
            //{
                    
            //}
        }

        //�t���A�J����
        if (FloorCameraArea.inArea)
        {
            nextCamera = CameraType.TPS;
        }
        else
        {
            nextCamera = CameraType.Floor;
        }

        //�_�b? �M�~�b�N
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
        FloorCamera.SetActive(false);
        FloorCamera2.SetActive(false);
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


        switch (floorNo)
        {

            case 1:
                FloorCamera.SetActive(true);
                break;
            case 2:
                FloorCamera2.SetActive(true);
                break;

            default:
                break;
        }

    }
}
