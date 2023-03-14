using UnityEngine;
using Effekseer;

public class ProjectileSlash : ProjectileBase
{
    /*******************************
    * private
    *******************************/
    private void Update()
    {
        if (isPrefab) return;
        transform.position += moveVec * speed * Time.deltaTime;
    }

    public void PlayEffect()
    {
        GetComponent<Effekseer.EffekseerEmitter>().Play();
    }

    /*******************************
    * override
    *******************************/
    public override void Create()
    {
        var projectile = Object.Instantiate(this);

        projectile.GetComponent<ProjectileBase>().Init(this.transform.position,
            this.transform.rotation, moveVec, speed, attackInfo.power);
        projectile.GetComponent<ProjectileBase>().SetCollisionExclusionID(collisionExclusionID);

        //衝突半径を大きさを設定
        Vector3 size = gameObject.GetComponent<BoxCollider>().size;
        size.x *= gameObject.transform.lossyScale.x / gameObject.transform.localScale.x;
        size.y *= gameObject.transform.lossyScale.y / gameObject.transform.localScale.y;
        size.z *= gameObject.transform.lossyScale.z / gameObject.transform.localScale.z;

        projectile.GetComponent<ProjectileBase>().SetColliderSize(size);

        projectile.GetComponent<ProjectileSlash>().PlayEffect();
    }
}