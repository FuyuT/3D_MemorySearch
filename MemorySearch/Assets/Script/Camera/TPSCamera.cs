using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public GameObject target; // an object to follow
    public Vector3 offset; // offset form the target object

    [SerializeField] private float distance = 4.0f; // distance from following object
    [SerializeField] private float YAngle = 0.0f; // angle with y-axis
    [SerializeField] private float XAngle = 0.0f; // angle with x-axis

    //マウスでの操作関連////////////////////////////////////
    [SerializeField] private float minPolarAngle = 5.0f;
    [SerializeField] private float maxPolarAngle = 75.0f;
    //////////////////////////////////////////////////////////

    //キー入力関連///////////////////////////////////////////
    [SerializeField] public KeyCode RightKeyCord;
    [SerializeField] public KeyCode LeftKeyCord;
    float angle;
    public float KeyRotationSpeed;
    /////////////////////////////////////////////////////////

    //レイ関連///////////////////////////////////////////////
    [Header("障害物のレイヤー")]
    [SerializeField]
    private LayerMask WallLayer;

    [SerializeField]
    private LayerMask CeilingLayer;

    RaycastHit hit;
    //////////////////////////////////////////////////////////
    public float BaseRotationSpeed;

    public float a;

    void FixedUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    XAngle = Mathf.Repeat(Mathf.Abs(target.transform.rotation.y + 270), 360);
        //    UpdatePosition(target.transform.position + offset);
        //    transform.LookAt(target.transform.position);

        //    return;
        //}

        Cursor.visible = false;
        UpdateAngle();
        //Debug.Log(XAngle);

    }

    void UpdateAngle()
    {
        //入力で視点を変更
        AngleChangeForInput();

        var lookAtPos = target.transform.position + offset;
        UpdatePosition(lookAtPos);
        if (!AdjustAngle())
        {
            transform.LookAt(lookAtPos);
        }
    }

    void AngleChangeForInput()
    {
        Vector2 input = Vector2.zero;
        //キー入力をinputに反映
        input.x = Input.GetKey(LeftKeyCord) ? -KeyRotationSpeed : input.x;
        input.x = Input.GetKey(RightKeyCord) ? KeyRotationSpeed : input.x;

        //キー入力が無ければ、マウスの入力を入れる
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
    /// アングルを調整する
    /// </summary>
    /// <returns>カメラの前にステージがあったならtrue、なければfalseを返す</returns>
    bool AdjustAngle()
    {
        bool isHitStage = false;

        //ステージの壁、天井に当たっていたら
        if (Physics.Linecast(target.transform.position, transform.position, out hit, WallLayer))
        {
            transform.position = hit.point + new Vector3(0, 10, 0);
            isHitStage = true;
        }
        if (Physics.Linecast(target.transform.position, transform.position, out hit, CeilingLayer))
        {
            transform.position = hit.point - new Vector3(0, 5, 0);
            isHitStage = true;
        }

        //注視点を変更
        if (isHitStage)
        {
            transform.LookAt(target.transform);
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
}
