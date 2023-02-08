using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pless : MonoBehaviour
{
 

    [Header("ブレス降下スピード")]
    public float MoveDownspeed;

    [Header("ブレス急上スピード")]
    public float MoveUPspeed;

    //一階もしくは二階なのかの識別
    private int NowFloorNo;

    private float FirstFloorPos;

    [SerializeField]
    private float SecondFloorPos;

    //移動中
    bool move;

    //待機時間
    [SerializeField]
    private float StopTime;

    //動き始める時間
    [SerializeField]
    public float MoveTime;

    //エレベータの行動ステータス
    enum ElevatorSituation
    {
        Stop,
        Up,
        Down,
    }

    ElevatorSituation situation;

    // Start is called before the first frame update
    void Start()
    {
        NowFloorNo = 1;
        FirstFloorPos = transform.localPosition.y;
        move = false;
        StopTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (StopTime >= 0 && NowFloorNo == 2)
        {
            StopTime -= Time.deltaTime;
        }
        else if (StopTime >= 0 && NowFloorNo == 1)
        {
            StopTime -= Time.deltaTime;
        }
        SituationUpdate();
        MoveUpdate();

    }

    void SituationUpdate()
    {

        //指定位置になったら止まる
        //タイムが0になったら動き出す

        if (transform.localPosition.y < SecondFloorPos && NowFloorNo == 1 && StopTime <= 0)
        {
            situation = ElevatorSituation.Up;
            StopTime = MoveTime;
        }

        if (transform.localPosition.y >= SecondFloorPos && NowFloorNo == 1 && StopTime >= 0)
        {
            situation = ElevatorSituation.Stop;
            NowFloorNo = 2;
        }

        if (transform.localPosition.y > FirstFloorPos && NowFloorNo == 2 && StopTime <= 0)
        {
            situation = ElevatorSituation.Down;
            StopTime = MoveTime;
        }

        if (transform.localPosition.y <= FirstFloorPos && NowFloorNo == 2 && StopTime >= 0)
        {
            situation = ElevatorSituation.Stop;
            NowFloorNo = 1;
        }

    }

    void MoveUpdate()
    {
        switch (situation)
        {
            case ElevatorSituation.Stop:
                transform.Translate(0, 0, 0);
                break;
            case ElevatorSituation.Up:
                transform.Translate(0, MoveUPspeed * Time.deltaTime, 0);
                break;
            case ElevatorSituation.Down:
                transform.Translate(0, -MoveDownspeed * Time.deltaTime, 0);
                break;
        }
    }
}
