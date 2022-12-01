using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warp : MonoBehaviour

{
    private GameObject Player;

    public GameObject point;

    float Countdown;

    bool flg;

   
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Countdown = 10;
        flg = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(flg)
        {
         
            Countdown -= Time.deltaTime;
            
           
        }
        if(Countdown<=0)
        {
          
            Countdown = 10;
            flg = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!flg)
        {
            if (other.name == Player.name)
            {
                //Charaが接触したらpointオブジェクトの位置に移動するよ！
                Player.transform.position = point.transform.position;
                flg = true;
            }
        }
    }
}
