using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lockon : MonoBehaviour
{
    /*******************************
    * private
    *******************************/

    //FPSカメラ関連///////////////////////////////////
    [Header("FPSカメラ専用")]
    [Header("ターゲット")]
    [SerializeField]
    GameObject target;

    [Header("スキャンの当たり判定")]
    [SerializeField]
    SearchMemory Search;

    [Header("スキャン可能枠")]
    [SerializeField]
    GameObject ScanImg;
    Animator ScanImgAnim;
    [SerializeField]
    float scanImagePosY;

    [Header("スキャン不可枠")]
    [SerializeField]
    GameObject ScanGetedImg;
    Animator ScanGetedImgAnim;

    [Header("ゲットできるメモリー")]
    [SerializeField]
    GameObject GetMemoryImg;
    [SerializeField]
    public Vector3 GetMemoryImgPos;

    /////////////////////////////////////////////////

    MemoryType currentScanMemory;

    private void Awake()
    {
        currentScanMemory = new MemoryType();
    }

    private void OnDisable()
    {
        ScanImg.SetActive(false);
        ScanGetedImg.SetActive(false);
    }

    void Start()
    {
        if (CameraManager.instance.GetCurrentCameraType() == (int)CameraManager.CameraType.FPS)
        {
            ScanImg.SetActive(false);
            ScanImgAnim = ScanImg.GetComponent<Animator>();

            ScanGetedImg.SetActive(false);
            ScanGetedImgAnim = ScanGetedImg.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (target == null) return;

        UpdateScanImgTransform();
        PlayScanImgAnim();
        if (Search.isScan)
        {
            StopScanImgAnim();
        }
    }

    void PlayScanImgAnim()
    {
        if (!Search.isScan && ScanImgAnim.speed == 0)
        {
            ScanImgAnim.speed = 1;
            ScanGetedImgAnim.speed = 1;
        }
    }

    void StopScanImgAnim()
    {
        if (Search.isScan)
        {
            ScanImgAnim.speed = 0;
            ScanImgAnim.Play("ScanPossible", 0, 0);
        }
    }

    void UpdateScanImgTransform()
    {
        Ray EnemyRay = new Ray(target.transform.position + new Vector3(0, 50, 0), new Vector3(0, -1, 0)); ;

        foreach (RaycastHit hit in Physics.RaycastAll(EnemyRay))
        {
            if (hit.collider.gameObject == target)
            {
                Debug.Log(target.name + hit.point);
                ScanGetedImg.transform.position = hit.point + new Vector3(0, scanImagePosY, 0);

                ScanImg.transform.position = hit.point + new Vector3(0, scanImagePosY, 0);
            }
        }
        ScanImg.transform.rotation = Camera.main.transform.rotation;
        ScanGetedImg.transform.rotation = Camera.main.transform.rotation;
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

    private void UpdateCuurentScanMemory()
    {
        currentScanMemory = target.gameObject.GetComponent<EnemyBase>().GetMemory();
        if (DataManager.instance.IPlayerData().PossesionMemoryIsContain(currentScanMemory))
        {
            ScanImg.SetActive(false);
            ScanGetedImg.SetActive(true);
            return;
        }         
        else
        {
            ScanImg.SetActive(true);
            ScanGetedImg.SetActive(false);
            GetMemoryImg.GetComponent<Image>().sprite = DataManager.instance.GetMemorySprite(currentScanMemory);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag != "Enemy") return;

        if (target == null)
        {
            target = collision.gameObject;
            UpdateCuurentScanMemory();
            return;
        }
        //スキャンしてる時は、ターゲットを切り替えない
        if (Search.isScan)
        {
            if (target != null)
            {
                return;
            }
        }
        else
        {
            //現在のターゲットと、別のターゲットのどちらがプレイヤーに近いか判断する
            Vector3 PlayerPos = Player.readPlayer.GetPos();
            Vector3 temp = PlayerPos - target.transform.position;
            float targetDistance = temp.x + temp.y + temp.z;
            temp = Player.readPlayer.GetPos() - collision.transform.position;
            float collisionDistance = temp.x + temp.y + temp.z;

            if (collisionDistance <= targetDistance)
            {
                target = collision.gameObject;
                UpdateCuurentScanMemory();
            }
            else
            {
                UpdateCuurentScanMemory();
            }

        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject == target)
        {
            ScanImg.SetActive(false);
            ScanGetedImg.SetActive(false);

            target = null;
        }

    }
}
