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
        //索敵範囲内にいるか確認 範囲内なら追従(chase)、それ以外なら索敵(search)
        float x = Mathf.Abs(Owner.transform.position.x) - Mathf.Abs(Owner.TargetTransform.position.x);
        float z = Mathf.Abs(Owner.transform.position.z) - Mathf.Abs(Owner.TargetTransform.position.z);
        float targetDistance = Mathf.Abs(x) + Mathf.Abs(z);

        if (targetDistance <= Owner.SearchDistance)
        {
            //Debug.Log("プレイヤーとの距離が近い");
            moveState = (int)MoveState.Chase;
        }
        else
        {
            //Debug.Log("ターゲット距離：" + targetDistance);
           // Debug.Log("プレイヤーとの距離が遠い");
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
                //目標に向かって追従移動
                Vector3 moveAdd = Owner.TargetTransform.position - Owner.transform.position;
                moveAdd += Vector3.Normalize(moveAdd) * Owner.MoveSpeed;

                moveAdd.y = 0;
                Owner.objectParam.AddMoveVec(moveAdd);
                break;
            case (int)MoveState.Search:
                //索敵
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

                //ジャンプ
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
