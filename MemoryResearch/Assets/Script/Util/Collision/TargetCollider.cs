using UnityEngine;

namespace MyUtil
{
    public class TargetCollider : MonoBehaviour
    {
        /*******************************
        * private
        *******************************/
        [Header("使用するキャラクター")]
        [SerializeField] GameObject chara;

        [Header("ターゲットのタグ　入力しなければ、自分のタグでターゲットのタグを判断する")]
        [SerializeField] string tag = null;

        private void Awake()
        {
            if(tag == null || tag.Length == 0)
            {
                switch(chara.gameObject.tag)
                {
                    case "Player":
                        tag = "Enemy";
                        break;
                    case "Enemy":
                        tag = "Player";
                        break;
                    default:
                        Destroy(this);
                        break;
                }
            }
            InTarget = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(tag))
            {
                InTarget = true;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag(tag))
            {
                InTarget = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(tag))
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