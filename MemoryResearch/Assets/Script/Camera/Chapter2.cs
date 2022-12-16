using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter2 : MonoBehaviour
{
    public GameObject player;
    //public float rotate_speed;

    public float high;
    public float profound;

    private const int ROTATE_BUTTON = 1;
    private const float ANGLE_LIMIT_UP = 60f;
    private const float ANGLE_LIMIT_DOWN = -60f;


    //todo ��Ńe�L�X�g�}�l�[�W���Ɉڂ�
    //�e�L�X�g�֘A
    [Header("�`���v�^�[�����e�L�X�g")]
    [SerializeField]
    GameObject CompleteText;
    public float timer;

    [Header("�I�v�V�����X���C�_�[X")]
    public Slider sliderX;
    [Header("�I�v�V�����X���C�_�[Y")]
    public Slider sliderY;
    [Header("�`���v�^�[�X���C�_�[")]
    public Slider ChapterSlider;

    GameObject mainCamera;

    //���]�@�\��AimX��AimY������炤
    [SerializeField] GameObject Aimx;
    [SerializeField] GameObject Aimy;
    AimX aimx;
    AimY aimy;

    //Lockon�̃X�N���v�g
    [SerializeField]
    Lockon lockon;

    GameObject lockOnTarget;

    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = player.transform.rotation;

        CompleteText.SetActive(false);

        mainCamera = Camera.main.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");

        aimx = Aimx.GetComponent<AimX>();
        aimy = Aimy.GetComponent<AimY>();
    }

    public void Update()
    {

        rotateCmaeraAngle();
        
    }
    private void FixedUpdate()
    {

        transform.position = player.transform.position+new Vector3(0, 7, 0);
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y + high, player.transform.position.z + profound);

        GameObject target = lockon.getTarget();

        //transform.position = player.transform.position;

        if (target != null)
        {
            lockOnTarget = target;
        }
        else
        {
            lockOnTarget = null;
        }

        if (lockOnTarget)
        {

            lockOnTargetObject(lockOnTarget);
            //���N���b�N�����Ƃ��Ƀ������i�A�N�V�����j��o�^
            if (Input.GetMouseButton(0))
            {
                SetPossesionMemory(lockOnTarget);

                //�e�L�X�g�֘A
                CompleteText.SetActive(true);
                timer = 5f;

                if (Input.GetMouseButton(0))
                {
                    SetPossesionMemory(lockOnTarget);
                }
            }
        }
        if (CompleteText.activeSelf)
        {
            timer -= Time.deltaTime;
            
            if (timer <= 0)
            {
                CompleteText.SetActive(false);
            }
            //if (Input.GetMouseButton(ROTATE_BUTTON))
            //{
            rotateCmaeraAngle();
            // }
        }

        rotateCmaeraAngle();

        float angle_x = 180f <= transform.eulerAngles.x ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
        transform.eulerAngles = new Vector3(
            Mathf.Clamp(angle_x, ANGLE_LIMIT_DOWN, ANGLE_LIMIT_UP),
            transform.eulerAngles.y,
            transform.eulerAngles.z
        );
        
    }
    private void rotateCmaeraAngle()
    {
        //Y���������]
        //if (aimx.XOnOff && !aimy.YOnOff)
        //{
        //    Vector3 angle = new Vector3(
        //        Input.GetAxis("Mouse X") * sliderX.value,

        //        Input.GetAxis("Mouse Y") * sliderY.value,
        //        0
        //    );
        //    transform.eulerAngles += new Vector3(angle.y, angle.x);
        //}
        //X���������]
        //else if (!aimx.XOnOff && aimy.YOnOff)
        //{
        //    Vector3 angle = new Vector3(
        //        Input.GetAxis("Mouse X") * sliderX.value,
        //        Input.GetAxis("Mouse Y") * sliderY.value,
        //        0
        //    );
        //    Debug.Log(aimx.XOnOff);
        //    transform.eulerAngles += new Vector3(angle.y, angle.x);
        //}
        //X,Y�����]
        //else if (aimx.OnOff && aimy.OnOff)
        //{
        //    Vector3 angle = new Vector3(
        //        Input.GetAxis("Mouse X") * -sliderX.value,

        //        Input.GetAxis("Mouse Y") * -sliderY.value,
        //        0
        //    );
        //    transform.eulerAngles += new Vector3(angle.y, angle.x);
        //}
        //else
        //{
        Vector3 angle = new Vector3(
            Input.GetAxis("Mouse X") * sliderX.value,
            Input.GetAxis("Mouse Y") * -sliderX.value,
            0
        );
        transform.eulerAngles += new Vector3(angle.y, angle.x);
        //}
    }
    private void lockOnTargetObject(GameObject target)
    {

    }

    /// <summary>
    /// �v���C���[�̏������Ă��郁�����z��ɁA�T�[�`�����G����擾�������������i�[����
    /// </summary>
    /// <param name="target">�T�[�`�����G</param>
    private void SetPossesionMemory(GameObject target)
    {
        //todo:�����̈ʒu����������
        //�擾�������������v���C���[�ɐݒ�
        int targetMemory = target.GetComponent<Enemy>().param.Get<int>((int)Enemy.ParamKey.PossesionMemory);
        //�󂢂Ă���z��ԍ�������Γo�^
        var p = player.GetComponent<Player>();
        int arrayValue = p.GetMemoryArrayNullValue(targetMemory);
        if (arrayValue != -1)
        {
            //todo:�o�^�z��ԍ���ύX
            p.SetPossesionMemory(targetMemory, arrayValue);
        }
    }
}
