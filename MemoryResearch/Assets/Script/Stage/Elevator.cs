//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Elevator : MonoBehaviour
//{
//    // Start is called before the first frame update

//    [Header("�G���x�[�^�|�W�V����")]
//    private Vector3 pos;

//    //[Header("�G���x�[�^�ړ��͈�")]
//    public Vector3 Move;

//    [Header("�G���x�[�^�X�s�[�h")]
//    public float Movespeed;

//    //��K�������͓�K�Ȃ̂��̎���
//    public bool is2ndFloor;

//    void Start()
//    {
//        is2ndFloor = false;
//    }

//    //2�K�֏㏸
//    public void MoveUp()
//    {
//        StartCoroutine("MoveUpStart");
//    }

//    //public void MoveUpStart()
//    //{
//    //    if (pos.y < Move.y && !is2ndFloor)
//    //    {
//    //        Debug.Log("�オ��");
//    //        pos = new Vector3(0, Move.y, 0);
//    //        transform.Translate(0, Movespeed, 0);

//    //    }
//    //    is2ndFloor = true;
//    //}


//    IEnumerator MoveUpStart()
//    {
//        //while (pos.y < Move.y)
//        //{
//        //    //Debug.Log("�オ��");
//        //    pos = new Vector3(0, Move.y, 0);
//        //    transform.Translate(0, Movespeed, 0);
//        //    yield return new WaitForSeconds(0.01f);
//        //}
//        //is2ndFloor = true;

//        while (transform.position.y < Move.y)
//        {
//            //Debug.Log("�オ��");
//            // pos = new Vector3(0, Move.y, 0);
//            transform.Translate(0, Movespeed, 0);
//            yield return new WaitForSeconds(0.01f);
//        }
//        is2ndFloor = true;
//    }

//    //1�K�։��~
//    public void MoveDown()
//    {
//        StartCoroutine("MoveDownStart");

//    }

//    //IEnumerator MoveDownStart()
//    //{
//    //    while (pos.y > 0.0f)
//    //    {
//    //        pos = transform.position;
//    //        transform.Translate(0, -Movespeed, 0);
//    //        yield return new WaitForSeconds(0.01f);
//    //    }
//    //    is2ndFloor = false;
//    //}

//    //�G���x�[�^�ɏ��Əオ��
//    private void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.tag == "Player")
//        {
//            MoveUp();
//        }
//    }

//    ////�G���x�[�^���痣���Ɖ�����
//    //private void OnCollisionExit(Collision collision)
//    //{
//    //    if (collision.gameObject.tag == "Player")
//    //    {
//    //        MoveDown();
//    //    }
//    //}
//}
