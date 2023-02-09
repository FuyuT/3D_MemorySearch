using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    /*******************************
    * protected
    *******************************/
    protected AttackInfo attackInfo;
    protected bool    isPrefab;
    protected Vector3 moveVec;
    protected float   speed;

    protected int collisionExclusionID;

    /*******************************
    * public
    *******************************/
    public ProjectileBase()
    {
        attackInfo = new AttackInfo();
        isPrefab = true;
        moveVec  = Vector3.zero;
        speed    = 0.0f;
        attackInfo.power = 0;
        collisionExclusionID = -1;
    }

    public void Init(Vector3 pos, Quaternion rot, Vector3 moveVec, float speed, int damage)
    {
        transform.position = pos;
        transform.rotation = rot;
        isPrefab = false;
        this.moveVec = moveVec;
        this.speed = speed;
        this.attackInfo.power = damage;
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
        this.attackInfo.power = damage;
        this.gameObject.SetActive(true);
    }

    public void SetColliderSize(Vector3 scale)
    {
        gameObject.GetComponent<BoxCollider>().size = scale;
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
        this.attackInfo.power = damage;
    }
    public void SetCollisionExclusionID(int id)
    {
        this.collisionExclusionID = id;
    }

    /*******************************
    * virtual
    *******************************/
    virtual public void Create()
    {
        var projectile = Object.Instantiate(this);

        projectile.Init(transform.position,
            transform.rotation, transform.lossyScale, transform.forward, speed, attackInfo.power);
    }

    /*******************************
    * colision
    *******************************/

    private void OnCollisionEnter(Collision collision)
    {
        if (isPrefab) return;

        switch (collision.gameObject.tag)
        {
            case "Player":
                AttackInfo attackInfo = this.attackInfo;
                attackInfo.attacker = gameObject;
                attackInfo.attackPos = transform.position;
                collision.gameObject.GetComponentInChildren<CharaBase>().AddDamageInfo(attackInfo.id, attackInfo);
                break;
            case "Untagged":
                return;
        }

        //親IDが設定されていたら
        if (collisionExclusionID != -1)
        {
            //オブジェクトの親に当たっていたら終了
            try
            {
                if (collision.gameObject.GetComponent<CharaBase>().GetID() == collisionExclusionID) return;
            }
            catch
            {

            }
        }

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPrefab) return;

        switch (other.gameObject.tag)
        {
            case "Player":
                AttackInfo attackInfo = this.attackInfo;
                attackInfo.attacker = gameObject;
                attackInfo.attackPos = transform.position;
                other.gameObject.GetComponentInChildren<CharaBase>().AddDamageInfo(attackInfo.id, attackInfo);
                break;
            case "Untagged":
                return;
        }

        //親IDが設定されていたら
        if (collisionExclusionID != -1)
        {
            //オブジェクトの親に当たっていたら終了
            try
            {
                if (other.gameObject.GetComponent<CharaBase>().GetID() == collisionExclusionID) return;
            }
            catch
            {

            }
        }

        Destroy(this.gameObject);
    }
}