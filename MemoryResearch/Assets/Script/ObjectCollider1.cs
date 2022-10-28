using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollider1 : MonoBehaviour
{
    public bool inArea2 = false;


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
            inArea2 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colObj = CollisionObject.None;
        inArea2 = false;
    }
}
