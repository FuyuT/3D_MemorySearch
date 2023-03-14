using UnityEngine;

namespace MyUtil
{
    class ActiveSetter : MonoBehaviour
    {
        [SerializeField] GameObject[] activeTargets;

        public void SetActive(int no)
        {
            activeTargets[no].SetActive(true);
        }

        public void SetNotActive(int no)
        {
            activeTargets[no].SetActive(false);
        }
    }
}
