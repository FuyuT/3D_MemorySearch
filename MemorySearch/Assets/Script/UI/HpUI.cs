using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUI : MonoBehaviour
{
    [SerializeField] Animator[] hpAnimator;

    int beforeHp;

    void Awake()
    {
        beforeHp = 5;
    }

    void Update()
    {
        if (beforeHp <= 0) return;
        Damage();
        Heal();
    }

    void Damage()
    {
        //プレイヤーのHPに変更があれば、UIのアニメーションを再生する
        if (Player.readPlayer.GetHP() < beforeHp)
        {
            //UIを減少させるアニメーションを再生

            for (int n = beforeHp; n > Player.readPlayer.GetHP(); n--)
            {
                if (n <= 0) break;
                BehaviorAnimation.UpdateTrigger(ref hpAnimator[n - 1], "Damage"); //HPの要素数は０から始まるので-1
            }
            beforeHp = Player.readPlayer.GetHP();
        }
    }

    void Heal()
    {
        //プレイヤーのHPに変更があれば、UIのアニメーションを再生する
        if (Player.readPlayer.GetHP() > beforeHp)
        {
            //UIを上昇させるアニメーションを再生

            for (int n = beforeHp; n < Player.readPlayer.GetHP(); n++)
            {
                BehaviorAnimation.UpdateTrigger(ref hpAnimator[n - 1], "Heal"); //HPの要素数は０から始まるので-1
            }
            beforeHp = Player.readPlayer.GetHP();
        }
    }
}
