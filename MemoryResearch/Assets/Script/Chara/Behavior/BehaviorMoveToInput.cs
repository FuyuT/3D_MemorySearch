using UnityEngine;

public static class BehaviorMoveToInput
{
    /// <summary>
    /// 入力方向のベクトルを取得
    /// </summary>
    /// <returns>入力方向のベクトル</returns>
    public static Vector3 GetInputVec(bool isParseToCamera = false)
    {
        //入力値を取得
        Vector3 inputVec = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            inputVec += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVec += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVec += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVec += new Vector3(-1, 0, 0);
        }

        return inputVec;
    }

    /// <summary>
    /// メインカメラに合わせたベクトルに変換する
    /// ※XとZのみ（Yは0にする）
    /// </summary>
    /// <param name="outVec">変換する変数</param>
    public static void ParseToCameraVec(ref Vector3 outVec)
    {
        outVec = Camera.main.transform.forward * outVec.z + Camera.main.transform.right * outVec.x;
        outVec.y = 0;
    }

    public static Vector3 GetDushVec(Vector3 frontVec)
    {
        Vector3 moveVec = GetInputVec();

        //入力がなければ、前方のベクトルを返す
        if(moveVec == Vector3.zero)
        {
            return frontVec;
        }

        //進行方向をカメラの向きと合わせる
        moveVec = Camera.main.transform.forward * moveVec.z + Camera.main.transform.right * moveVec.x;

        //y軸の移動を無効化
        moveVec.y = 0;

        return moveVec;
    }
}
