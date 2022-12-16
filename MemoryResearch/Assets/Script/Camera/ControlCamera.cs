using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ControlCamera : MonoBehaviour
{
    //ChangeCamera�̃X�N���v�g
    ChangeMoveObjectCamera Script;

    [SerializeField]
    GameObject ControlleCamera;

    [SerializeField]
    GameObject USBObject;

    // �J�����I�u�W�F�N�g���i�[����ϐ�
    public Camera mainCamera;

    // �J�����̉�]���x���i�[����ϐ�
    public Vector2 rotationSpeed;

    // �}�E�X�ړ������ƃJ������]�����𔽓]���锻��t���O
    public bool reverse;

    // �}�E�X���W���i�[����ϐ�
    private Vector2 lastMousePosition;

    // �J�����̊p�x���i�[����ϐ��i�����l��0,0�����j
    private Vector2 newAngle = new Vector2(0, 0);

    [Header("�J�����}�l�[�W���[")]
    [SerializeField] CameraManager camemana;

    //�Y�[���p�ϐ�
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

        //���N���b�N������
        if (Input.GetMouseButtonDown(1))
        {
            //�J�����̊p�x��ϐ�newAngle�Ɋi�[
            newAngle = mainCamera.transform.localEulerAngles;

            newAngle = ControlleCamera.transform.localEulerAngles;

            //�}�E�X���W��ϐ�lastMousePosition�Ɋi�[
            lastMousePosition = Input.mousePosition;
        }
        //���h���b�O���Ă����
        else if (Input.GetMouseButton(1))
        {
            //�J������]�����̔���t���O��true�̏ꍇ
            if (!reverse)
            {
                // Y���̉�]�F�}�E�X�h���b�O�����Ɏ��_��]
                // �}�E�X�̐����ړ��l�ɕϐ�rotationSpeed���|����
                //�i�N���b�N���̍��W�ƃ}�E�X���W�̌��ݒl�̍����l�j
                newAngle.y -= (lastMousePosition.x - Input.mousePosition.x) * rotationSpeed.y;

                ////����ȏ�͉�]�ł��Ȃ�
                //if (newAngle.y <= 90)
                //{
                //    rotationSpeed.y = 0;
                //}
                //if (newAngle.x <= -33)
                //{
                //    rotationSpeed.x = 0;
                //}

                // X���̉�]�F�}�E�X�h���b�O�����Ɏ��_��]
                // �}�E�X�̐����ړ��l�ɕϐ�rotationSpeed���|����
                //�i�N���b�N���̍��W�ƃ}�E�X���W�̌��ݒl�̍����l�j
                newAngle.x -= (Input.mousePosition.y - lastMousePosition.y) * rotationSpeed.x;

                //newAngle�̊p�x���J�����p�x�Ɋi�[
                mainCamera.transform.localEulerAngles = newAngle;

                mainCamera.transform.localEulerAngles = newAngle;

                //�}�E�X���W��ϐ�lastMousePosition�Ɋi�[
                lastMousePosition = Input.mousePosition;
            }
            //�J������]�����̔���t���O��reverse�̏ꍇ
            else if (reverse)
            {
                //Y���̉�]�F�}�E�X�h���b�O�Ƌt�����Ɏ��_��]
                newAngle.y -= (Input.mousePosition.x - lastMousePosition.x) * rotationSpeed.y;

                //X���̉�]�F�}�E�X�h���b�O�Ƌt�����Ɏ��_��]
                newAngle.x -= (lastMousePosition.y - Input.mousePosition.y) * rotationSpeed.x;

                //newAngle�̊p�x���J�����p�x�Ɋi�[
                mainCamera.transform.localEulerAngles = newAngle;

                mainCamera.transform.localEulerAngles = newAngle;

                //�}�E�X���W��ϐ�lastMousePosition�Ɋi�[
                lastMousePosition = Input.mousePosition;
            }

            //�}�E�X�z�[���h�ŃY�[���C���E�Y�[���A�E�g
            var scroll = Input.mouseScrollDelta.y;
            mainCamera.transform.position -= -mainCamera.transform.forward * scroll * ZoomSpeed;

            //���ȏ�Y�[���͏o���Ȃ�����
            if (mainCamera.transform.position.y <= 0)
            {
                ZoomSpeed = 0;
            }
        }
    }
}
