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

    MemoryType scanMemory;

    void Start()
    {
        //�J�[�\�����b�N
        Cursor.lockState = CursorLockMode.Locked;

        player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = player.transform.rotation;

        InitSearchSlider();

        isScan = false;

        scanMemory = new MemoryType();
    }

    void FixedUpdate()
    {
        UpdatePosition();

        RotateCmaeraAngle(viewAngle);

        Scan();
    }

    void UpdatePosition()
    {
        transform.position = player.transform.position + new Vector3(0, 7, 0);
    }

    void RotateCmaeraAngle(float limit)
    {

        float maxLimit = limit, minLimit = 360 - maxLimit;
        var option = DataManager.instance.IOptionData().GetAimOption();
        //X����]
        var localAngle = transform.localEulerAngles;
        localAngle.x -= Input.GetAxis("Mouse Y") * option.sensitivity.y;
        if (localAngle.x > maxLimit && localAngle.x < 180)
            localAngle.x = maxLimit;
        if (localAngle.x < minLimit && localAngle.x > 180)
            localAngle.x = minLimit;
        transform.localEulerAngles = localAngle;
        //Y����]
        var angle = transform.eulerAngles;
        angle.y += Input.GetAxis("Mouse X") * option.sensitivity.x;
        transform.eulerAngles = angle;
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

        if (Input.GetMouseButtonDown(1) && lockon.GetTarget())
        {
            isScan = true;
            scanMemory = lockon.GetTarget().GetComponent<EnemyBase>().GetMemory();
            SoundManager.instance.PlaySe(Chargeclip,transform.position);
        }

        if (!isScan) return;

        //�^�[�Q�b�g�����Ȃ��Ȃ�����
        if (!lockon.GetTarget())
        {
            MissScan();
        }

        ScanUpdate();
    }

    void ScanUpdate()
    {
        //�{�^���𗣂�����T�[�`���s�A�������I��
        if (Input.GetMouseButtonUp(1))
        {
            MissScan();
            return;
        }

        //�T�[�`�Q�[�W�𒙂߂�
        if (SearchSlider.value <1)
        {
            SearchSlider.value += SearcCompleteSpeed;
        }

        if (SearchSlider.value == 1)
        {
            SuccessScan();
        }
    }

    void MissScan()
    {
        isScan = false;
        SoundManager.instance.StopSe(Chargeclip);
        SoundManager.instance.PlaySe(Missclip, transform.position);
        SearchSlider.value = 0;
    }

    void SuccessScan()
    {
        //�X�L�����������𗬂�
        SoundManager.instance.StopSe(Chargeclip);
        SoundManager.instance.PlaySe(Successclip, transform.position);

        //�擾�������������v���C���[�f�[�^�ɓo�^
        DataManager.instance.IPlayerData().AddPossesionMemory(scanMemory);

        getMemoryUI.Play();
        InitSearchSlider();
        isScan = false;
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

