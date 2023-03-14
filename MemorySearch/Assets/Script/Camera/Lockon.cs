using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lockon : MonoBehaviour
{
    /*******************************
    * private
    *******************************/

    //FPS�J�����֘A///////////////////////////////////
    [Header("FPS�J������p")]
    [Header("�^�[�Q�b�g")]
    [SerializeField]
    GameObject target;

    [Header("�X�L�����̓����蔻��")]
    [SerializeField]
    SearchMemory Search;

    [Header("�X�L�����\�g")]
    [SerializeField]
    GameObject ScanImg;
    Animator ScanImgAnim;
    [SerializeField]
    float scanImagePosY;

    [Header("�X�L�����s�g")]
    [SerializeField]
    GameObject ScanGetedImg;

    [Header("�v���r���[�̃X�L�����\�g")]
    [SerializeField]
    GameObject PreviewScanImg;
    Animator PreviewScanImgAnim;
    [Header("�v���r���[�̃X�L�����s�g")]
    [SerializeField]
    GameObject PreviewScanGetedImg;

    [Header("�Q�b�g�ł��郁�����[")]
    [SerializeField]
    GameObject GetMemoryPreviewImg;
    [SerializeField]
    public Vector3 GetMemoryImgPos;



    [SerializeField]
    Sprite MemoryPreviewFrameSprite;

    /////////////////////////////////////////////////

    MemoryType currentScanMemory;

    private void Awake()
    {
        currentScanMemory = new MemoryType();
    }

    private void OnDisable()
    {
        ScanImg.SetActive(false);
        PreviewScanImg.SetActive(false);
        ScanGetedImg.SetActive(false);
        PreviewScanGetedImg.SetActive(false);
        GetMemoryPreviewImg.GetComponent<Image>().sprite = MemoryPreviewFrameSprite;
    }

    void Start()
    {
        ScanImg.SetActive(false);
        PreviewScanImg.SetActive(false);
        ScanImgAnim = ScanImg.GetComponent<Animator>();
        PreviewScanImgAnim = PreviewScanImg.GetComponent<Animator>();
        ScanGetedImg.SetActive(false);
        PreviewScanGetedImg.SetActive(false);
        ScanGetedImg.GetComponent<Animator>().speed = 0;
        PreviewScanGetedImg.GetComponent<Animator>().speed = 0;
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
            PreviewScanImgAnim.speed = 1;
        }
    }

    void StopScanImgAnim()
    {
        if (Search.isScan)
        {
            ScanImgAnim.speed = 0;
            PreviewScanImgAnim.speed = 0;
            ScanImgAnim.Play("ScanPossible", 0, 0);
            PreviewScanImgAnim.Play("ScanPossible", 0, 0);
        }
    }

    void UpdateScanImgTransform()
    {
        Ray EnemyRay = new Ray(target.transform.position + new Vector3(0, 50, 0), new Vector3(0, -1, 0)); ;

        foreach (RaycastHit hit in Physics.RaycastAll(EnemyRay))
        {
            if (hit.collider.gameObject == target)
            {
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
    * �Փˏ���
    *******************************/

    private void UpdateCuurentScanMemory()
    {
        GetMemoryPreviewImg.GetComponent<Image>().sprite = DataManager.instance.GetMemorySprite(currentScanMemory);

        currentScanMemory = target.gameObject.GetComponent<EnemyBase>().GetMemory();
        if (DataManager.instance.IPlayerData().PossesionMemoryIsContain(currentScanMemory))
        {
            ScanImg.SetActive(false);
            PreviewScanImg.SetActive(false);
            ScanGetedImg.SetActive(true);
            PreviewScanGetedImg.SetActive(true);
            return;
        }         
        else
        {
            ScanImg.SetActive(true);
            PreviewScanImg.SetActive(true);
            ScanGetedImg.SetActive(false);
            PreviewScanGetedImg.SetActive(false);
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
        //�X�L�������Ă鎞�́A�^�[�Q�b�g��؂�ւ��Ȃ�
        if (Search.isScan)
        {
            if (target != null)
            {
                return;
            }
        }
        else
        {
            //���݂̃^�[�Q�b�g�ƁA�ʂ̃^�[�Q�b�g�̂ǂ��炪�v���C���[�ɋ߂������f����
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
            PreviewScanImg.SetActive(false);
            ScanGetedImg.SetActive(false);
            PreviewScanGetedImg.SetActive(false);
            target = null;
        }

        GetMemoryPreviewImg.GetComponent<Image>().sprite = MemoryPreviewFrameSprite;
    }
}
