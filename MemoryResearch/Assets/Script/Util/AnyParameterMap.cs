using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Map = System.Collections.Generic.Dictionary<int, object>;

/**
 * @brief       �p�����[�^�ۑ�
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
	 * @brief		�p�����[�^�̒ǉ�
	 *              �o�^�ł��Ȃ������ꍇ��false
	 * @param[in]	key			�L�[
	 * @param[in]	value		�l
	 */
	public bool Add(int key, object value)
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
	 * @brief		�p�����[�^�̎擾
	 * �@			���݂��Ȃ��ꍇ��null��Ԃ�
	 * @param[in]	key			�L�[
	 * @return		�擾�����p�����[�^(object)
	 */
	public object Get(int key)
	{
		if(!parameters.ContainsKey(key))
        {
			return null;
        }
		return parameters[key];
	}

	/**
	 * @brief		�p�����[�^�̎擾�i�W�F�l���b�N�Łj
	 * �@			���݂��Ȃ��ꍇ��null��Ԃ�
	 * @param[in]	key			�L�[
	 * @return		�擾�����p�����[�^(object)
	 */
	public Type Get<Type>(int key)
	{
		Type returnValue = default(Type);

		if (!parameters.ContainsKey(key))
        {
			Debug.LogError("�L�[�ɊY������p�����[�^�����݂��܂���");
			//todo:�L�[�ɊY������p�����[�^��������Ȃ������ꍇ�̕Ԃ�l��ύX����(���݂͎w�肵���^��default�l��Ԃ��j
			return returnValue;
        }

		try
        {
			returnValue = (Type)parameters[key];
        }
		catch
        {
			Debug.LogError("�p�����[�^�擾���Ɏw�肵���^�ɕϊ��ł��܂���ł����B");
        }

		return returnValue;
	}


	/**
	 * @brief		�p�����[�^�̐ݒ�
	 *              �ݒ�ł�����true�A�ł��Ȃ����false
	 * @param[in]	key			�L�[
	 * @param[in]	value		�l
	 */

	public bool Set(int key, object value)
    {
		if (!parameters.ContainsKey(key))
		{
			Debug.LogError("�L�[�ɊY������p�����[�^�����݂��܂���");
			return false;
		}
		parameters[key] = value;
		return true;
	}
}
