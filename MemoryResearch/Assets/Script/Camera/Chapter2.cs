using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2 : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;
    public float rotate_speed;

    private const int ROTATE_BUTTON = 1;
    private const float ANGLE_LIMIT_UP = 60f;
    private const float ANGLE_LIMIT_DOWN = -60f;

    [SerializeField]
    GameObject Enemy1;

    [SerializeField]
    GameObject Enemy2;

    //Lockonのスクリプト
    Lockon lockon;

    GameObject lockOnTarget;

   
    void Start()
    {

        mainCamera = Camera.main.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        lockon = player.GetComponentInChildren<Lockon>();
       
    }

    void Update()
    {
        transform.position = player.transform.position;

        //if (Input.GetKeyDown(KeyCode.R))
        //{

            GameObject target = lockon.getTarget();

            if (target != null)
            {
                lockOnTarget = target;
            }
            else
            {
                lockOnTarget = null;
            }
        //}

        if (lockOnTarget)
        {
            lockOnTargetObject(lockOnTarget);
        }
        else
        {
            if (Input.GetMouseButton(ROTATE_BUTTON))
            {
                rotateCmaeraAngle();
            }
        }

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
            Input.GetAxis("Mouse Y") * rotate_speed,
            0
        );

        transform.eulerAngles += new Vector3(angle.y, angle.x);
    }
    private void lockOnTargetObject(GameObject target)
    {
        if (target == Enemy1)
        { 
            Debug.Log("a");
           // transform.LookAt(target.transform, Vector3.up);
        }

        if (target == Enemy2)
        {
            Debug.Log("b");
            //transform.LookAt(target.transform, Vector3.up);
        }
    }
}
