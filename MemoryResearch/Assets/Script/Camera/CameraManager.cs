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

    //エリアカメラ関連
    ObjectCollider FloorCameraArea;
    [SerializeField] GameObject Area;

    public enum CameraType
    {
        None,
        FPS,
        TPS,
        Floor,
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
            if(FPSCamera.activeSelf)
            {
                
            }
        }

        if(FloorCameraArea.inArea)
        {
            nextCamera = CameraType.TPS;
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

            default:
                break;
        }

    }
}
