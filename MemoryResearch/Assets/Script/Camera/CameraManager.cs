using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("FPS�J����")]
    [SerializeField] GameObject FPSCamera;

    [Header("TPS�J����")]
    [SerializeField] GameObject TPSCamera;

    //TODO
    [Header("�t���A�J����")]
    [SerializeField] GameObject FloorCamera;

    [Header("�t���A�J����2")]
    [SerializeField] GameObject FloorCamera2;

    [Header("�R���g���[���J����")]
    [SerializeField] GameObject ControllerCamera;

    //�G���A�J�����֘A
    ObjectCollider              FloorCameraArea;
    [SerializeField] GameObject Area;

    //�G���A�J�����֘A
    ObjectCollider1 FloorCameraArea2;
    [SerializeField] GameObject Area2;

    //�R���g���[���J�����֘A
    ChangeMoveObjectCamera      MoveObjCamScript;
    //TODO
    [SerializeField] GameObject Operation;

    bool On;

    public enum CameraType
    {
        None,
        FPS,
        TPS,
        Floor,
        Floor2,
        Controller
    }

    CameraType nowCamera;
    CameraType nextCamera;

    // Start is called before the first frame update
    void Start()
    {
        nowCamera = CameraType.None;
        nextCamera = CameraType.Floor;
        ChangeMainCamara();

        FloorCameraArea = Area.GetComponent<ObjectCollider>();
        FloorCameraArea2 = Area2.GetComponent<ObjectCollider1>();
        MoveObjCamScript = Operation.GetComponent<ChangeMoveObjectCamera>();

        On = false;
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
            //�t���A�J����
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

            //�_�b? �M�~�b�N
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

        }
    }

    void ChangeMainCamara()
    {
        if (nowCamera == nextCamera) return;

        FPSCamera.SetActive(false);
        TPSCamera.SetActive(false);
        FloorCamera.SetActive(false);
        FloorCamera2.SetActive(false);
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

            case CameraType.Floor2:
                FloorCamera2.SetActive(true);
                break;

            case CameraType.Controller:
                ControllerCamera.SetActive(true);
                break;

            default:
                break;
        }

    }
}
