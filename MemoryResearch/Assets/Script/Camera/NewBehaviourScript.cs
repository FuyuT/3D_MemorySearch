using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    //���g�p����Ă�J����
    [SerializeField]
    GameObject OnCamera;

    //���ɍ쓮����J����
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
