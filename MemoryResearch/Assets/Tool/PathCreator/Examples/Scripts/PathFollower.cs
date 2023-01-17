using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed;
        public Quaternion endPathRotate;
        
        void Start() {
            if (pathCreator != null)
            {
                pathCreator.path.SetSituation(endOfPathInstruction);
                endPathRotate.eulerAngles = Vector4.zero;
            }
        }

        void Update()
        {
            MoveRestart();

            //ゴールについていたら終了
            if (pathCreator.path.IsEndPath()) return;
            transform.position = pathCreator.path.GetPointAtDistance(speed);
            transform.rotation = pathCreator.path.GetRotationAtDistance();
        }

        //移動のリスタート
        void MoveRestart()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pathCreator.path.Restart();
            }
        }
    }
}