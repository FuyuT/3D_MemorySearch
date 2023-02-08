using UnityEngine;

public class UniqueIDSetter : MyUtil.Singleton<UniqueIDSetter>
{
    /*******************************
    * private
    *******************************/

    int beforeID;
    public UniqueIDSetter()
    {
        beforeID = -1;
    }

    /*******************************
    * public
    *******************************/

    public int GetUniqueID()
    {
        //振られていないIDを返す
        return beforeID++;
    }
}