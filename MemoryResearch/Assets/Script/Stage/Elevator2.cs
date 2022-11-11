using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator2 : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [Header("エレベータスピード")]
    public float Movespeed;

    //一階もしくは二階なのかの識別
    private int NowFloorNo;

    private float FirstFloorPos;

    [SerializeField]
    private float SecondFloorPos;

    //移動中
    bool move;

    enum ElevatorSituation
    {
        Stop,
        Up,
        Down,
    }

    enum PlayerPosInfo
    {
        None,
        InElevator,
        OutElevater,
        UpFloor,
        DownFloor,
    }

    ElevatorSituation situation;
    PlayerPosInfo     playerPosInfo;

    // Start is called before the first frame update
    void Start()
    {
        NowFloorNo = 1;
        FirstFloorPos = transform.localPosition.y;
        playerPosInfo = PlayerPosInfo.None;
        move = false;
    }

    // Update is called once per frame
    void Update()
    {
        SituationUpdate();
        MoveUpdate();
    }

    void SituationUpdate()
    {
        //Debug.Log(FirstFloorPos);
        //プレイヤーが乗ったら上昇・下降
        //指定位置になったら止まる
        //プレイヤーが乗っていないかつ自分より下にいるなら下降する

        if (transform.localPosition.y < SecondFloorPos && playerPosInfo==PlayerPosInfo.InElevator && NowFloorNo==1)
        {
            situation = ElevatorSituation.Up;
        }

        if (transform.localPosition.y >= SecondFloorPos && NowFloorNo == 1 )
        {
            situation = ElevatorSituation.Stop;
        }

        if (transform.localPosition.y > FirstFloorPos&& playerPosInfo == PlayerPosInfo.InElevator && NowFloorNo==2 )
        {
            situation = ElevatorSituation.Down;
        }

        if (transform.localPosition.y <= FirstFloorPos && NowFloorNo == 2 )
        {
            situation = ElevatorSituation.Stop;
           
        }

        if (transform.position.y >= player.transform.localPosition.y && playerPosInfo == PlayerPosInfo.OutElevater && move)
        {
           
            situation = ElevatorSituation.Down;
            NowFloorNo = 2;
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
                transform.Translate(0, Movespeed * Time.deltaTime, 0);
                break;
            case ElevatorSituation.Down:
                transform.Translate(0, -Movespeed * Time.deltaTime, 0);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)//エレベーターの中に入ったら
    {
       if(other.tag=="Player" )
       {
           playerPosInfo=PlayerPosInfo.InElevator;
           move = true;
        
       }
    }
    private void OnTriggerExit(Collider other)//エレベーターから出たら
    {
        if(other.tag=="Player")
        {
            if(NowFloorNo==1)
            {
                NowFloorNo = 2;
            }
            else
            {
                NowFloorNo = 1;
            }

            playerPosInfo = PlayerPosInfo.OutElevater;
           
        }
    }
}

