using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ControlCamera : MonoBehaviour
{
    // カメラの回転速度を格納する変数
    public Vector2 rotationSpeed;

    Vector2 clickPos;

    void Rotate()
    {
        if (!Input.GetMouseButton(1)) return;

        //最初に押したとき
        if(Input.GetMouseButtonDown(1))
        {
            clickPos = Input.mousePosition;
        }

        //マウスの移動ベクトルを取得
        Vector2 inputVec = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - clickPos;
        inputVec = inputVec.normalized;
        if (inputVec == Vector2.zero) return;

        //現在の角度を変更
        Vector2 newAngle = transform.localEulerAngles;
        newAngle.x += inputVec.y * -rotationSpeed.y * Time.deltaTime;
        newAngle.y += inputVec.x * rotationSpeed.x * Time.deltaTime;
        transform.localEulerAngles = new Vector3(newAngle.x, newAngle.y, 0);
    }

    //角度のリセット
    void ResetRotate()
    {
        //Qを押した時に、PathFollowerの最終パスのRotationにリセットする
        if(Input.GetKeyDown(KeyCode.Q))
        {

        }
    }

    void Update()
    {
        Rotate();
        ResetRotate();
        Cursor.visible = true;

    }
}


