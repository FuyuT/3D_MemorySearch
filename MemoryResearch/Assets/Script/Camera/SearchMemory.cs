using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchMemory : MonoBehaviour
{
    /*******************************
    * private
    *******************************/

    [SerializeField] GameObject player;

    [SerializeField]
    float viewAngle;

    //�e�L�X�g�֘A
    [Header("�`���v�^�[����UI")]
    [SerializeField]
    GetMemoryUI getMemoryUI;

    //�I�v�V�����֘A/////////////////////////

    [Header("�I�v�V�����X���C�_�[X")]
    [SerializeField] Slider sliderX;
    [Header("�I�v�V�����X���C�_�[Y")]
    [SerializeField] Slider sliderY;
    //�I�v�V�����̏����擾
    [SerializeField] OptionManager optionManager;
    ///////////////////////////////////////////

    //SE�֘A/////////////////////////
    [Header("�T�E���h�}�l�[�W���[")]
    [SerializeField]
    SoundManager soundManager;
    [SerializeField] AudioClip Successclip;

    [SerializeField] AudioClip Missclip;

    [SerializeField] AudioClip Chargeclip;

    
    ///////////////////////////////////////////

    [Header("�T�[�`�X���C�_�[")]
    [SerializeField] Slider SearchSlider;
    [SerializeField] float SearcCompleteSpeed;

    //Lockon�̃X�N���v�g
    [SerializeField]
    Lockon lockon;

    bool Successcomplete;
    bool ScanStart;

    public enum SEType
    {
        
    }

    void Start()
    {
        //�J�[�\����\��
        Cursor.lockState = CursorLockMode.Locked;

        player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = player.transform.rotation;

        getMemoryUI.Stop();

        InitSearchSlider();

        isScan = false;
        ScanStart = false;
        Successcomplete = false;
    }

    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 7, 0);

        RotateCmaeraAngle(viewAngle);

        Scan();
    }

    void RotateCmaeraAngle(float limit)
    {

        float maxLimit = limit, minLimit = 360 - maxLimit;
        //X����]
        var localAngle = transform.localEulerAngles;
        localAngle.x -= Input.GetAxis("Mouse Y") * sliderY.value;
        if (localAngle.x > maxLimit && localAngle.x < 180)
            localAngle.x = maxLimit;
        if (localAngle.x < minLimit && localAngle.x > 180)
            localAngle.x = minLimit;
        transform.localEulerAngles = localAngle;
        //Y����]
        var angle = transform.eulerAngles;
        angle.y += Input.GetAxis("Mouse X") * sliderX.value;
        transform.eulerAngles = angle;

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

    //Active�ɂȂ�����
    void OnEnable()
    {
        //�v���C���[�̊p�x�ɍ��킹��
        transform.rotation = player.transform.rotation;
    }


    void Scan()
    {
        //�������擾����UI���Đ�
        if (lockon.GetTarget())
        {

        }

        if (Input.GetMouseButtonDown(1) && lockon.GetTarget())
        {
            isScan = true;
        }

        if (!isScan) return;

        if (!lockon.GetTarget())
        {
            MissScan();
            Successcomplete = false;
            ScanStart = false;
        }

        ScanUpdate();
    }

    void ScanUpdate()
    {

        //�`���[�WSE�𗬂�
        if (SearchSlider.value <1)
        {
            if (!ScanStart)
            {
                soundManager.PlaySe(Chargeclip);
            }
            SearchSlider.value += SearcCompleteSpeed;
            ScanStart = true;


            if (Input.GetMouseButtonUp(1) && !Successcomplete)
            {
                MissScan();
                return;
            }
        }

        if (!lockon.GetTarget())
        {
            MissScan();
            ScanStart = false;
        }

        if (SearchSlider.value == 1 && !Successcomplete)
        {
            //�X�L�����������𗬂�
            soundManager.StopSe(Chargeclip);
            soundManager.PlaySe(Successclip);
            SetPossesionMemory(lockon.GetTarget());
            getMemoryUI.Play();
            Successcomplete = true;
        }

    }

    void MissScan()
    {
        isScan = false;
        soundManager.StopSe(Chargeclip);
        soundManager.PlaySe(Missclip);
        SearchSlider.value = 0;
    }

    //void ChargeSE()
    //{
    //    //if (!soundManager.IsPlayingSe(Chargeclip))
    //    //{
    //    //    soundManager.StopSe(Missclip);
    //    //    soundManager.PlaySe(Chargeclip);
    //    //}
    //}

    //void MissSE()
    //{
    //    if (!soundManager.IsPlayingSe(Missclip))
    //    {
    //        soundManager.StopSe(Chargeclip);
    //        soundManager.PlaySe(Missclip);
    //    }
    //}

    //void SuccessSE()
    //{
    //    if (!soundManager.IsPlayingSe(Successclip))
    //    {
    //        soundManager.StopSe(Chargeclip);
    //        //soundManager.StopSe(Missclip);
    //        soundManager.PlaySe(Successclip);
    //    }
    //}
    

    /*******************************
    * public
    *******************************/
    public bool isScan { get; private set; }

    public void InitSearchSlider()
    {
        SearchSlider.value = 0;
    }
}

