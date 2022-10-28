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

<<<<<<< HEAD
    [Header("フロアカメラ2")]
    [SerializeField] GameObject FloorCamera2;

=======
>>>>>>> origin/#24-繝繝｡繝ｼ繧ｸ蜃ｦ逅・
    [Header("コントロールカメラ")]
    [SerializeField] GameObject ControllerCamera;

    //エリアカメラ関連
    ObjectCollider              FloorCameraArea;
    [SerializeField] GameObject Area;

<<<<<<< HEAD
    //エリアカメラ関連
    ObjectCollider1 FloorCameraArea2;
    [SerializeField] GameObject Area2;

=======
>>>>>>> origin/#24-繝繝｡繝ｼ繧ｸ蜃ｦ逅・
    //コントロールカメラ関連
    ChangeMoveObjectCamera      MoveObjCamScript;
    //TODO
    [SerializeField] GameObject Operation;

<<<<<<< HEAD
    bool On;

=======
>>>>>>> origin/#24-繝繝｡繝ｼ繧ｸ蜃ｦ逅・
    public enum CameraType
    {
        None,
        FPS,
        TPS,
        Floor,
<<<<<<< HEAD
        Floor2,
=======
>>>>>>> origin/#24-繝繝｡繝ｼ繧ｸ蜃ｦ逅・
        Controller
    }

    CameraType nowCamera;
    CameraType nextCamera;

    // Start is called before the first frame update
    void Start()
    {
        nowCamera = CameraType.None;
<<<<<<< HEAD
        nextCamera = CameraType.Floor;
        ChangeMainCamara();

        FloorCameraArea = Area.GetComponent<ObjectCollider>();
        FloorCameraArea2 = Area2.GetComponent<ObjectCollider1>();
        MoveObjCamScript = Operation.GetComponent<ChangeMoveObjectCamera>();

        On = false;
=======
        nextCamera = CameraType.None;
        ChangeMainCamara();

        FloorCameraArea = Area.GetComponent<ObjectCollider>();
        MoveObjCamScript = Operation.GetComponent<ChangeMoveObjectCamera>();
>>>>>>> origin/#24-繝繝｡繝ｼ繧ｸ蜃ｦ逅・
    }

   // Update is called once per frame
    void Update()
    {
        SelectNextCamera();
        ChangeMainCamara();
    }

<<<<<<< HEAD

=======
>>>>>>> origin/#24-繝繝｡繝ｼ繧ｸ蜃ｦ逅・
    void SelectNextCamera()
    {
        if (Input.GetKeyDown("space"))
        {
<<<<<<< HEAD
            if (!FPSCamera.activeSelf)
            {
                nextCamera = CameraType.FPS;
                return;
                On = true;
            }


            if (FPSCamera.activeSelf)
            {
                nextCamera = CameraType.TPS;
                On = false;
            }
            return;
        }

        if (!FPSCamera.activeSelf)
        {
            //フロアカメラ
            if (FloorCameraArea.inArea)
            {
                nextCamera = CameraType.TPS;
                return;
            }

            if (FloorCameraArea2.inArea2)
            {
                nextCamera = CameraType.Floor2;
                return;
            }

            //神獣? ギミック
            if (MoveObjCamScript.ChangFlg)
            {

                nextCamera = CameraType.Controller;
                return;
            }
            else
            {
                nextCamera = CameraType.Floor;
                return;
            }

=======
          
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
>>>>>>> origin/#24-繝繝｡繝ｼ繧ｸ蜃ｦ逅・
        }
    }

    void ChangeMainCamara()
    {
        if (nowCamera == nextCamera) return;

        FPSCamera.SetActive(false);
        TPSCamera.SetActive(false);
        FloorCamera.SetActive(false);
<<<<<<< HEAD
        FloorCamera2.SetActive(false);
        ControllerCamera.SetActive(false);
       
=======
        ControllerCamera.SetActive(false);
>>>>>>> origin/#24-繝繝｡繝ｼ繧ｸ蜃ｦ逅・

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

<<<<<<< HEAD
            case CameraType.Floor2:
                FloorCamera2.SetActive(true);
                break;

=======
>>>>>>> origin/#24-繝繝｡繝ｼ繧ｸ蜃ｦ逅・
            case CameraType.Controller:
                ControllerCamera.SetActive(true);
                break;

            default:
                break;
        }

    }
}
