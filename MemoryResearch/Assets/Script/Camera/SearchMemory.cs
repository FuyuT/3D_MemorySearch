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

    //テキスト関連
    [Header("チャプター完了UI")]
    [SerializeField]
    GetMemoryUI getMemoryUI;

    //オプション関連/////////////////////////

    [Header("オプションスライダーX")]
    [SerializeField] Slider sliderX;
    [Header("オプションスライダーY")]
    [SerializeField] Slider sliderY;
    //オプションの情報を取得
    [SerializeField] OptionManager optionManager;
    ///////////////////////////////////////////

    //SE関連/////////////////////////
    [SerializeField] AudioClip Successclip;

    [SerializeField] AudioClip Missclip;

    [SerializeField] AudioClip Chargeclip;

    
    ///////////////////////////////////////////

    [Header("サーチスライダー")]
    [SerializeField] Slider SearchSlider;
    [SerializeField] float SearcCompleteSpeed;

    //Lockonのスクリプト
    [SerializeField]
    Lockon lockon;

    MemoryType scanMemory;

    void Start()
    {
        //カーソルロック
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
        //X軸回転
        var localAngle = transform.localEulerAngles;
        localAngle.x -= Input.GetAxis("Mouse Y") * option.sensitivity.y;
        if (localAngle.x > maxLimit && localAngle.x < 180)
            localAngle.x = maxLimit;
        if (localAngle.x < minLimit && localAngle.x > 180)
            localAngle.x = minLimit;
        transform.localEulerAngles = localAngle;
        //Y軸回転
        var angle = transform.eulerAngles;
        angle.y += Input.GetAxis("Mouse X") * option.sensitivity.x;
        transform.eulerAngles = angle;
    }

    //Activeになった時
    void OnEnable()
    {
        //プレイヤーの角度に合わせる
        transform.rotation = player.transform.rotation;
    }

    void Scan()
    {
        //メモリ取得時のUIを再生

        if (Input.GetMouseButtonDown(1) && lockon.GetTarget())
        {
            isScan = true;
            scanMemory = lockon.GetTarget().GetComponent<EnemyBase>().GetMemory();
            SoundManager.instance.PlaySe(Chargeclip,transform.position);
        }

        if (!isScan) return;

        //ターゲットがいなくなった時
        if (!lockon.GetTarget())
        {
            MissScan();
        }

        ScanUpdate();
    }

    void ScanUpdate()
    {
        //ボタンを離したらサーチ失敗、処理を終了
        if (Input.GetMouseButtonUp(1))
        {
            MissScan();
            return;
        }

        //サーチゲージを貯める
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
        //スキャン成功音を流す
        SoundManager.instance.StopSe(Chargeclip);
        SoundManager.instance.PlaySe(Successclip, transform.position);

        //取得したメモリをプレイヤーデータに登録
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

