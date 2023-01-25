using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transparentObject : MonoBehaviour
{
    private MeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FPS"))
        {
            mesh.enabled = true;
        }
    }
}
