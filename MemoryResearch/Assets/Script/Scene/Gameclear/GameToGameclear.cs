using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameToGameclear : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.ToClear();
        }
    }
}
