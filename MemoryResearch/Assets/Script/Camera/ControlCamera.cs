using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ControlCamera : MonoBehaviour
{
    //ChangeCameraのスクリプト
    ChangeMoveObjectCamera Script;

    [SerializeField]
    GameObject ControlleCamera;

    [SerializeField]
    GameObject USBObject;

    // カメラオブジェクトを格納する変数
    public Camera mainCamera;

    // カメラの回転速度を格納する変数
    public Vector2 rotationSpeed;

    // マウス移動方向とカメラ回転方向を反転する判定フラグ
    public bool reverse;

    // マウス座標を格納する変数
    private Vector2 lastMousePosition;

    // カメラの角度を格納する変数（初期値に0,0を代入）
    private Vector2 newAngle = new Vector2(0, 0);

    [Header("カメラマネージャー")]
    [SerializeField] CameraManager camemana;

    //ズーム用変数
    public float ZoomSpeed;

    public bool MoveObjectSwitch;

    public Vector3 MovePos;
    public Transform target;

    private float moveSpeed;

    void Start()
    {
       
        Script = USBObject.GetComponent<ChangeMoveObjectCamera>();

        MoveObjectSwitch = false;

        moveSpeed = 5.0f;
        target.transform.position = new Vector3(-625f, 50f, 546f);
        //target.transform.position = new Vector3(MovePos.x, MovePos.y, MovePos.z);

    }

    void Update()
    {
       
    }

    void FixedUpdate()
    {
        if (ControlleCamera.activeSelf)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed);
        }

        if (camemana.GetCurrentCameraType() != (int)CameraManager.CameraType.Controller)
        {
            return;
        }
        MoveObjectSwitch = true;

        ControlleCamera.SetActive(true);

        //左クリックした時
        if (Input.GetMouseButtonDown(1))
        {
            //カメラの角度を変数newAngleに格納
            newAngle = mainCamera.transform.localEulerAngles;

            newAngle = ControlleCamera.transform.localEulerAngles;

            //マウス座標を変数lastMousePositionに格納
            lastMousePosition = Input.mousePosition;
        }
        //左ドラッグしている間
        else if (Input.GetMouseButton(1))
        {
            //カメラ回転方向の判定フラグがtrueの場合
            if (!reverse)
            {
                // Y軸の回転：マウスドラッグ方向に視点回転
                // マウスの水平移動値に変数rotationSpeedを掛ける
                //（クリック時の座標とマウス座標の現在値の差分値）
                newAngle.y -= (lastMousePosition.x - Input.mousePosition.x) * rotationSpeed.y;

                ////これ以上は回転できない
                //if (newAngle.y <= 90)
                //{
                //    rotationSpeed.y = 0;
                //}
                //if (newAngle.x <= -33)
                //{
                //    rotationSpeed.x = 0;
                //}

                // X軸の回転：マウスドラッグ方向に視点回転
                // マウスの垂直移動値に変数rotationSpeedを掛ける
                //（クリック時の座標とマウス座標の現在値の差分値）
                newAngle.x -= (Input.mousePosition.y - lastMousePosition.y) * rotationSpeed.x;

                //newAngleの角度をカメラ角度に格納
                mainCamera.transform.localEulerAngles = newAngle;

                mainCamera.transform.localEulerAngles = newAngle;

                //マウス座標を変数lastMousePositionに格納
                lastMousePosition = Input.mousePosition;
            }
            //カメラ回転方向の判定フラグがreverseの場合
            else if (reverse)
            {
                //Y軸の回転：マウスドラッグと逆方向に視点回転
                newAngle.y -= (Input.mousePosition.x - lastMousePosition.x) * rotationSpeed.y;

                //X軸の回転：マウスドラッグと逆方向に視点回転
                newAngle.x -= (lastMousePosition.y - Input.mousePosition.y) * rotationSpeed.x;

                //newAngleの角度をカメラ角度に格納
                mainCamera.transform.localEulerAngles = newAngle;

                mainCamera.transform.localEulerAngles = newAngle;

                //マウス座標を変数lastMousePositionに格納
                lastMousePosition = Input.mousePosition;
            }

            //マウスホールドでズームイン・ズームアウト
            var scroll = Input.mouseScrollDelta.y;
            mainCamera.transform.position -= -mainCamera.transform.forward * scroll * ZoomSpeed;

            //一定以上ズームは出来なくする
            if (mainCamera.transform.position.y <= 0)
            {
                ZoomSpeed = 0;
            }
        }
    }
}
