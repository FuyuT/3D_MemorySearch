using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    /*******************************
    * protected
    *******************************/
    protected bool    isPrefab;
    protected Vector3 moveVec;
    protected float   speed;
    protected int     damage;

    /*******************************
    * public
    *******************************/
    public ProjectileBase()
    {
        isPrefab = true;
        moveVec  = Vector3.zero;
        speed    = 0.0f;
        damage   = 0;
    }
    public void Init(Vector3 pos, Quaternion rot, Vector3 moveVec, float speed, int damage)
    {
        transform.position = pos;
        transform.rotation = rot;
        isPrefab = false;
        this.moveVec = moveVec;
        this.speed = speed;
        this.damage = damage;
        this.gameObject.SetActive(true);
    }

    public void Init(Vector3 pos, Quaternion rot, Vector3 scale, Vector3 moveVec, float speed, int damage)
    {
        transform.position = pos;
        transform.rotation = rot;
        transform.localScale = scale;
        isPrefab = false;
        this.moveVec = moveVec;
        this.speed = speed;
        this.damage = damage;
        this.gameObject.SetActive(true);
    }

    public void SetMoveVec(Vector3 moveVec)
    {
        this.moveVec = moveVec;
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    /*******************************
    * virtual
    *******************************/
    virtual public void Create()
    {
        var projectile = Object.Instantiate(this);

        projectile.Init(transform.position,
            transform.rotation, transform.lossyScale, transform.forward, speed, damage);
    }

    /*******************************
    * colision
    *******************************/
    public void OnCollisionStay(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                other.gameObject.GetComponentInChildren<CharaBase>().Damage(damage);
                break;
            case "Untagged":
                return;
        }
        Destroy(this.gameObject);
    }
}