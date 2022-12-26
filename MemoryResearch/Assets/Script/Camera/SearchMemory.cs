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


    //SE関連/////////////////////////
    [Header("サウンドマネージャー")]
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

    //オプションの情報を取得
    [SerializeField] OptionManager optionManager;

    //Lockonのスクリプト
    [SerializeField]
    Lockon lockon;

    GameObject lockOnTarget;

    void Start()
    {
        //カーソル非表示
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
        //X軸回転
        var localAngle = transform.localEulerAngles;
        localAngle.x -= _inputY*sliderY.value;
        if (localAngle.x > maxLimit && localAngle.x < 180)
            localAngle.x = maxLimit;
        if (localAngle.x < minLimit && localAngle.x > 180)
            localAngle.x = minLimit;
        transform.localEulerAngles = localAngle;
        //Y軸回転
        var angle = transform.eulerAngles;
        angle.y += _inputX*sliderX.value;
        transform.eulerAngles = angle;
      
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
        //左クリックしたときにメモリ（アクション）を登録
        if (Input.GetMouseButton(1))
        {
            if (lockOnTarget)
            {
                //チャージSEを流す
                ChargeSE();
                if (SearcSlider.value <= 1)
                {
                    ChargeisPlaying = false;
                    SearcSlider.value += SearcCompleteSpeed;
                    //スキャン成功
                    if (SearcSlider.value >= 1)
                    {
                        //スキャン成功音を流す
                        SuccessSE();
                        SetPossesionMemory(lockOnTarget);

                        //テキスト関連
                        CompleteText.SetActive(true);
                        timer = 5f;
                        SearcSlider.value = 0;

                    }

                    //スキャン失敗

                }
               
            }
            else
            {
                Debug.Log("外れた");
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
