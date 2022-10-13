using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveObject : MonoBehaviour
{
    [SerializeField]
    GameObject ControlScript;

    //ControlCamera�̃X�N���v�g���擾
    ControlCamera Script;

    //�I�u�W�F�N�g�𓮂����Ƃ��̑���
    public float MoveObjectSpeed;

    const float Move_Max = 4.5f;

    Vector3 currenPos ,previousPos;

    public float Sensitivity = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Script = ControlScript.GetComponent<ControlCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Script.MoveObjectSwitch == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                previousPos = Input.mousePosition;
            }
            if (Input.GetMouseButton(1))
            {
                currenPos = Input.mousePosition;
                float diffDistance = (currenPos.x - previousPos.x) / Screen.width * MoveObjectSpeed;
                diffDistance *= Sensitivity;

                float newX = Mathf.Clamp(transform.localPosition.x + diffDistance,-Move_Max,Move_Max);
                this.gameObject.transform.Translate(newX, 0, 0);


                previousPos = currenPos;
            }
        }
    }
}
