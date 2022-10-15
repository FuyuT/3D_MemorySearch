using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActor
{
    /// <summary>
    /// �A�N�^�[�̍X�V����
    /// �ʒu�̍X�V
    /// �p�x�̍X�V���s��
    /// </summary>
    void TransformUpdate();

    /// <summary>
    /// �p�����[�^�擾
    /// </summary>
    /// <returns>AnyParameterMap</returns>
    ref AnyParameterMap GetParameterMap();

    #region ���W�֌W
    /// <summary>
    /// ���W�擾
    /// </summary>
    /// <returns>���WX</returns>
    float GetPositionX();

    /// <summary>
    /// ���W�擾
    /// </summary>
    /// <returns>���WY</returns>
    float GetPositionY();

    /// <summary>
    /// ���W�擾
    /// </summary>
    /// <returns>���WZ</returns>
    float GetPositionZ();

    /// <summary>
    /// ���W�擾
    /// </summary>
    /// <returns>���W</returns>
    Vector3 GetPosition();

    /// <summary>
    /// ���W�ݒ�
    /// </summary>
    /// <param name="pos">�ݒ肷����W</param>
    void SetPosition(Vector3 pos);
    #endregion

    #region �p�x�֌W
    /// <summary>
    /// �p�x�擾
    /// </summary>
    /// <returns>AnyParameterMap</returns>
    Vector3 GetRotate();

    /// <summary>
    /// �p�x�ݒ�
    /// </summary>
    /// <param name="rotate">�ݒ肷��p�x</param>
    void SetRotate(Vector3 rotate);

    #endregion
}
