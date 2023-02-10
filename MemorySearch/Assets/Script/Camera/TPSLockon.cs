using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSLockon : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [Header("ロックオン枠")]
    [SerializeField]
    GameObject LockonImg;
    [SerializeField]
    float LockonImagePosY;

    void Start()
    {
        target = null;
    }

    void Update()
    {
        if (target == null) return;

        UpdateScanImgTransform();
    }

    void UpdateScanImgTransform()
    {
        Ray EnemyRay = new Ray(target.transform.position + new Vector3(0, 20, 0), new Vector3(0, -1, 0)); ;

        foreach (RaycastHit hit in Physics.RaycastAll(EnemyRay))
        {
            if (hit.collider.gameObject == target)
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
    * 衝突処理
    *******************************/

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag != "Enemy") return;

        if (target == null)
        {
            target = collision.gameObject;
            return;
        }
        //現在のターゲットと、別のターゲットのどちらがプレイヤーに近いか判断する
        Vector3 PlayerPos = Player.readPlayer.GetPos();
        Vector3 temp = PlayerPos - target.transform.position;
        float targetDistance = temp.x + temp.y + temp.z;
        temp = Player.readPlayer.GetPos() - collision.transform.position;
        float collisionDistance = temp.x + temp.y + temp.z;
       
        //近い方にターゲットを切り替える
        if (collisionDistance <= targetDistance)
        {
            target = collision.gameObject;
        }     
    }

    private void OnTriggerExit(Collider collision)
    {
        //ターゲットがエリアから外れたら、ターゲットをnullにする
        if (collision.gameObject.tag != "Enemy") return;
        if (collision.gameObject == target)
        {
            target = null;
        }
    }
}
