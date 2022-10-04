using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Map = System.Collections.Generic.Dictionary<string, object>;

/**
 * @brief       �p�����[�^�[�ۑ�
 */
public class AnyParameterMap
{
    //�}�b�v
    Map parameters;

	public AnyParameterMap()
    {
		parameters = new Map();
    }

	/**
	 * @brief		�p�����[�^�[�̒ǉ�
	 *              �o�^�ł��Ȃ������ꍇ��false
	 * @param[in]	key			�L�[
	 * @param[in]	value		�l
	 */
	public bool Add(string key, object value)
    {
        try
        {
			parameters.Add(key, value);
		}
		catch
        {
			return false;
        }
		return true;
    }

	/**
	 * @brief		�p�����[�^�[�̎擾
	 * �@			���݂��Ȃ��ꍇ��null��Ԃ�
	 * @param[in]	key			�L�[
	 * @return		�擾�����p�����[�^�[
	 */
	public object Get(string key)
	{
		if(!parameters.ContainsKey(key))
        {
			return null;
        }
		return parameters[key];
	}

	/**
	 * @brief		�p�����[�^�[�̐ݒ�
	 *              �ݒ�ł�����true�A�ł��Ȃ����false
	 * @param[in]	key			�L�[
	 * @param[in]	value		�l
	 */

	public bool Set(string key, object value)
    {
		if (!parameters.ContainsKey(key))
		{
			return false;
		}
		parameters[key] = value;
		return true;
	}


}
