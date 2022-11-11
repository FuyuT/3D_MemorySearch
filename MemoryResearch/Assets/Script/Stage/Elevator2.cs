using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator2 : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [Header("�G���x�[�^�X�s�[�h")]
    public float Movespeed;

    //��K�������͓�K�Ȃ̂��̎���
    private int NowFloorNo;

    private float FirstFloorPos;

    [SerializeField]
    private float SecondFloorPos;

    //�ړ���
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
        //�v���C���[���������㏸�E���~
        //�w��ʒu�ɂȂ�����~�܂�
        //�v���C���[������Ă��Ȃ���������艺�ɂ���Ȃ牺�~����

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

    private void OnTriggerEnter(Collider other)//�G���x�[�^�[�̒��ɓ�������
    {
       if(other.tag=="Player" )
       {
           playerPosInfo=PlayerPosInfo.InElevator;
           move = true;
        
       }
    }
    private void OnTriggerExit(Collider other)//�G���x�[�^�[����o����
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

