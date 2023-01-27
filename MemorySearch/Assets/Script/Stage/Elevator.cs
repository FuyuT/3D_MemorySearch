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

    //2äKÇ÷è„è∏
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

    //1äKÇ÷â∫ç~
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

    //USBÇ…ãﬂÇ√Ç≠Ç∆ï\é¶
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MoveUp();
        }
    }

    //USBÇ©ÇÁó£ÇÍÇÈÇ∆îÒï\é¶
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MoveDown();
        }
    }
}
