//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Elevator : MonoBehaviour
//{
//    // Start is called before the first frame update

//    [Header("エレベータポジション")]
//    private Vector3 pos;

//    //[Header("エレベータ移動範囲")]
//    public Vector3 Move;

//    [Header("エレベータスピード")]
//    public float Movespeed;

//    //一階もしくは二階なのかの識別
//    public bool is2ndFloor;

//    void Start()
//    {
//        is2ndFloor = false;
//    }

//    //2階へ上昇
//    public void MoveUp()
//    {
//        StartCoroutine("MoveUpStart");
//    }

//    //public void MoveUpStart()
//    //{
//    //    if (pos.y < Move.y && !is2ndFloor)
//    //    {
//    //        Debug.Log("上がる");
//    //        pos = new Vector3(0, Move.y, 0);
//    //        transform.Translate(0, Movespeed, 0);

//    //    }
//    //    is2ndFloor = true;
//    //}


//    IEnumerator MoveUpStart()
//    {
//        //while (pos.y < Move.y)
//        //{
//        //    //Debug.Log("上がる");
//        //    pos = new Vector3(0, Move.y, 0);
//        //    transform.Translate(0, Movespeed, 0);
//        //    yield return new WaitForSeconds(0.01f);
//        //}
//        //is2ndFloor = true;

//        while (transform.position.y < Move.y)
//        {
//            //Debug.Log("上がる");
//            // pos = new Vector3(0, Move.y, 0);
//            transform.Translate(0, Movespeed, 0);
//            yield return new WaitForSeconds(0.01f);
//        }
//        is2ndFloor = true;
//    }

//    //1階へ下降
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

//    //エレベータに乗ると上がる
//    private void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.tag == "Player")
//        {
//            MoveUp();
//        }
//    }

//    ////エレベータから離れると下がる
//    //private void OnCollisionExit(Collision collision)
//    //{
//    //    if (collision.gameObject.tag == "Player")
//    //    {
//    //        MoveDown();
//    //    }
//    //}
//}
