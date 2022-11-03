using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollider : MonoBehaviour
{
    public bool inArea=false;

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
        if(other.tag == "Player")
        {
            colObj = CollisionObject.Player;
            inArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colObj = CollisionObject.None;
        inArea = false;
    }
}
