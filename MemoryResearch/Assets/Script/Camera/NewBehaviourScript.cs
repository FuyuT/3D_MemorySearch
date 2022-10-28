using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    //今使用されてるカメラ
    [SerializeField]
    GameObject OnCamera;

    //次に作動するカメラ
    [SerializeField]
    GameObject OffCamera;

    // Start is called before the first frame update
    void Start()
    {
        OnCamera.SetActive(true);

        OffCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnCamera.SetActive(false);
            OffCamera.SetActive(true);
        }
    }
}
