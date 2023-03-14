using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorCamara : MonoBehaviour
{
    [SerializeField] GameObject Player;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        transform.LookAt(Player.transform);
    }
}
