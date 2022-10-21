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
        //Debug.Log(player.transform.position+("プレイヤー"));
        //Debug.Log(mainCamera.transform.position + ("カメラ"));
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

            //左クリックしたら対象のメモリを登録
            if (Input.GetMouseButton(0))
            {
                SetPossesionMemory(lockOnTarget);
            }
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
      
    }

    /// <summary>
    /// プレイヤーの所持しているメモリ配列に、サーチした敵から取得したメモリを格納する
    /// </summary>
    /// <param name="target">サーチした敵</param>
    private void SetPossesionMemory(GameObject target)
    {
        //todo:処理の位置調整したい
        //同じメモリがあるか確認（ある場合配列番号を返す）
        var p = player.GetComponent<Player>();
        //同じメモリがあれば、同じ配列番号にメモリに応じた強化メモリを設定


        //敵からメモリを取得
        int targetMemory = target.GetComponent<Enemy>().param.Get<int>((int)Enemy.ParamKey.PossesionMemory);
        //設定できる配列番号を取得
        int arrayValue = p.GetMemoryArrayNullValue(targetMemory);

        //配列に設定可能な場合
        if (arrayValue != -1)
        {
            //メモリ配列に登録
            p.SetPossesionMemory(targetMemory, arrayValue);
        }
        else
        {
            //todo:メモリ配列が空いていない時の処理
        }
    }
}
