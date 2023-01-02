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

        void Start() {
            if (pathCreator != null)
            {
                pathCreator.path.SetSituation(endOfPathInstruction);
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pathCreator.path.Restart();
            }

            //ゴールについていたら終了
            if (pathCreator.path.IsEndPath()) return;
            transform.position = pathCreator.path.GetPointAtDistance(speed);
            transform.rotation = pathCreator.path.GetRotationAtDistance();

        }

    }
}