using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pless : MonoBehaviour
{
 

    [Header("�u���X�~���X�s�[�h")]
    public float MoveDownspeed;

    [Header("�u���X�}��X�s�[�h")]
    public float MoveUPspeed;

    //��K�������͓�K�Ȃ̂��̎���
    private int NowFloorNo;

    private float FirstFloorPos;

    [SerializeField]
    private float SecondFloorPos;

    //�ړ���
    bool move;

    //�ҋ@����
    [SerializeField]
    private float StopTime;

    //�����n�߂鎞��
    [SerializeField]
    public float MoveTime;

    //�G���x�[�^�̍s���X�e�[�^�X
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

        //�w��ʒu�ɂȂ�����~�܂�
        //�^�C����0�ɂȂ����瓮���o��

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
