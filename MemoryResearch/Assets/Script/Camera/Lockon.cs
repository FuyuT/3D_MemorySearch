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
        return this.target;
    }

   // Start is called before the first frame update
    void Start()
    {

    }

    //Update is called once per frame
    void Update()
    {
   
    }

    // void FixedUpdate()
    //{
    //    transform.rotation = Quaternion.LookRotation(ca.transform.forward);
    //}
}
