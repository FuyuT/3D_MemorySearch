using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCameraScript : MonoBehaviour
{
    public GameObject target; // an object to follow
    public Vector3 offset; // offset form the target object

    [SerializeField] private float distance = 4.0f; // distance from following object
    [SerializeField] private float YAngle = 45.0f; // angle with y-axis
    [SerializeField] private float XAngle = 45.0f; // angle with x-axis

    [SerializeField] private float minDistance = 1.0f;
    [SerializeField] private float maxDistance = 7.0f;
    [SerializeField] private float minPolarAngle = 5.0f;
    [SerializeField] private float maxPolarAngle = 75.0f;
    [SerializeField] private float mouseXSensitivity = 5.0f;
    [SerializeField] private float mouseYSensitivity = 5.0f;
    [SerializeField] private float scrollSensitivity = 5.0f;

    [Header("障害物のレイヤー")]
    [SerializeField]
    private LayerMask WallLayer;

    [SerializeField]
    private LayerMask CeilingLayer;

    void Start()
    {
    }

    void Update()
    {
    }

    void FixedUpdate()
    {

        Cursor.visible = false;

        RaycastHit hit;
        //マウスで視点を変更
        updateAngle(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        var lookAtPos = target.transform.position + offset;
        updatePosition(lookAtPos);
        transform.LookAt(lookAtPos);
        //transform.RotateAround(target.transform.position, Vector3.up, lookAtPos);
        if (Physics.Linecast(target.transform.position, transform.position, out hit, WallLayer))
        {
            transform.position = hit.point+new Vector3(0,10,0);
            transform.LookAt(target.transform);
        }

        if (Physics.Linecast(target.transform.position, transform.position, out hit, CeilingLayer))
        {
            transform.position = hit.point - new Vector3(0, 5, 0);
            transform.LookAt(target.transform);
        }

    }

    void updateAngle(float x, float y)
    {
        x = XAngle - x * mouseXSensitivity;
        XAngle = Mathf.Repeat(x, 360);

        y =YAngle + y * mouseYSensitivity;
        YAngle = Mathf.Clamp(y, minPolarAngle, maxPolarAngle);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.LookAt(target.transform.forward);
        }
    }

    void updateDistance(float scroll)
    {
        scroll = distance - scroll * scrollSensitivity;
        distance = Mathf.Clamp(scroll, minDistance, maxDistance);
    }

    void updatePosition(Vector3 lookAtPos)
    {
        var da = XAngle * Mathf.Deg2Rad;
        var dp = YAngle * Mathf.Deg2Rad;
        transform.position = new Vector3(
            lookAtPos.x + distance * Mathf.Sin(dp) * Mathf.Cos(da),
            lookAtPos.y + distance * Mathf.Cos(dp),
            lookAtPos.z + distance * Mathf.Sin(dp) * Mathf.Sin(da));
    }
}
