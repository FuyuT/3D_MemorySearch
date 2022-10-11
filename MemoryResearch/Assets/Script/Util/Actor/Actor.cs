using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IActor, IReaderActor
{
    AnyParameterMap parameters;

    public Actor()
    {
        parameters = new AnyParameterMap();
    }


    /// <summary>
    /// �A�N�^�[�̍X�V����
    /// �ʒu�̍X�V
    /// �p�x�̍X�V���s��
    /// </summary>
    public void TransformUpdate()
    {

    }

    /// <summary>
    /// �p�����[�^�擾
    /// </summary>
    /// <returns>AnyParameterMap</returns>
    public ref AnyParameterMap GetParameterMap()
    {
        return ref parameters;
    }

    #region ���W�֌W
    /// <summary>
    /// ���W�擾
    /// </summary>
    /// <returns>���WX</returns>
    public float GetPositionX()
    {
        return transform.position.x;
    }

    /// <summary>
    /// ���W�擾
    /// </summary>
    /// <returns>���WY</returns>
    public float GetPositionY()
    {
        return transform.position.y;
    }

    /// <summary>
    /// ���W�擾
    /// </summary>
    /// <returns>���WZ</returns>
    public float GetPositionZ()
    {
        return transform.position.z;
    }

    /// <summary>
    /// ���W�擾
    /// </summary>
    /// <returns>���W</returns>
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    /// <summary>
    /// ���W�ݒ�
    /// </summary>
    /// <param name="pos">�ݒ肷����W</param>
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    #endregion

    #region �p�x�֌W
    /// <summary>
    /// �p�x�擾
    /// </summary>
    /// <returns>AnyParameterMap</returns>
    public Vector3 GetRotate()
    {
        return transform.rotation.eulerAngles;
    }

    /// <summary>
    /// �p�x�ݒ�
    /// </summary>
    /// <param name="rotate">�ݒ肷��p�x</param>
    public void SetRotate(Vector3 rotate)
    {
        transform.rotation = Quaternion.Euler(rotate);
    }

    #endregion
}
