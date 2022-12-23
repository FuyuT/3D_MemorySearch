using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<EnemyCow>;

public class EnemyCow : CharaBase
{
    ///***************************
    /// private

    float searchTime;

    StateMachine<EnemyCow> stateMachine;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        CharaBaseInit();
    }

    private void Update()
    {
        //索敵範囲内にいるか確認 範囲内なら追従(chase)、それ以外なら索敵(search)
        float x = Mathf.Abs(transform.position.x) - Mathf.Abs(TargetTransform.position.x);
        float z = Mathf.Abs(transform.position.z) - Mathf.Abs(TargetTransform.position.z);
        float targetDistance = Mathf.Abs(x) + Mathf.Abs(z);

        Vector3 moveVec = Vector3.zero;
        if (targetDistance <= SearchDistance)
        {
            moveVec  = Vector3.Normalize(TargetTransform.position - transform.position);
            moveVec *= MoveSpeed;
        }
        else
        {
            if (searchTime < 0)
            {
                moveVec.x = Random.Range(-10, 11);
                moveVec.z = Random.Range(-10, 11);
                moveVec *= 0.1f;
                searchTime = 3;
            }
            else
            {
                searchTime -= Time.deltaTime;
            }
        }
        objectParam.AddMoveVec(moveVec);

        RotateUpdate();
        PositionUpdate();
    }

    /// <summary>
    /// 角度の更新
    /// </summary>
    void RotateUpdate()
    {
        RotateUpdateToMoveVec();
    }

    /// <summary>
    /// 位置の更新
    /// </summary>
    void PositionUpdate()
    {
        //移動
        ObjectPositionUpdate(ObjectBase.MoveType.Rigidbody);
    }

    ///***************************
    /// public

    [SerializeField] public Transform TargetTransform;
    [SerializeField] public float MoveSpeed;
    [Space]
    [Header("索敵距離")]
    [SerializeField] public float SearchDistance;

}