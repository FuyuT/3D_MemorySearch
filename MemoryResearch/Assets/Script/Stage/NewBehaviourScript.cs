//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class NewBehaviourScript : MonoBehaviour
//{
//    private CharacterController characterController;
//    private Animator animator;
//    private Vector3 velocity = Vector3.zero;



//   // private GameObject Bel;
  
//    //�@�������̏�ɂ��邩�ǂ���
//    private bool onTheFloor = false;
//    //�@�������̓����Ă�������
//    private Vector3 floorMoveDirection;


//    // Start is called before the first frame update
//    void Start()
//    {

//        //Bel = GameObject.Find("BeltConveyor");
//        characterController = GetComponent<CharacterController>();
//        animator = GetComponent<Animator>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
   
//            //�@�x���g�R���x�A�[�ɏ���Ă�����͂�������	
//            if (onTheFloor)
//            {
//          
//                velocity += floorMoveDirection;
//            }
        

//        //velocity.y += Physics.gravity.y * Time.deltaTime;
//        //characterController.Move(velocity * Time.deltaTime);
//    }

//    void OnControllerColliderHit(ControllerColliderHit col)
//    {
//       

//        //�@���̃R���C�_�ƐڐG���Ă����牺�����Ƀ��C���΂���Block���ǂ������ׂ�
//        if (Physics.Linecast(transform.position + Vector3.up * 0.1f, transform.position + Vector3.up * 0.1f + Vector3.down * 0.2f, LayerMask.GetMask("Block")))
//        {
//           
//            var beltConveyor = col.gameObject.GetComponent<BeltConveyor>();
//            if (beltConveyor != null)
//            {
//             
//                //floorMoveDirection = FindObjectOfType<BeltConveyor>().ConveyorVelocity();
//                floorMoveDirection = beltConveyor.ConveyorVelocity();
//                onTheFloor = true;
//            }
//            else
//            {
//                onTheFloor = false;
//            }
//        }
//        else
//        {
//        
//            onTheFloor = false;
//        }
//    }


//}
