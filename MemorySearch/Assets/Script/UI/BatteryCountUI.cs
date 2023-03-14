using UnityEngine;
using UnityEngine.UI;

public class BatteryCountUI : MonoBehaviour
{
    enum Type
    {
        Possesion,
        To,
        Combine,
        AddAnim,
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
        switch (type)
        {
            case Type.Possesion:
                text.text = ToUpper(DataManager.instance.IPlayerData().GetPossesionCombineCost().ToString());
                break;
        }
    }

    string ToUpper(string value)
    {
        value = StringWidthConverter.ConvertToFullWidth(value);
        return value.Replace("ÅD", ".");
    }

    /*******************************
    * public
    *******************************/
    public void SetBatteryCount(float count)
    {
        if (type == BatteryCountUI.Type.AddAnim)
        {
            text.text = ToUpper("Å{" + count.ToString());
        }
        else
        {
            text.text = ToUpper(count.ToString());
        }
    }

    public void InitToCost(float to)
    {
        SetBatteryCount(to);
        text.color = Color.black;
    }

    public void UpdateToCost(float to)
    {
        SetBatteryCount(to);
        if(to < 0)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.red;
        }
    }
}