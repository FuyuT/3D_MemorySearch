using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Enemy>;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// ステートenum
    /// </summary>
    public enum Event
    {
        Idle,
        //移動
        Move,
        //ジャンプ
        Jump,
        Floating,
        //攻撃
        Attack_Punch,
        Attack_Tackle,
    }

    StateMachine<Enemy> stateMachine;

    [SerializeField] Transform Player;
    AnyParameterMap parameter;

    /// <summary>
    /// MainStart
    /// </summary>
    void Start()
    {
        Init();
        parameter = new AnyParameterMap();

        if (parameter.Add("a", 1))
        {
            Debug.Log("値を登録");
        }
        else
        {
            Debug.Log("登録に失敗");
        }

    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Init()
    {
        StateMachineInit();
    }

    /// <summary>
    /// ステートマシンの初期化
    /// </summary>
    void StateMachineInit()
    {
        stateMachine = new StateMachine<Enemy>(this);
        //アクターもしくはパラメータ必須
    }


    // Update is called once per frame
    void Update()
    {
        int a = (int)parameter.Get("a");
        Debug.Log(a);
    }
}
