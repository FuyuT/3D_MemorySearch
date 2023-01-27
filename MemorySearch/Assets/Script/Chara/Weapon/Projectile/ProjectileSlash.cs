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

        projectile.GetComponent<ProjectileSlash>().Init(this.transform.position,
            this.transform.rotation, moveVec, speed, damage);

        projectile.GetComponent<ProjectileSlash>().PlayEffect();
    }
}