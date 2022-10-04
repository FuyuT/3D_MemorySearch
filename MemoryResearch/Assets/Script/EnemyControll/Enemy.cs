using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Enemy>;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// �X�e�[�genum
    /// </summary>
    public enum Event
    {
        Idle,
        //�ړ�
        Move,
        //�W�����v
        Jump,
        Floating,
        //�U��
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
            Debug.Log("�l��o�^");
        }
        else
        {
            Debug.Log("�o�^�Ɏ��s");
        }

    }

    /// <summary>
    /// ������
    /// </summary>
    private void Init()
    {
        StateMachineInit();
    }

    /// <summary>
    /// �X�e�[�g�}�V���̏�����
    /// </summary>
    void StateMachineInit()
    {
        stateMachine = new StateMachine<Enemy>(this);
        //�A�N�^�[�������̓p�����[�^�K�{
    }


    // Update is called once per frame
    void Update()
    {
        int a = (int)parameter.Get("a");
        Debug.Log(a);
    }
}
