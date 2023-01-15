using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomInputKey;


/// <summary>
/// オブジェクトを動かせるカメラを起動させる
/// コンソールの当たり判定に入っているかを検知する
/// </summary>
public class MoveObjectConsoleRange : MonoBehaviour
{
    //案内UI
    [SerializeField] GameObject GuideUI;

    //範囲に入ったか
    public bool InRange { get; private set; }


    void Start()
    {
        InRange = false;
    }

    //装置に近づくと表示
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GuideUI.SetActive(true);
            InRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GuideUI.SetActive(false);
            InRange = false;
        }
    }
}
