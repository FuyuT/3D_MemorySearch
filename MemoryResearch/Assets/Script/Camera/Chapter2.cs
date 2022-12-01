using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2 : MonoBehaviour
{
    public GameObject player;
    public float rotate_speed;

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


    GameObject mainCamera;

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

    }

    void Update()
    {
        Debug.Log("Update");
    }

    private void FixedUpdate()
    {

        transform.position = player.transform.position + new Vector3(0, 10, 5);
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y + high, player.transform.position.z + profound);

        GameObject target = lockon.getTarget();



        transform.position = player.transform.position;

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
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("�N���b�N");
                SetPossesionMemory(lockOnTarget);

                //�e�L�X�g�֘A
                CompleteText.SetActive(true);
                timer = 5f;

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
     
        Vector3 angle = new Vector3(
            Input.GetAxis("Mouse X") * rotate_speed,
            Input.GetAxis("Mouse Y") * -rotate_speed,
            0
        );

        transform.eulerAngles += new Vector3(angle.y, angle.x);
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
        //�G�������Ă��郁�������擾
        int targetMemory = target.GetComponent<Enemy>().param.Get<int>((int)Enemy.ParamKey.PossesionMemory);
        var p = player.GetComponent<Player>();
        //�󂢂Ă���z��ԍ����m�F
        int arrayValue = p.GetMemoryArrayNullValue(targetMemory);
        
        if (arrayValue != -1)
        {
            int setMemory = targetMemory;
            if (p.CheckPossesionMemory(targetMemory))
            {
                switch (targetMemory)
                {
                    case (int)Player.Event.Jump:
                        setMemory = (int)Player.Event.Double_Jump;
                        break;
                    default:
                        break;
                }
            }
            p.SetPossesionMemory(setMemory, arrayValue);
        }
    }
}
