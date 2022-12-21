using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveObject : MonoBehaviour
{
    CameraManager cameraManager;

    public float moveSpeed;
    private void Awake()
    {
        cameraManager = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManager>();
    }

    void Update()
    {
        if (cameraManager.GetCurrentCameraType() != (int)CameraManager.CameraType.Controller) return;

        if (!Input.GetMouseButton(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         foreach (RaycastHit hit in Physics.RaycastAll(ray))
         {
             if (hit.transform.name == "Dore")
             {
                 Vector3 objPos = Camera.main.WorldToScreenPoint(transform.position);
                 transform.position += new Vector3(0, Input.GetAxis("Mouse Y") * moveSpeed, 0);
             }
         }
    }
}

