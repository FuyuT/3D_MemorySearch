using UnityEditor;
using UnityEngine;

public class EquipmentJump : MyUtil.Singleton<EquipmentJump>
{
    /*******************************
    * private
    *******************************/


    /*******************************
    * public
    *******************************/
    public void Update(Player owner)
    {

    }

    public bool IsPossible(Player owner)
    {
        if (!owner.isGround) return false;

        return true;
    }
}
