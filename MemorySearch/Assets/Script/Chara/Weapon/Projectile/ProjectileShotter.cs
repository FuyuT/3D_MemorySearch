using UnityEngine;

public class ProjectileShotter : MonoBehaviour
{
    /*******************************
    * private
    *******************************/
    [SerializeField] ProjectileBase projectile;
    [SerializeField] CharaBase collisionExclusionChara;
    public void SetCollisionExclusionChara()
    {
        if(collisionExclusionChara)
        {
            projectile.SetCollisionExclusionID(collisionExclusionChara.GetID());
        }
    }

    /*******************************
    * public
    *******************************/
    public void Shot()
    {
        SetCollisionExclusionChara();
        projectile.Create();
    }

}