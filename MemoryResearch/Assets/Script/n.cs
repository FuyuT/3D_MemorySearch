using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class n : MonoBehaviour
{
    public Vector3 MovePos;
    public Transform target;

    private float moveSpeed;

    void Start()
    { 
        moveSpeed = 2.0f;
        target.transform.position = new Vector3(5.5f, 3.2f, -2.8f);
        //target.transform.position = new Vector3(MovePos.x, MovePos.y, MovePos.z);

    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed);        
    }
}
