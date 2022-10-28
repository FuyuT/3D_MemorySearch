using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("FPSカメラ")]
    [SerializeField] GameObject FPSCamera;

    [Header("TPSカメラ")]
    [SerializeField] GameObject TPSCamera;

    //TODO
    [Header("フロアカメラ")]
    [SerializeField] GameObject FloorCamera;

    [Header("コントロールカメラ")]
    [SerializeField] GameObject ControllerCamera;

    //エリアカメラ関連
    ObjectCollider              FloorCameraArea;
    [SerializeField] GameObject Area;

    //コントロールカメラ関連
    ChangeMoveObjectCamera      MoveObjCamScript;
    //TODO
    [SerializeField] GameObject Operation;

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
        nowCamera = CameraType.None;
        nextCamera = CameraType.None;
        ChangeMainCamara();

        FloorCameraArea = Area.GetComponent<ObjectCollider>();
        MoveObjCamScript = Operation.GetComponent<ChangeMoveObjectCamera>();
    }

   // Update is called once per frame
    void Update()
    {
        SelectNextCamera();
        ChangeMainCamara();
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

        //フロアカメラ
        if (FloorCameraArea.inArea)
        {
            nextCamera = CameraType.TPS;
        }
        else
        {
            nextCamera = CameraType.Floor;
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
}
