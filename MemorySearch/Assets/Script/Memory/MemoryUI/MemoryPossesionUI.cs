using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPossesionUI : MonoBehaviour
{
    /*******************************
    * private
    *******************************/
    [SerializeField] MemoryUI[] MemoriesUI;

    int beforeMemoryCount;

    private void Update()
    {
        var possesionMemories = DataManager.instance.IPlayerData().GetPossesionMemories();

        //所持数が変わっていなければ終了
        if (beforeMemoryCount == possesionMemories.Count) return;

        //取得した順番に、レベルで並べ替えて表示する
        SetPossesionMemoriesUI();
    }

    private void OnEnable()
    {
        var possesionMemories = DataManager.instance.IPlayerData().GetPossesionMemories();

        //Activeになった時に、所持しているメモリをUIに反映させる
        SetPossesionMemoriesUI();

        //所持数を更新
        beforeMemoryCount = possesionMemories.Count;
    }

    //取得した順番並べる
    private void SetPossesionMemoriesUI()
    {
        var playerData = DataManager.instance.IPlayerData();
        var memoryData = DataManager.instance.IMemoryData();
        var possesionMemories = DataManager.instance.IPlayerData().GetPossesionMemories();

        //todo:所持メモリUIのソート（しないかも？）取得した順番に、レベルで昇順にソートしてUIにセットする
        //List<int> sortList;

        for (int n = 0; n < MemoriesUI.Length; n++)
        {
            //所持している数を超えていれば
            if(n > possesionMemories.Count - 1) //0からループを開始しているので、カウントを合わせるため-1している
            {
                //所持していないという意味のメモリ番号に変更する
                MemoriesUI[n].ChangeMemoryType(MemoryType.None);
            }
            else
            {
                //所持しているメモリに変更する
                MemoriesUI[n].ChangeMemoryType(possesionMemories[n]);
            }
        }

        //所持メモリをUIに反映
    }
}
