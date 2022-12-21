using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ControlCamera : MonoBehaviour
{
    // カメラの回転速度を格納する変数
    public Vector2 rotationSpeed;

    //ズーム用変数
    public float ZoomSpeed;

    Vector2 fromMousePos;

    //マウスホールドでズームイン・ズームアウト
    void Zoom()
    {
        var scroll = Input.mouseScrollDelta.y;
        //mainCamera.transform.position -= -mainCamera.transform.forward * scroll * ZoomSpeed;

        ////一定以上ズームは出来なくする
        //if (mainCamera.transform.position.y <= 0)
        //{
        //    ZoomSpeed = 0;
        //}
    }

    void Rotate()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    //現在のマウス座標を変数fromMousePosに格納
        //    Vector2 fromMousePos = Input.mousePosition;
        //}

        if (!Input.GetMouseButton(1)) return;

        //現在の角度で初期化
        Vector2 newAngle = transform.localEulerAngles;
        //フレーム中マウスの移動量分回転させる
        newAngle += (fromMousePos - new Vector2(Input.mousePosition.x, Input.mousePosition.y)) * rotationSpeed;
        fromMousePos = Input.mousePosition;
        transform.localEulerAngles = new Vector3 (newAngle.y,newAngle.x,0);

        ////これ以上は回転できない
        //if (newAngle.y <= 90)
        //{
        //    rotationSpeed.y = 0;
        //}
        //if (newAngle.x <= -33)
        //{
        //    rotationSpeed.x = 0;
        //}
    }


    void Update()
    {
        Rotate();
        Zoom();
    }
}


