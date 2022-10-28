using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 pos;

    public Vector3  Move;

    public bool is2ndFloor;

    void Start()
    {
        is2ndFloor = false;
    }

    //2�K�֏㏸
    public void MoveUp()
    {
        StartCoroutine("MoveUpStart");
    }

    IEnumerator MoveUpStart()
    {
        while (pos.y < Move.y)
        {
            pos = transform.position;
            transform.Translate(0, 0.2f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        is2ndFloor = true;
    }

    //1�K�։��~
    public void MoveDown()
    {
        StartCoroutine("MoveDownStart");

    }

    IEnumerator MoveDownStart()
    {
        while (pos.y > 0.0f)
        {
            pos = transform.position;
            transform.Translate(0, -0.02f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        is2ndFloor = false;
    }

    //USB�ɋ߂Â��ƕ\��
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MoveUp();
        }
    }

    //USB���痣���Ɣ�\��
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MoveDown();
        }
    }
}
