using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, Vector3.one);
    }
}