using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollider3 : MonoBehaviour
{
    public bool inArea4 = false;

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
            inArea4 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colObj = CollisionObject.None;
        inArea4 = false;
    }
}
