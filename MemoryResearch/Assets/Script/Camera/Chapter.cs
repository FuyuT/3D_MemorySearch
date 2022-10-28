using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter : MonoBehaviour
{
    private Image img;

    //ChangeCameraのスクリプト
    ChangeCamera Script;

    [SerializeField]
    GameObject CameraControll;

    [SerializeField]
    GameObject FPSVisibility;

    // カメラオブジェクトを格納する変数
    public Camera mainCamera;
    
    // カメラの回転速度を格納する変数
    public Vector2 rotationSpeed;
    
    // マウス移動方向とカメラ回転方向を反転する判定フラグ
    public bool reverse;
    
    // マウス座標を格納する変数
    private Vector2 lastMousePosition;

    // カメラの角度を格納する変数（初期値に0,0を代入）
    private Vector2 newAngle = new Vector2(0, 0);


    void Start()
    {
        img = GetComponent<Image>();
        img.color = Color.clear;

        Script = CameraControll.GetComponent<ChangeCamera>();

        FPSVisibility.SetActive(false);

       
    }

   public void Update()
    {
        if (Script.ChangFlg == true)
        {
            FPSVisibility.SetActive(true);
            //return入力でシャッターをきる
            //if (Input.GetKeyDown("return") || Input.GetMouseButtonDown(1))
            //{
            //    img.color = new Color(1, 1, 1, 1);
            //}
            //else
            //{
            //    //元の色に戻す
            //    img.color = Color.Lerp(img.color, Color.clear, Time.deltaTime);
            //}


            //左クリックした時
            if (Input.GetMouseButtonDown(0))
            {
                //カメラの角度を変数newAngleに格納
                newAngle = mainCamera.transform.localEulerAngles;

                newAngle = FPSVisibility.transform.localEulerAngles;

                //マウス座標を変数lastMousePositionに格納
                lastMousePosition = Input.mousePosition;
            }
            //左ドラッグしている間
            else if (Input.GetMouseButton(0))
            {
                //カメラ回転方向の判定フラグがtrueの場合
                if (!reverse)
                {
                    // Y軸の回転：マウスドラッグ方向に視点回転
                    // マウスの水平移動値に変数rotationSpeedを掛ける
                    //（クリック時の座標とマウス座標の現在値の差分値）
                    newAngle.y -= (lastMousePosition.x - Input.mousePosition.x) * rotationSpeed.y;
                   
                    // X軸の回転：マウスドラッグ方向に視点回転
                    // マウスの垂直移動値に変数rotationSpeedを掛ける
                    //（クリック時の座標とマウス座標の現在値の差分値）
                    newAngle.x -= (Input.mousePosition.y - lastMousePosition.y) * rotationSpeed.x;
                   
                    //newAngleの角度をカメラ角度に格納
                    mainCamera.transform.localEulerAngles = newAngle;

                    FPSVisibility.transform.localEulerAngles = newAngle;

                    //マウス座標を変数lastMousePositionに格納
                    lastMousePosition = Input.mousePosition;
                }
                //カメラ回転方向の判定フラグがreverseの場合
                else if (reverse)
                {
                    //Y軸の回転：マウスドラッグと逆方向に視点回転
                    newAngle.y -= (Input.mousePosition.x - lastMousePosition.x) * rotationSpeed.y;
                    
                    //X軸の回転：マウスドラッグと逆方向に視点回転
                    newAngle.x -= (lastMousePosition.y - Input.mousePosition.y) * rotationSpeed.x;
                   
                    //newAngleの角度をカメラ角度に格納
                    mainCamera.transform.localEulerAngles = newAngle;

                    FPSVisibility.transform.localEulerAngles = newAngle;

                    //マウス座標を変数lastMousePositionに格納
                    lastMousePosition = Input.mousePosition;
                }
            }
           
        }
        else
        {
            newAngle = new Vector2(0, 0);

            mainCamera.transform.localEulerAngles = newAngle;

            FPSVisibility.transform.localEulerAngles = newAngle;
            FPSVisibility.SetActive(false);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                //        float search_radius = 10f;

                //        var hits = Physics.SphereCastAll(
                //            player.transform.position,
                //            search_radius,
                //            player.transform.forward,
                //            0.01f,
                //            LayerMask.GetMask("LockonTarget")
                //        ).Select(h => h.transform.gameObject).ToList();

                //        hits = FilterTargetObject(hits);

                //        if (0 < hits.Count())
                //        {
                //            float min_target_distance = float.MaxValue;
                //            GameObject target = null;

                //            foreach (var hit in hits)
                //            {
                //                Vector3 targetScreenPoint = Camera.main.WorldToViewportPoint(hit.transform.position);
                //                float target_distance = Vector2.Distance(
                //                    new Vector2(0.5f, 0.5f),
                //                    new Vector2(targetScreenPoint.x, targetScreenPoint.y)
                //                );
                //               

                //                if (target_distance < min_target_distance)
                //                {
                //                    min_target_distance = target_distance;
                //                    target = hit.transform.gameObject;
                //                }
                //            }

                //            return target;
                //        }
                //        else
                //        {
                //            return null;
                //        }
                //    }

                //    protected List<GameObject> FilterTargetObject(List<GameObject> hits)
                //    {
                //        return hits
                //            .Where(h => {
                //                Vector3 screenPoint = Camera.main.WorldToViewportPoint(h.transform.position);
                //                return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
                //            })
                //            .Where(h => h.tag == "Enemy")
                //            .ToList();
                //    }
                //}
            }
        }
    }
}
