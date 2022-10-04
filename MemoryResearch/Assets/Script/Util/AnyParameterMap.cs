using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Map = System.Collections.Generic.Dictionary<string, object>;

/**
 * @brief       パラメーター保存
 */
public class AnyParameterMap
{
    //マップ
    Map parameters;

	public AnyParameterMap()
    {
		parameters = new Map();
    }

	/**
	 * @brief		パラメーターの追加
	 *              登録できなかった場合はfalse
	 * @param[in]	key			キー
	 * @param[in]	value		値
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
	 * @brief		パラメーターの取得
	 * 　			存在しない場合はnullを返す
	 * @param[in]	key			キー
	 * @return		取得したパラメーター
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
	 * @brief		パラメーターの設定
	 *              設定できたらtrue、できなければfalse
	 * @param[in]	key			キー
	 * @param[in]	value		値
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
