using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public GameObject user; // an object to follow
    public Vector3 offset; // offset form the target object

    [SerializeField] private float distance = 4.0f; // distance from following object
    [SerializeField] private float YAngle = 0.0f; // angle with y-axis
    [SerializeField] private float XAngle = 0.0f; // angle with x-axis

    //�}�E�X�ł̑���֘A////////////////////////////////////
    [SerializeField] private float minPolarAngle = 5.0f;
    [SerializeField] private float maxPolarAngle = 75.0f;
    //////////////////////////////////////////////////////////

    //�L�[���͊֘A///////////////////////////////////////////
    [SerializeField] public KeyCode RightKeyCord;
    [SerializeField] public KeyCode LeftKeyCord;
    [SerializeField] public KeyCode UpKeyCord;
    [SerializeField] public KeyCode DownKeyCord;

    float angle;
    /////////////////////////////////////////////////////////

    //���C�֘A///////////////////////////////////////////////
    [Header("��Q���̃��C���[")]
    [SerializeField]
    private LayerMask WallLayer;

    [SerializeField]
    private LayerMask CeilingLayer;

    RaycastHit hit;
    //////////////////////////////////////////////////////////
    public float BaseRotationSpeed;

    //Lockon�֘A/////////////////////////////////////////////
    [SerializeField]
    TPSLockon lockon;
    bool isLockon;

    [Header("���b�N�I���g")]
    [SerializeField]
    GameObject LockonImg;
    //////////////////////////////////////////////////////////

    ///�N�C�b�N�J�����֘A//////////////////////////////////// 
    [SerializeField]
    GameObject DirectionObject;
    bool RotetionFrg;
    /////////////////////////////////////////////////////////

    private void Start()
    {
        isLockon = false;
        LockonImg.SetActive(false);

        RotetionFrg = false;
    }

    void Update()
    {
        //���b�N�I���@�\
        //1,Q�L�[���͂ŋN��
        //2,�����G���͈͂ɓ�������G�̕�������������
        //3,�����łȂ���΃v���C���[�������Ă�������ɃJ������������
        if (Input.GetKeyDown(KeyCode.Q))
        {

            if (lockon.GetTarget())
            {
                Debug.Log("a");

                isLockon = !isLockon;
                ChangeIsLockOn();
            }
            else if(lockon.GetTarget()==null)
            {
                RotetionFrg = !RotetionFrg;
                PlayerDirectionCamera();
            }
        }
    }

    void FixedUpdate()
    {
        if (isLockon)
        {
            LockonUpdate();
        }
        else
        {
            Cursor.visible = false;
            UpdateAngle();
        }
    }

    void ChangeIsLockOn()
    {
        if (isLockon)
        {
            LockonImg.SetActive(true);
        }
        else
        {
            LockonImg.SetActive(false);
        }
    }


    void UpdateAngle()
    {
        //���͂Ŏ��_��ύX
        AngleChangeForInput();

        var lookAtPos = user.transform.position + offset;
        UpdatePosition(lookAtPos);
        if (!AdjustAngle())
        {
            transform.LookAt(lookAtPos);
        }
    }

    void AngleChangeForInput()
    {
        Vector2 input = Vector2.zero;
        //�L�[���͂�input�ɔ��f
        input.x = Input.GetKey(LeftKeyCord)  ? -1  : input.x;
        input.x = Input.GetKey(RightKeyCord) ? 1   : input.x;
        input.y = Input.GetKey(UpKeyCord)    ? 1   : input.y;
        input.y = Input.GetKey(DownKeyCord)  ? -1  : input.y;
        //�L�[���͂�������΁A�}�E�X�̓��͂�����
        if (input == Vector2.zero)
        {
            input.x = Input.GetAxis("Mouse X") * DataManager.instance.IOptionData().GetAimOption().sensitivity.x;
            input.y = Input.GetAxis("Mouse Y") * DataManager.instance.IOptionData().GetAimOption().sensitivity.y;
        }
        else
        {
            input.x *= DataManager.instance.IOptionData().GetAimOption().sensitivity.x;
            input.y *= DataManager.instance.IOptionData().GetAimOption().sensitivity.y;
        }

        ChangeAngle(input.x, input.y);
    }

    void ChangeAngle(float x, float y)
    {
        x = XAngle - x * DataManager.instance.IOptionData().GetAimOption().sensitivity.x * BaseRotationSpeed;
        ;
        XAngle = Mathf.Repeat(x, 360);

        y = YAngle + y * DataManager.instance.IOptionData().GetAimOption().sensitivity.y * BaseRotationSpeed;
        YAngle = Mathf.Clamp(y, minPolarAngle, maxPolarAngle);
    }

    /// <summary>
    /// �A���O���𒲐�����
    /// </summary>
    /// <returns>�J�����̑O�ɃX�e�[�W���������Ȃ�true�A�Ȃ����false��Ԃ�</returns>
    bool AdjustAngle()
    {
        bool isHitStage = false;

        //�X�e�[�W�̕ǁA�V��ɓ������Ă�����
        if (Physics.Linecast(user.transform.position, transform.position, out hit, WallLayer))
        {
            transform.position = hit.point + new Vector3(0, 20, 0);
            isHitStage = true;
        }
        if (Physics.Linecast(user.transform.position, transform.position, out hit, CeilingLayer))
        {
            transform.position = hit.point - new Vector3(0, 5, 0);
            isHitStage = true;
        }

        //�����_��ύX
        if (isHitStage)
        {
            transform.LookAt(user.transform);
        }

        return isHitStage;
    }

    void UpdatePosition(Vector3 lookAtPos)
    {
        var da = XAngle * Mathf.Deg2Rad;
        var dp = YAngle * Mathf.Deg2Rad;
        transform.position = new Vector3(
            lookAtPos.x + distance * Mathf.Sin(dp) * Mathf.Cos(da),
            lookAtPos.y + distance * Mathf.Cos(dp),
            lookAtPos.z + distance * Mathf.Sin(dp) * Mathf.Sin(da));
    }

    void LockonUpdate()
    {
        if (lockon.GetTarget())
        {
            var lookAtPos = user.transform.position + offset;
            UpdatePosition(lookAtPos);
            transform.LookAt(lockon.GetTarget().transform, Vector3.up);
        }
        else
        {
            isLockon = false;
            ChangeIsLockOn();
        }
    }

    void PlayerDirectionCamera()
    {

       // XAngle = Mathf.Repeat(x, 360);
        YAngle = Mathf.Repeat(90, 360);
        var lookAtPos = DirectionObject.transform.position + offset;
        UpdatePosition(lookAtPos);
    }
}
