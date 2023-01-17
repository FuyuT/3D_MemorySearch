using UnityEngine;
using UnityEngine.UI;

public class BatteryCountUI : MonoBehaviour
{
    [SerializeField] Text text;

    private void Update()
    {
        CombineBattery battery = Player.readPlayer.GetCombineBattery();
        //todo:XV‰ñ”‚ğŒ¸‚ç‚·
        text.text = battery.GetBatteryCount().ToString();
    }

    private void OnEnable()
    {

    }
}
