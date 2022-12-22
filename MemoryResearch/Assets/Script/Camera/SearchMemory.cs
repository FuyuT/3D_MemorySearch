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


    //todo 後でテキストマネージャに移す
    //テキスト関連
    [Header("チャプター完了テキスト")]
    [SerializeField]
    GameObject CompleteText;
    public float timer;

    [Header("オプションスライダーX")]
    public Slider sliderX;
    [Header("オプションスライダーY")]
    public Slider sliderY;
    [Header("サーチスライダー")]
    public Slider SearcSlider;
    public float SearcCompleteSpeed;

    GameObject mainCamera;

    //オプションの情報を取得
    [SerializeField] OptionManager optionManager;

    //Lockonのスクリプト
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
            //左クリックしたときにメモリ（アクション）を登録

            if (SearcSlider.value == 1)
            {
                if (Input.GetMouseButton(0))
                {
                    SetPossesionMemory(lockOnTarget);

                    //テキスト関連
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
        //Y軸だけ反転
        //if (aimx.XOnOff && !aimy.YOnOff)
        //{
        //    Vector3 angle = new Vector3(
        //        Input.GetAxis("Mouse X") * sliderX.value,

        //        Input.GetAxis("Mouse Y") * sliderY.value,
        //        0
        //    );
        //    transform.eulerAngles += new Vector3(angle.y, angle.x);
        //}
        //X軸だけ反転
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
        //X,Y軸反転
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
    /// プレイヤーの所持しているメモリ配列に、サーチした敵から取得したメモリを格納する
    /// </summary>
    /// <param name="target">サーチした敵</param>
    private void SetPossesionMemory(GameObject target)
    {
        //todo:処理の位置調整したい 取得したメモリをプレイヤーに設定

        //int targetMemory = target.GetComponent<Enemy>().param.Get<int>((int)Enemy.ParamKey.PossesionMemory);
        //空いている配列番号があれば登録
        //var p = player.GetComponent<Player>();
        //int arrayValue = p.GetMemoryArrayNullValue(targetMemory);
        //if (arrayValue != -1)
        //{
        //    //todo:登録配列番号を変更
        //    p.SetPossesionMemory(targetMemory, arrayValue);
        //}
    }

    void OnEnable()
    {
        transform.rotation = player.transform.rotation;
    }
}
