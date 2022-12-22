using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockon : MonoBehaviour
{

    [SerializeField]
    private GameObject target;

    //[SerializeField]
    // Camera ca;

    protected void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Enemy")
        {
            target = c.gameObject;
        }
    }

    protected void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Enemy")
        {
            target = null;
        }
    }

    public GameObject getTarget()
    {
        return target;
    }
}
