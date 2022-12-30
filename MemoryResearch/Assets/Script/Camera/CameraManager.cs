using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [Header("FPSカメラ")]
    [SerializeField] GameObject FPSCamera;

    [Header("TPSカメラ")]
    [SerializeField] GameObject TPSCamera;

    [Header("コントロールカメラ")]
    [SerializeField] public GameObject ControllerCamera;

    //コントロールカメラ関連
    public ChangeMoveObjectCamera MoveObjCamScript;
    public StageGimmick stageGimmick;
    
    [SerializeField] GameObject Operation;

    int floorNo;

    MyUtil.StateMachine<CameraManager> stateMachine;
    public int GetCurrentCameraType() {return stateMachine.currentStateKey; }
    [SerializeField] public StagecolorChange colorChange;

    public enum CameraType
    {
        None,
        FPS,
        TPS,
        Controller
    }

    CameraType nowCamera;

    CameraManager()
    {
        instance = this;
    }

    void Start()
    {
        MoveObjCamScript = Operation.GetComponent<ChangeMoveObjectCamera>();
        stageGimmick = Operation.GetComponent<StageGimmick>();

        StateMachineInit();

        ChangeMainCamara();

        floorNo = 1;
    }

    void StateMachineInit()
    {
        stateMachine = new MyUtil.StateMachine<CameraManager>(this);

        stateMachine.AddAnyTransition<StateCameraTPS>((int)CameraType.TPS);
        stateMachine.AddAnyTransition<StateCameraFPS>((int)CameraType.FPS);
        stateMachine.AddAnyTransition<StateCameraControlObject>((int)CameraType.Controller);
        stateMachine.Start(stateMachine.GetOrAddState<StateCameraTPS>());
        stateMachine.currentStateKey = (int)CameraType.TPS;
    }

    void Update()
    {
        stateMachine.Update();
    }

    void FixedUpdate()
    {
        ChangeMainCamara();
    }

    void AllCameraInit()
    {
        FPSCamera.SetActive(false);
        TPSCamera.SetActive(false);
        ControllerCamera.SetActive(false);
    }

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

            case (int)CameraType.Controller:
                ControllerCamera.SetActive(true);
                MoveObjCamScript.ChangFlg = true;
                break;
        }
        nowCamera = (CameraType)stateMachine.currentStateKey;
    }
}
