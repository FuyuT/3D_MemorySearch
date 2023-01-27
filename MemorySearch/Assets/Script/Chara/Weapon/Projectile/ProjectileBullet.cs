using UnityEngine;

public class ProjectileBullet : ProjectileBase
{
    /*******************************
    * private
    *******************************/
    private void Update()
    {
        if (isPrefab) return;
        transform.position += moveVec * speed * Time.deltaTime;
    }
}