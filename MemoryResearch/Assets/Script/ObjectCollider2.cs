using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollider2 : MonoBehaviour
{
    public bool inArea3 = false;


    public enum CollisionObject
    {
        None,
        Player,
    }

    public CollisionObject colObj { get; private set; }

    private void Awake()
    {
        colObj = CollisionObject.None;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            colObj = CollisionObject.Player;
            inArea3 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colObj = CollisionObject.None;
        inArea3 = false;
    }
}
