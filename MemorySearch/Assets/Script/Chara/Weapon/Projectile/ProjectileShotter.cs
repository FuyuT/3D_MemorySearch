using UnityEngine;

public class ProjectileShotter : MonoBehaviour
{
    /*******************************
    * private
    *******************************/
    [SerializeField] ProjectileBase projectile;

    /*******************************
    * public
    *******************************/
    public void Shot()
    {
        projectile.Create();
    }
}