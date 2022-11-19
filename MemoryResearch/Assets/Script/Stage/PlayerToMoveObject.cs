using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToMoveObject : MonoBehaviour
{
    public GameObject elevator;
    public GameObject Movefloor;
    private Vector3 floorMoveDirection;

    //　動く床の上にいるかどうか
    private bool onTheFloor = false;


    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //　ベルトコンベアーに乗っていたら力を加える	
        if (onTheFloor)
        {

            velocity += floorMoveDirection;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ////エレベータ
        //if (other.gameObject.name == "Area")
        //{
        //    if (elevator.GetComponent<Elevator>().is2ndFloor == false)
        //    {
        //        elevator.GetComponent<Elevator>().MoveUp();
        //    }

        //    if (elevator.GetComponent<Elevator>().is2ndFloor == true)
        //    {
        //        elevator.GetComponent<Elevator>().MoveDown();
        //    }
        //}

        //横移動の床
        if (other.tag == "floor")
        {

            this.gameObject.transform.parent = Movefloor.gameObject.transform;
        }


        if (other.gameObject.tag == "Block")
        {

            var beltConveyor = other.gameObject.GetComponent<BeltConveyor>();
            if (beltConveyor != null)
            {

                //floorMoveDirection = FindObjectOfType<BeltConveyor>().ConveyorVelocity();
                floorMoveDirection = beltConveyor.ConveyorVelocity();
                onTheFloor = true;
            }
            else
            {
                onTheFloor = false;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //横移動の床
        if (other.tag == "floor")
        {
            this.gameObject.transform.parent = null;
        }
    }
}
