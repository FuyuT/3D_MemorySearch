using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter : MonoBehaviour
{
    private Image img;

    //ChangeCamera�̃X�N���v�g
    ChangeCamera Script;

    [SerializeField]
    GameObject CameraControll;

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

    void Start()
    {
        img = GetComponent<Image>();
        img.color = Color.clear;

        Script = CameraControll.GetComponent<ChangeCamera>();
    }

    void Update()
    {
        if (Script.ChangFlg == true)
        {
            //return���͂ŃV���b�^�[������
            if (Input.GetKeyDown("return"))
            {
                img.color = new Color(1, 1, 1, 1);
            }
            else
            {
                //���̐F�ɖ߂�
                img.color = Color.Lerp(img.color, Color.clear, Time.deltaTime);
            }
            //���N���b�N������
            if (Input.GetMouseButtonDown(0))
            {
                //�J�����̊p�x��ϐ�newAngle�Ɋi�[
                newAngle = mainCamera.transform.localEulerAngles;

                //�}�E�X���W��ϐ�lastMousePosition�Ɋi�[
                lastMousePosition = Input.mousePosition;
            }
            //���h���b�O���Ă����
            else if (Input.GetMouseButton(0))
            {
                //�J������]�����̔���t���O��true�̏ꍇ
                if (!reverse)
                {
                    // Y���̉�]�F�}�E�X�h���b�O�����Ɏ��_��]
                    // �}�E�X�̐����ړ��l�ɕϐ�rotationSpeed���|����
                    //�i�N���b�N���̍��W�ƃ}�E�X���W�̌��ݒl�̍����l�j
                    newAngle.y -= (lastMousePosition.x - Input.mousePosition.x) * rotationSpeed.y;
                   
                    // X���̉�]�F�}�E�X�h���b�O�����Ɏ��_��]
                    // �}�E�X�̐����ړ��l�ɕϐ�rotationSpeed���|����
                    //�i�N���b�N���̍��W�ƃ}�E�X���W�̌��ݒl�̍����l�j
                    newAngle.x -= (Input.mousePosition.y - lastMousePosition.y) * rotationSpeed.x;
                   
                    //newAngle�̊p�x���J�����p�x�Ɋi�[
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

                    //�}�E�X���W��ϐ�lastMousePosition�Ɋi�[
                    lastMousePosition = Input.mousePosition;
                }
            }
        }
    }
}
