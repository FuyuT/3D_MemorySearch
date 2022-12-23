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
        //���G�͈͓��ɂ��邩�m�F �͈͓��Ȃ�Ǐ](chase)�A����ȊO�Ȃ���G(search)
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
    /// �p�x�̍X�V
    /// </summary>
    void RotateUpdate()
    {
        RotateUpdateToMoveVec();
    }

    /// <summary>
    /// �ʒu�̍X�V
    /// </summary>
    void PositionUpdate()
    {
        //�ړ�
        ObjectPositionUpdate(ObjectBase.MoveType.Rigidbody);
    }

    ///***************************
    /// public

    [SerializeField] public Transform TargetTransform;
    [SerializeField] public float MoveSpeed;
    [Space]
    [Header("���G����")]
    [SerializeField] public float SearchDistance;

}