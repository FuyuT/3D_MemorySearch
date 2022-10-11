using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Map = System.Collections.Generic.Dictionary<string, object>;

/**
 * @brief       パラメータ保存
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
	 * @brief		パラメータの追加
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
	 * @brief		パラメータの取得
	 * 　			存在しない場合はnullを返す
	 * @param[in]	key			キー
	 * @return		取得したパラメータ(object)
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
	 * @brief		パラメータの取得（ジェネリック版）
	 * 　			存在しない場合はnullを返す
	 * @param[in]	key			キー
	 * @return		取得したパラメータ(object)
	 */
	public Type Get<Type>(string key)
	{
		Type returnValue = default(Type);

		if (!parameters.ContainsKey(key))
        {
			Debug.LogError("キーに該当するパラメータが存在しません");
			//todo:キーに該当するパラメータが見つからなかった場合の返り値を変更する(現在は指定した型のdefault値を返す）
			return returnValue;
        }

		try
        {
			returnValue = (Type)parameters[key];
        }
		catch
        {
			Debug.LogError("パラメータ取得時に指定した型に変換できませんでした。");
        }

		return returnValue;
	}


	/**
	 * @brief		パラメータの設定
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
