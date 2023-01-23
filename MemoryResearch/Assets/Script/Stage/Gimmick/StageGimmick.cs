using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageGimmick : MonoBehaviour
{
    [Header("パネル")]
    [SerializeField]GameObject panel;

    [Header("ステージ")]
    [SerializeField]GameObject sutage;

    //プレイヤー格納用
    [SerializeField] GameObject Player;

    //表示するテキスト格納用
    [SerializeField] GameObject ShowText;

    //カメラ変更フラグ      
    public bool ChangFlg;

    //範囲に入ったか
    public bool RangeInFlg;

    public CollisionObject colObj { get; private set; }

    public enum CollisionObject
    {
        None,
        Player,
    }

    float alfa;
    float InOROutSpeed = 0.01f;
    float red, green, blue;

    // Start is called before the first frame update
    void Start()
    {
        ChangFlg = false;
        RangeInFlg = false;
        colObj = CollisionObject.None;

        red = panel.GetComponent<Image>().color.r;
        green = panel.GetComponent<Image>().color.g;
        blue = panel.GetComponent<Image>().color.b;
    }

    // Update is called once per frame
    void Update()
    {
        if (ChangFlg)
        {
            sutage.SetActive(false);
            panel.GetComponent<Image>().color = new Color(red, green, blue, alfa);
            alfa += InOROutSpeed;
        }
    }

    //装置に近づくと表示
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShowText.SetActive(true);
            colObj = CollisionObject.Player;
            RangeInFlg = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShowText.SetActive(false);
            colObj = CollisionObject.None;
            RangeInFlg = false;
        }
        colObj = CollisionObject.None;
    }
}
