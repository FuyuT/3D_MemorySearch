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
    [Header("サウンドマネージャー")]
    [SerializeField]
    SoundManager soundManager;
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

    bool Successcomplete;
    bool ScanStart;

    public enum SEType
    {
        
    }

    void Start()
    {
        //カーソル非表示
        Cursor.lockState = CursorLockMode.Locked;

        player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = player.transform.rotation;

        getMemoryUI.Stop();

        InitSearchSlider();

        isScan = false;
        ScanStart = false;
        Successcomplete = false;
    }

    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 7, 0);

        RotateCmaeraAngle(viewAngle);

        Scan();
    }

    void RotateCmaeraAngle(float limit)
    {

        float maxLimit = limit, minLimit = 360 - maxLimit;
        //X軸回転
        var localAngle = transform.localEulerAngles;
        localAngle.x -= Input.GetAxis("Mouse Y") * sliderY.value;
        if (localAngle.x > maxLimit && localAngle.x < 180)
            localAngle.x = maxLimit;
        if (localAngle.x < minLimit && localAngle.x > 180)
            localAngle.x = minLimit;
        transform.localEulerAngles = localAngle;
        //Y軸回転
        var angle = transform.eulerAngles;
        angle.y += Input.GetAxis("Mouse X") * sliderX.value;
        transform.eulerAngles = angle;

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

    //Activeになった時
    void OnEnable()
    {
        //プレイヤーの角度に合わせる
        transform.rotation = player.transform.rotation;
    }


    void Scan()
    {
        //メモリ取得時のUIを再生
        if (lockon.GetTarget())
        {

        }

        if (Input.GetMouseButtonDown(1) && lockon.GetTarget())
        {
            isScan = true;
        }

        if (!isScan) return;

        if (!lockon.GetTarget())
        {
            MissScan();
            Successcomplete = false;
            ScanStart = false;
        }

        ScanUpdate();
    }

    void ScanUpdate()
    {

        //チャージSEを流す
        if (SearchSlider.value <1)
        {
            if (!ScanStart)
            {
                soundManager.PlaySe(Chargeclip);
            }
            SearchSlider.value += SearcCompleteSpeed;
            ScanStart = true;


            if (Input.GetMouseButtonUp(1) && !Successcomplete)
            {
                MissScan();
                return;
            }
        }

        if (!lockon.GetTarget())
        {
            MissScan();
            ScanStart = false;
        }

        if (SearchSlider.value == 1 && !Successcomplete)
        {
            //スキャン成功音を流す
            soundManager.StopSe(Chargeclip);
            soundManager.PlaySe(Successclip);
            SetPossesionMemory(lockon.GetTarget());
            getMemoryUI.Play();
            Successcomplete = true;
        }

    }

    void MissScan()
    {
        isScan = false;
        soundManager.StopSe(Chargeclip);
        soundManager.PlaySe(Missclip);
        SearchSlider.value = 0;
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

