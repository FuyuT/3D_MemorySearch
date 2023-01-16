using UnityEngine;
using UnityEngine.UI;

public class BatteryCountUI : MonoBehaviour
{
    enum Type
    {
        Possesion,
        To,
        Combine,
    }
    /*******************************
    * private
    *******************************/
    [SerializeField] Text text;
    [SerializeField] Type type;
    private void Update()
    {
        //CombineBattery battery = Player.readPlayer.GetCombineBattery();
        ////todo:çXêVâÒêîÇå∏ÇÁÇ∑
        //text.text = battery.GetBatteryCount().ToString();
    }

    private void OnEnable()
    {
        switch(type)
        {
            case Type.Possesion:
                text.text = DataManager.instance.IPlayerData().GetPossesionCombineCost().ToString();
                break;
        }

    }

    /*******************************
    * public
    *******************************/
    public void SetBatteryCount(float count)
    {
        text.text = count.ToString();
    }

    public void InitToCost(float to)
    {
        text.text = to.ToString();
        text.color = Color.black;
    }

    public void UpdateToCost(float to)
    {
        text.text = to.ToString();

        if(to < 0)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.black;
        }
    }
}
