using UnityEngine;

public class BatteryItem : MonoBehaviour
{
    /*******************************
    * private
    *******************************/
    [SerializeField] float delayTimeMax;
    [SerializeField] float speed;

    [SerializeField] float startUpSpeed;
    [SerializeField] float upDecreaseSpeed;
    float startDistance;

    float nowTime = 0;
    float batteryPower;

    void Update()
    {
        if(nowTime <= delayTimeMax)
        {
            nowTime += Time.deltaTime;
            return;
        }

        Vector3 targetPos = Player.readPlayer.GetPos();
        if(startUpSpeed > 0)
        {
            targetPos.y += startUpSpeed;
            startUpSpeed -= upDecreaseSpeed;
        }
        
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed);
    }
    /*******************************
    * public
    *******************************/
    
    public void Create(Vector3 pos ,float batteryPower)
    {
        var battery = Instantiate(this);
        battery.transform.position = pos;
        battery.batteryPower = batteryPower;
    }

    /*******************************
    * è’ìÀîªíË
    *******************************/

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //çáê¨ÉRÉXÉgÇëùâ¡Ç≥ÇπÇÈ
            DataManager.instance.IPlayerData().AddPossesionCombineCost(batteryPower);
            Destroy(this.gameObject);
        }
    }
}
