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

    ////オブジェクトを動かすときの速さ
    //public float MoveObjectSpeed;

    //const float Move_Max = 4.5f;

    //Vector3 currenPos ,previousPos;

    //public float Sensitivity = 1f;

    private Vector3 mouse;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        Script = ControlScript.GetComponent<ControlCamera>();
    }

    // Update is called once per frame
    void Update()
    {
 
        if (Script.MoveObjectSwitch)
        {
            if(Input.GetMouseButton(0))
            {
                mouse = Input.mousePosition;
                target = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, 10));
                target.x = transform.position.x;
                target.z = transform.position.z;
                this.transform.position = target;



                //  Vector3 objectPoint
                //= Camera.main.WorldToScreenPoint(transform.position);

                //  //Cubeの現在位置(マウス位置)を、pointScreenに格納
                //  Vector3 pointScreen
                //      = new Vector3(Input.mousePosition.x,
                //                    Input.mousePosition.y,
                //                    objectPoint.z);

                //  //Cubeの現在位置を、スクリーン座標からワールド座標に変換して、pointWorldに格納
                //  Vector3 pointWorld = Camera.main.ScreenToWorldPoint(pointScreen);
                //  pointWorld.z = transform.position.z;

                //  //Cubeの位置を、pointWorldにする
                //  transform.position = pointWorld;


                //if (Input.GetMouseButtonDown(1))
                //{
                //    previousPos = Input.mousePosition;
                //}
                //if (Input.GetMouseButton(1))
                //{
                //    currenPos = Input.mousePosition;
                //    float diffDistance = (currenPos.x - previousPos.x) / Screen.width * MoveObjectSpeed;
                //    diffDistance *= Sensitivity;

                //    float newX = Mathf.Clamp(transform.localPosition.x + diffDistance,-Move_Max,Move_Max);
                //    this.gameObject.transform.Translate(newX, 0, 0);


                //    previousPos = currenPos;
                //}
            }
        }
        else
        {
            return;
        }
    }
}
