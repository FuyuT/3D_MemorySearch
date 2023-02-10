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
    
    ///////////////////////////////////////////

    [Header("�T�[�`�X���C�_�[")]
    [SerializeField] Slider SearchSlider;
    [SerializeField] float SearcCompleteSpeed;

     [Header("�Q�b�g�����������[")]
    [SerializeField]
    GameObject IsGetMemoryImg;
    [SerializeField]
    public Vector3 IsGetMemoryImgPos;

    //Lockon�̃X�N���v�g
    [SerializeField]
    Lockon lockon;

    //ScanMemoryUI
    [SerializeField]
    MemoryUI memoryUI;

    MemoryType scanMemory;

    public bool isScan { get; private set; }

    private void OnEnable()
    {
        transform.rotation = player.transform.rotation;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        InitSearchSlider();

        isScan = false;

        scanMemory = new MemoryType();

        IsGetMemoryImg.SetActive(false);
    }

    private void Update()
    {
        ScanStartUpdate();
    }

    void FixedUpdate()
    {
        RotateCmaeraAngle(viewAngle);
        UpdatePosition();
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

    void  ScanStartUpdate()
    {
        if (!lockon.GetTarget()) return;
        if (DataManager.instance.IPlayerData().PossesionMemoryIsContain(lockon.GetTarget().GetComponent<EnemyBase>().GetMemory())) return;
           
        if (Input.GetMouseButtonDown(1))
        {
            isScan = true;
            scanMemory = lockon.GetTarget().GetComponent<EnemyBase>().GetMemory();
        }
    }

    void Scan()
    {
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
        SoundManager.instance.PlaySe(Missclip, transform.position);
        SearchSlider.value = 0;
    }

    void SuccessScan()
    {
        //�X�L�����������𗬂�
        SoundManager.instance.PlaySe(Successclip, transform.position);

        //�擾�������������v���C���[�f�[�^�ɓo�^
        DataManager.instance.IPlayerData().AddPossesionMemory(scanMemory);

        getMemoryUI.Play();
        IsGetMemoryImg.SetActive(true);
        IsGetMemoryImg.GetComponent<Image>().sprite = DataManager.instance.GetMemorySprite(scanMemory);

        InitSearchSlider();
        
        isScan = false;
    }


    /*******************************
    * public
    *******************************/

    public void InitSearchSlider()
    {
        SearchSlider.value = 0;
    }

    public void ScanAnimStart()
    {

    }
}

