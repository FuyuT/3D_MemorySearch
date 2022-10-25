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


    //Lockonのスクリプト
    [SerializeField]
    Lockon lockon;

    GameObject lockOnTarget;

   
    void Start()
    {
        mainCamera = Camera.main.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
      
        transform.position = player.transform.position;
        GameObject target = lockon.getTarget();

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
            //左クリックしたときにメモリ（アクション）を登録
            if(Input.GetMouseButton(0))
            {
                SetPossesionMemory(lockOnTarget);
            }
        }
        else
        {
            //if (Input.GetMouseButton(ROTATE_BUTTON))
            //{
                rotateCmaeraAngle();
           // }
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
      
    }

    /// <summary>
    /// プレイヤーの所持しているメモリ配列に、サーチした敵から取得したメモリを格納する
    /// </summary>
    /// <param name="target">サーチした敵</param>
    private void SetPossesionMemory(GameObject target)
    {
        //todo:処理の位置調整したい
        //取得したメモリをプレイヤーに設定
        int targetMemory = target.GetComponent<Enemy>().param.Get<int>((int)Enemy.ParamKey.PossesionMemory);
        //空いている配列番号があれば登録
        var p = player.GetComponent<Player>();
        int arrayValue = p.GetMemoryArrayNullValue();
        if (arrayValue != -1)
        {
            //todo:登録配列番号を変更
            p.SetPossesionMemory(targetMemory, arrayValue);
        }
    }
}
