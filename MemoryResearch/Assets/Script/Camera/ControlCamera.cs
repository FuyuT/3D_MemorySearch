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

    Vector2 clickPos;

    //�}�E�X�z�[���h�ŃY�[���C���E�Y�[���A�E�g
    void Zoom()
    {
        var scroll = Input.mouseScrollDelta.y;
        //transform.position -= -transform.forward * scroll * ZoomSpeed;

        ////���ȏ�Y�[���͏o���Ȃ�����
        //if (mainCamera.transform.position.y <= 0)
        //{
        //    ZoomSpeed = 0;
        //}
    }

    void Rotate()
    {
        if (!Input.GetMouseButton(1)) return;

        //�ŏ��ɉ������Ƃ�
        if(Input.GetMouseButtonDown(1))
        {
            clickPos = Input.mousePosition;
        }

        //�}�E�X�̈ړ��x�N�g�����擾
        Vector2 inputVec = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - clickPos;
        inputVec = inputVec.normalized;
        if (inputVec == Vector2.zero) return;

        //���݂̊p�x��ύX
        Vector2 newAngle = transform.localEulerAngles;
        newAngle.x += inputVec.y * -rotationSpeed.y * Time.deltaTime;
        newAngle.y += inputVec.x * rotationSpeed.x * Time.deltaTime;
        transform.localEulerAngles = new Vector3(newAngle.x, newAngle.y, 0);
    }

    //�p�x�̃��Z�b�g
    void ResetRotate()
    {
        //Q�����������ɁAPathFollower�̍ŏI�p�X��Rotation�Ƀ��Z�b�g����
        if(Input.GetKeyDown(KeyCode.Q))
        {

        }
    }

    void Update()
    {
        Rotate();
        Zoom();
        ResetRotate();
    }
}


