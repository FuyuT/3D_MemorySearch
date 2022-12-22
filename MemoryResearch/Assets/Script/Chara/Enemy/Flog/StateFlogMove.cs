using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<EnemyFlog>;

public class StateFlogMove : State
{
    float searchTime;

    enum MoveState
    {
        None,
        Search,
        Chase,
    }

    int moveState;
    Vector3 searchVec;

    protected override void OnEnter(State prevState)
    {
        moveState = (int)MoveState.None;

        searchVec = Vector3.zero;
    }

    protected override void OnUpdate()
    {
        //���G�͈͓��ɂ��邩�m�F �͈͓��Ȃ�Ǐ](chase)�A����ȊO�Ȃ���G(search)
        float x = Mathf.Abs(Owner.transform.position.x) - Mathf.Abs(Owner.TargetTransform.position.x);
        float z = Mathf.Abs(Owner.transform.position.z) - Mathf.Abs(Owner.TargetTransform.position.z);
        float targetDistance = Mathf.Abs(x) + Mathf.Abs(z);

        if (targetDistance <= Owner.SearchDistance)
        {
            //Debug.Log("�v���C���[�Ƃ̋������߂�");
            moveState = (int)MoveState.Chase;
        }
        else
        {
            //Debug.Log("�^�[�Q�b�g�����F" + targetDistance);
           // Debug.Log("�v���C���[�Ƃ̋���������");
            if (searchTime < 0)
            {
                searchVec.x = Random.Range(-10, 11);
                searchVec.z = Random.Range(-10, 11);
                searchVec *= 0.1f;
                moveState = (int)MoveState.Search;
                searchTime = 3;
            }
            else
            {
                searchTime -= Time.deltaTime;
            }
        }

        switch (moveState)
        {
            case (int)MoveState.Chase:
                //�ڕW�Ɍ������ĒǏ]�ړ�
                Vector3 moveAdd = Owner.TargetTransform.position - Owner.transform.position;
                moveAdd += Vector3.Normalize(moveAdd) * Owner.MoveSpeed;

                moveAdd.y = 0;
                Owner.objectParam.AddMoveVec(moveAdd);
                break;
            case (int)MoveState.Search:
                //���G
                Owner.objectParam.AddMoveVec(Vector3.Normalize(searchVec) * (Owner.MoveSpeed * 0.7f));
                break;
        }

        SelectNextState();
    }

    protected override void SelectNextState()
    {

        switch (moveState)
        {
            case (int)MoveState.Chase:

                //�W�����v
                if (Owner.nowJumpDelayTime < 0)
                {
                    stateMachine.Dispatch((int)Enemy.Event.Jump);
                    return;
                }


                break;
            case (int)MoveState.Search:
                break;
        }
    }

    protected override void OnExit(State prevState)
    {
        moveState = (int)MoveState.None;
    }

}
