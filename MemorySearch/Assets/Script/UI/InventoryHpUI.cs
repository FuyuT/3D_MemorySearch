using UnityEngine;

public class InventoryHpUI : MonoBehaviour
{
    [SerializeField] GameObject[] hpBar;

    private void OnEnable()
    {
        int nowHp = Player.readPlayer.GetHP();

        for (int n = 0; n < hpBar.Length; n++)
        {
            if (nowHp <= n)
            {
                hpBar[n].SetActive(false);
            }
            else
            {
                hpBar[n].SetActive(true);
            }
        }
    }
}
