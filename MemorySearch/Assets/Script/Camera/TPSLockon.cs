using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSLockon : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [Header("���b�N�I���g")]
    [SerializeField]
    GameObject LockonImg;
    [SerializeField]
    float LockonImagePosY;

    void Start()
    {
       
            LockonImg.SetActive(false);
        
    }

    void Update()
    {
        if (target == null) return;

        UpdateScanImgTransform();
    
    }

    void UpdateScanImgTransform()
    {
        Ray EnemyRay = new Ray(target.transform.position + new Vector3(0, 20, 0), new Vector3(0, -1, 0)); ;

        RaycastHit hit;
        if (target.GetComponent<BoxCollider>().Raycast(EnemyRay, out hit, 30.0f))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                LockonImg.transform.position = hit.point + new Vector3(0, LockonImagePosY, 0);
            }
        }
        LockonImg.transform.rotation = Camera.main.transform.rotation;
    }

    /*******************************
    * public
    *******************************/

    public GameObject GetTarget()
    {
        return target;
    }

    /*******************************
    * �Փˏ���
    *******************************/

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag != "Enemy") return;

        
        if (target == null)
        {
            target = collision.gameObject;

            LockonImg.SetActive(true);

            return;
        }
        //�X�L�������Ă鎞�́A�^�[�Q�b�g��؂�ւ��Ȃ�
       
            //���݂̃^�[�Q�b�g�ƁA�ʂ̃^�[�Q�b�g�̂ǂ��炪�v���C���[�ɋ߂������f����
            Vector3 PlayerPos = Player.readPlayer.GetPos();
            Vector3 temp = PlayerPos - target.transform.position;
            float targetDistance = temp.x + temp.y + temp.z;
            temp = Player.readPlayer.GetPos() - collision.transform.position;
            float collisionDistance = temp.x + temp.y + temp.z;

            if (collisionDistance <= targetDistance)
            {
                target = collision.gameObject;

                LockonImg.SetActive(true);
            }
        
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag != "Enemy") return;
        if (collision.gameObject == target)
        {
            LockonImg.SetActive(false);

            target = null;

        }

    }
}
