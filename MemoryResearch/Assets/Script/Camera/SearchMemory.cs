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

    float _inputX, _inputY;
    [SerializeField]
    float viewAngle;

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


    //SE�֘A/////////////////////////
    [Header("�T�E���h�}�l�[�W���[")]
    [SerializeField]
    SoundManager soundManager;
    public AudioClip Successclip;
    bool SuccessisPlaying = false;

    public AudioClip Missclip;
    bool MissisPlaying = false;

    public AudioClip Chargeclip;
    bool ChargeisPlaying = false;
    ///////////////////////////////////////////
    GameObject mainCamera;

    //�I�v�V�����̏����擾
    [SerializeField] OptionManager optionManager;

    //Lockon�̃X�N���v�g
    [SerializeField]
    Lockon lockon;

    GameObject lockOnTarget;

    void Start()
    {
        //�J�[�\����\��
        Cursor.lockState = CursorLockMode.Locked;

        player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = player.transform.rotation;

        CompleteText.SetActive(false);

        mainCamera = Camera.main.gameObject;

        //optionManager = GameObject.Find("Option").GetComponent<OptionManager>();

        SearcSlider.value = 0;
        SuccessisPlaying = false;
        MissisPlaying= false;
        ChargeisPlaying = false;
    }

     void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 7, 0);

        Scan();
        _inputX = Input.GetAxis("Mouse X");
        _inputY = Input.GetAxis("Mouse Y");

        rotateCmaeraAngle(_inputX,_inputY,viewAngle);

      
    }
     void rotateCmaeraAngle(float _inputX, float _inputY, float limit)
     {
        
        float maxLimit = limit, minLimit = 360 - maxLimit;
        //X����]
        var localAngle = transform.localEulerAngles;
        localAngle.x -= _inputY*sliderY.value;
        if (localAngle.x > maxLimit && localAngle.x < 180)
            localAngle.x = maxLimit;
        if (localAngle.x < minLimit && localAngle.x > 180)
            localAngle.x = minLimit;
        transform.localEulerAngles = localAngle;
        //Y����]
        var angle = transform.eulerAngles;
        angle.y += _inputX*sliderX.value;
        transform.eulerAngles = angle;
      
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

    void Scan()
    {
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


        lockOnTargetObject(lockOnTarget);
        //���N���b�N�����Ƃ��Ƀ������i�A�N�V�����j��o�^
        if (Input.GetMouseButton(1))
        {
            if (lockOnTarget)
            {
                //�`���[�WSE�𗬂�
                ChargeSE();
                if (SearcSlider.value <= 1)
                {
                    ChargeisPlaying = false;
                    SearcSlider.value += SearcCompleteSpeed;
                    //�X�L��������
                    if (SearcSlider.value >= 1)
                    {
                        //�X�L�����������𗬂�
                        SuccessSE();
                        SetPossesionMemory(lockOnTarget);

                        //�e�L�X�g�֘A
                        CompleteText.SetActive(true);
                        timer = 5f;
                        SearcSlider.value = 0;

                    }

                    //�X�L�������s

                }
               
            }
            else
            {
                Debug.Log("�O�ꂽ");
                MissSE();
                //ChargeisPlaying = false;
            }
        }
        else
        {
            SearcSlider.value = 0;
            SuccessisPlaying = false;
            ChargeisPlaying = false;
            MissisPlaying = false;
        }
        if (CompleteText.activeSelf)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                CompleteText.SetActive(false);
            }
        }
    }

    void ChargeSE()
    {
        if (!ChargeisPlaying)
        {
            soundManager.PlaySe(Chargeclip);
            ChargeisPlaying = true;
        }
    }

    void MissSE()
    {
        if (!MissisPlaying)
        {
            soundManager.StopSe(Chargeclip);
            soundManager.PlaySe(Missclip);
            MissisPlaying = true;
        }
    }

    void SuccessSE()
    {
        if(!SuccessisPlaying)
        {
            soundManager.StopSe(Chargeclip);
            soundManager.PlaySe(Successclip);
            SuccessisPlaying = true;
        }
    }
}
