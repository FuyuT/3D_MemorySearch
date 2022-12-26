using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectParam
{
    //////////////////////////////
    /// private

    Vector3 moveVec;
    [Header("回転速度(一秒で変わる量)")]
    [SerializeField] float rotateSpeed;
    [SerializeField] bool  isUseGravity;


    //////////////////////////////
    /// public
    public ObjectParam()
    {
        Init();
    }

    public void Init()
    {
        moveVec = Vector3.zero;
        rotateSpeed = 0;
        isUseGravity = false;
    }

    //moveVec
    public void InitMoveVec()
    {
        moveVec = Vector3.zero;
    }

    public Vector3 GetMoveVec()
    {
        return moveVec;
    }

    public void AddMoveVec(Vector3 vec)
    {
        moveVec += vec;   
    }

    //rotate
    public float GetRotateSpeed()
    {
        return rotateSpeed;
    }

    public void SetRotateSpeed(float speed)
    {
        rotateSpeed = speed;
    }

    //gravity
    public bool IsUseGravity()
    {
        return isUseGravity;
    }

    public void SetUseGravity(bool flg)
    {
        isUseGravity = flg;
    }
}

public class ObjectBase : MonoBehaviour
{
    ///******************************
    /// private

    /// <summary>
    /// 位置の更新 rigidBodyのVelocityを使用して進行方向にmoveVecを設定する
    /// isUseGravityがtrueなら重力をベクトルに加え、falseなら加えない
    /// </summary>
    void PositionUpdateByRigidBody()
    {
        Rigidbody rigidbody;
        try
        {
            rigidbody = gameObject.GetComponent<Rigidbody>();
        }
        catch
        {
            return;
        }

        //ベクトルを設定
        Vector3 moveVec = objectParam.GetMoveVec();

        if (objectParam.IsUseGravity())
        {
            //重力をベクトルに加える
            moveVec += new Vector3(0, rigidbody.velocity.y, 0);
        }

        rigidbody.velocity = moveVec;
    }

    /// <summary>
    /// 位置の更新 重力を使用せずに、
    /// transform.position +=moveVec * TimeDeltatimeするのみ
    /// </summary>
    void PositionUpdateByTransform()
    {
        //ベクトルを設定
        transform.position += objectParam.GetMoveVec() * Time.deltaTime;
    }


    ///******************************
    /// protected
    protected ObjectBase()
    {
        ObjectBaseInit();
    }

    protected void ObjectBaseInit()
    {
    }

    ///******************************
    /// public
    public enum MoveType
    {
        Rigidbody,
        Transform,
    }
    [SerializeField] public ObjectParam objectParam;

    /// <summary>
    /// 角度の更新
    /// moveVecの方向に回転させる
    /// </summary>
    public void RotateUpdateToMoveVec()
    {
        //todo:現在のステートkeyを取得する関数を作成する
        if (objectParam.GetMoveVec() != Vector3.zero)
        {
            //yは回転の要素に加えない
            Vector3 temp = objectParam.GetMoveVec();
            temp.y = 0;
            var quaternion = Quaternion.LookRotation(temp);
            transform.rotation = Quaternion.Slerp(this.transform.rotation, quaternion, objectParam.GetRotateSpeed() * Time.deltaTime);
        }
    }

    /// <summary>
    /// 位置の更新
    /// </summary>
    /// <param name="type">移動方法の種類</param>
    public void ObjectPositionUpdate(ObjectBase.MoveType type)
    {
        switch(type)
        {
            case ObjectBase.MoveType.Rigidbody:
                PositionUpdateByRigidBody();
                break;
            case ObjectBase.MoveType.Transform:
                PositionUpdateByTransform();
                break;
        }

        objectParam.InitMoveVec();
    }
}