using UnityEngine;

namespace MyUtil
{
    public class TargetCollider : MonoBehaviour
    {
        /*******************************
        * private
        *******************************/
        [SerializeField] GameObject target;

        private void Awake()
        {
            InTarget = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == target)
            {
                InTarget = true;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject == target)
            {
                InTarget = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == target)
            {
                InTarget = false;
            }
        }

        /*******************************
        * public
        *******************************/
        /// <summary>
        /// ターゲットがエリアに入っているか
        /// 入っていればtrue,そうでなければfalse
        /// </summary>
        public bool InTarget { get; private set; }
    }
}