using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchMemory : MonoBehaviour
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
    [Header("�T�[�`�X���C�_�[")]
    public Slider SearcSlider;
    public float SearcCompleteSpeed;

    GameObject mainCamera;

    //�I�v�V�����̏����擾
    [SerializeField] OptionManager optionManager;

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

        //optionManager = GameObject.Find("Option").GetComponent<OptionManager>();

        SearcSlider.value = 0;
    }

     void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 7, 0);

        GameObject target = lockon.getTarget();

        if (target != null)
        {
            lockOnTarget = target;
        }
        else
        {
            lockOnTarget = null;
            SearcSlider.value = 0;
        }

        if (lockOnTarget)
        {
            SearcSlider.value += 0.1f;
            lockOnTargetObject(lockOnTarget);
            //���N���b�N�����Ƃ��Ƀ������i�A�N�V�����j��o�^

            if (SearcSlider.value == 1)
            {
                if (Input.GetMouseButton(0))
                {
                    SetPossesionMemory(lockOnTarget);

                    //�e�L�X�g�֘A
                    CompleteText.SetActive(true);
                    timer = 5f;
                    SearcSlider.value = 0;
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
        }

        rotateCmaeraAngle();

      
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
            Input.GetAxis("Mouse Y") * -sliderY.value,
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
        //todo:�����̈ʒu���������� �擾�������������v���C���[�ɐݒ�

        //int targetMemory = target.GetComponent<Enemy>().param.Get<int>((int)Enemy.ParamKey.PossesionMemory);
        //�󂢂Ă���z��ԍ�������Γo�^
        //var p = player.GetComponent<Player>();
        //int arrayValue = p.GetMemoryArrayNullValue(targetMemory);
        //if (arrayValue != -1)
        //{
        //    //todo:�o�^�z��ԍ���ύX
        //    p.SetPossesionMemory(targetMemory, arrayValue);
        //}
    }

    void OnEnable()
    {
        transform.rotation = player.transform.rotation;
    }
}
