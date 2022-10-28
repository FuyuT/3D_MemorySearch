using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Enemy>;

public class StateEnemyMove : State
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
        float x = Mathf.Abs(Owner.transform.position.x) - Mathf.Abs(Owner.PlayerTransform.position.x);
        float z = Mathf.Abs(Owner.transform.position.z) - Mathf.Abs(Owner.PlayerTransform.position.z);
        float targetDistance = Mathf.Abs(x) + Mathf.Abs(z);

        if (targetDistance <= Owner.SearchDistance)
        {
            Debug.Log("�v���C���[�Ƃ̋������߂�");
            moveState = (int)MoveState.Chase;
        }
        else
        {
            //Debug.Log("�^�[�Q�b�g�����F" + targetDistance);
            Debug.Log("�v���C���[�Ƃ̋���������");
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
                Vector3 moveAdd = Owner.PlayerTransform.position - Owner.transform.position;
                Owner.moveVec += Vector3.Normalize(moveAdd) * Owner.MoveSpeed;

                Owner.moveVec.y = 0;

                break;
            case (int)MoveState.Search:
                //���G
                Owner.moveVec = Vector3.Normalize(searchVec) * (Owner.MoveSpeed * 0.7f);
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

                //�p���`
                //if ((bool)Owner.parameter.Get((int)Enemy.ParamKey.IsAttackPossibble))
                //{
                //    stateMachine.Dispatch((int)Enemy.Event.Attack_Punch);
                //    return;

                //}

                //�^�b�N��
                if (Owner.nowDushDelayTime < 0)
                {
                    stateMachine.Dispatch((int)Enemy.Event.Attack_Tackle);
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
