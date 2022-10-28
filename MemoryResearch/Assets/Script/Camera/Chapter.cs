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

    [SerializeField]
    GameObject FPSVisibility;

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

        FPSVisibility.SetActive(false);

       
    }

   public void Update()
    {
        if (Script.ChangFlg == true)
        {
            FPSVisibility.SetActive(true);
            //return���͂ŃV���b�^�[������
            //if (Input.GetKeyDown("return") || Input.GetMouseButtonDown(1))
            //{
            //    img.color = new Color(1, 1, 1, 1);
            //}
            //else
            //{
            //    //���̐F�ɖ߂�
            //    img.color = Color.Lerp(img.color, Color.clear, Time.deltaTime);
            //}


            //���N���b�N������
            if (Input.GetMouseButtonDown(0))
            {
                //�J�����̊p�x��ϐ�newAngle�Ɋi�[
                newAngle = mainCamera.transform.localEulerAngles;

                newAngle = FPSVisibility.transform.localEulerAngles;

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

                    FPSVisibility.transform.localEulerAngles = newAngle;

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

                    FPSVisibility.transform.localEulerAngles = newAngle;

                    //�}�E�X���W��ϐ�lastMousePosition�Ɋi�[
                    lastMousePosition = Input.mousePosition;
                }
            }
           
        }
        else
        {
            newAngle = new Vector2(0, 0);

            mainCamera.transform.localEulerAngles = newAngle;

            FPSVisibility.transform.localEulerAngles = newAngle;
            FPSVisibility.SetActive(false);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                //        float search_radius = 10f;

                //        var hits = Physics.SphereCastAll(
                //            player.transform.position,
                //            search_radius,
                //            player.transform.forward,
                //            0.01f,
                //            LayerMask.GetMask("LockonTarget")
                //        ).Select(h => h.transform.gameObject).ToList();

                //        hits = FilterTargetObject(hits);

                //        if (0 < hits.Count())
                //        {
                //            float min_target_distance = float.MaxValue;
                //            GameObject target = null;

                //            foreach (var hit in hits)
                //            {
                //                Vector3 targetScreenPoint = Camera.main.WorldToViewportPoint(hit.transform.position);
                //                float target_distance = Vector2.Distance(
                //                    new Vector2(0.5f, 0.5f),
                //                    new Vector2(targetScreenPoint.x, targetScreenPoint.y)
                //                );
                //               

                //                if (target_distance < min_target_distance)
                //                {
                //                    min_target_distance = target_distance;
                //                    target = hit.transform.gameObject;
                //                }
                //            }

                //            return target;
                //        }
                //        else
                //        {
                //            return null;
                //        }
                //    }

                //    protected List<GameObject> FilterTargetObject(List<GameObject> hits)
                //    {
                //        return hits
                //            .Where(h => {
                //                Vector3 screenPoint = Camera.main.WorldToViewportPoint(h.transform.position);
                //                return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
                //            })
                //            .Where(h => h.tag == "Enemy")
                //            .ToList();
                //    }
                //}
            }
        }
    }
}
