using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ControlCamera : MonoBehaviour
{
    // �J�����̉�]���x���i�[����ϐ�
    public Vector2 rotationSpeed;

    //�Y�[���p�ϐ�
    public float ZoomSpeed;

    Vector2 fromMousePos;

    //�}�E�X�z�[���h�ŃY�[���C���E�Y�[���A�E�g
    void Zoom()
    {
        var scroll = Input.mouseScrollDelta.y;
        //mainCamera.transform.position -= -mainCamera.transform.forward * scroll * ZoomSpeed;

        ////���ȏ�Y�[���͏o���Ȃ�����
        //if (mainCamera.transform.position.y <= 0)
        //{
        //    ZoomSpeed = 0;
        //}
    }

    void Rotate()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    //���݂̃}�E�X���W��ϐ�fromMousePos�Ɋi�[
        //    Vector2 fromMousePos = Input.mousePosition;
        //}

        if (!Input.GetMouseButton(1)) return;

        //���݂̊p�x�ŏ�����
        Vector2 newAngle = transform.localEulerAngles;
        //�t���[�����}�E�X�̈ړ��ʕ���]������
        newAngle += (fromMousePos - new Vector2(Input.mousePosition.x, Input.mousePosition.y)) * rotationSpeed;
        fromMousePos = Input.mousePosition;
        transform.localEulerAngles = new Vector3 (newAngle.y,newAngle.x,0);

        ////����ȏ�͉�]�ł��Ȃ�
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


