using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Linq;

[Serializable]
public class ResponseObjects
{
	public RankingModel ranking;
}

public class CommunicationManager : MonoBehaviour
{

	public static IEnumerator ConnectServer(string endpoint, string paramater, Action action = null)
	{
		// *** リクエストの送付 ***
		UnityWebRequest unityWebRequest = UnityWebRequest.Get(Global.Server_URL + endpoint + paramater);
		yield return unityWebRequest.SendWebRequest();
		// エラーの場合
		if (!string.IsNullOrEmpty(unityWebRequest.error))
		{
			Debug.LogError(unityWebRequest.error);
			yield break;
		}

		// *** レスポンスの取得 ***
		string text = unityWebRequest.downloadHandler.text;
		Debug.Log("レスポンス : " + text);
		// エラーの場合
		if (text.All(char.IsNumber))
		{
			Debug.LogError("サーバーでエラーが発生しました。[システムエラー]");
			yield break;
		}

		// 正常終了アクション実行
		if (action != null)
		{
			action();
			action = null;
		}
	}
}