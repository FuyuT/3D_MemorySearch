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

    /*******************************
    * override
    *******************************/
    public override void Create()
    {
        var projectile = Object.Instantiate(this);

        projectile.GetComponent<ProjectileBase>().Init(this.transform.position,
            this.transform.rotation, transform.lossyScale, moveVec, speed, attackInfo.power);
        projectile.GetComponent<ProjectileBase>().SetCollisionExclusionID(collisionExclusionID);

        projectile.gameObject.SetActive(true);
    }
}