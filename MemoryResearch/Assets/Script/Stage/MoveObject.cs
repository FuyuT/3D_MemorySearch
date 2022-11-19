using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveObject : MonoBehaviour
{
    [SerializeField]
    GameObject ControlScript;

    //ControlCameraのスクリプトを取得
    ControlCamera Script;

    public float moveSpeed;

    Camera mainCamera;
    private float rayDistance;

    //private Transform FirstFloorPos;

    // Start is called before the first frame update
    void Start()
    {
        rayDistance = 100f;
        mainCamera = Camera.main;
        Script = ControlScript.GetComponent<ControlCamera>();
        //FirstFloorPos.localPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Script.MoveObjectSwitch)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            foreach (RaycastHit hit in Physics.RaycastAll(ray))
            {
                if (hit.transform.name == "Dore")
                {
                    if (Input.GetMouseButton(0))
                    {

                        Vector3 objPos = Camera.main.WorldToScreenPoint(transform.position);

                        Vector3 mousePos = new Vector3(objPos.x, Input.mousePosition.y, objPos.z);

                        transform.position += new Vector3(0, Input.GetAxis("Mouse Y") * moveSpeed, 0);
                        //Debug.Log(Input.GetAxis("Vertical"));
                     

                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
    }
}

