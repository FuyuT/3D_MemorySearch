using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;//情報を読み込むStringReaderを使用するために導入

[System.Serializable]
public class MemoryData
{
    //////////////////////////////
    /// private
    
    Dictionary<MemoryType, Memory> dictionary;

    //////////////////////////////
    /// public

    public MemoryData()
    {
        dictionary = new Dictionary<MemoryType, Memory>();
    }

    /// <summary>
    /// 素材を元に合成されるメモリを検索し、返す
    /// </summary>
    /// <param name="material">素材配列</param>
    /// <returns>検索結果を返す、存在しなければ MemoryType.None を返す</returns>
    public MemoryType FindCombineMemory(MemoryType[] material)
    {

        bool[] isMatch = new bool[Global.MemoryMaterialMax] { false,false,false };
        //全てのメモリから検索
        for(MemoryType type = 0; type < MemoryType.Count; type++)
        {
            //初期化
            isMatch[0] = isMatch[1] = isMatch[2] = false;
            //素材の数分検索
            for (int mat = 0; mat < Global.MemoryMaterialMax; mat++)
            {
                //素材のどれか１つに当てはまるか調べる、当てはまったらその番号のisMachiをtrueにする
                for(int n = 0; n < Global.MemoryMaterialMax; n++)
                {
                    //すでに見つかっている素材は飛ばす
                    if(isMatch[n]) continue;

                    //見つけたら次の素材へ
                    if (dictionary[type].materialType[mat] == material[n])
                    {
                        isMatch[n] = true;
                        break;
                    }
                }
            }
            //全ての素材が見つかっていれば合成されるメモリを返す
            if(isMatch[0] && isMatch[1] && isMatch[2])
            {
                //todo:debug.log消す
                //Debug.Log("素材:" + material[0] +
                //    "+" + material[1] +
                //    "+" + material[2]);

                //Debug.Log("メモリ:" + dictionary[type].type + "         id:" + (int)dictionary[type].type);

                //Debug.Log("合成完了" + dictionary[type].materialType[0] + 
                //    "+" + dictionary[type].materialType[1] +
                //    "+" + dictionary[type].materialType[2]);
                return dictionary[type].type;
            }
        }

        //存在しなければMemoryType.Noneを返す
        return MemoryType.None;
    }

    /// <summary>
    /// メモリデータを読み込む
    /// </summary>
    /// <param name="dataText">メモリデータを纏めたテキスト</param>
    public void Load()
    {
        //データをバッファに読み込み
        List<string[]> dataBuffer = Util.TextUtility.ReadCSVData(Application.dataPath + "/Resources/Data/MemoryDataCSV.csv");
        const int type          = 1;
        const int explanationNo = 2;
        const int materialNo_0  = 3;
        const int materialNo_1  = 4;
        const int materialNo_2  = 5;

        //読み込んだデータを設定(0行目はタイトルなので飛ばすためにlineNo+1している）
        for (int lineNo = 0; lineNo < (int)MemoryType.Count; lineNo++)
        {
            Memory memory = new Memory();
            //種類
            memory.type = (MemoryType)int.Parse(dataBuffer[lineNo + 1][type]);
            //説明
            memory.explanation = dataBuffer[lineNo + 1][explanationNo];
            //素材
            memory.materialType[0] = (MemoryType)int.Parse(dataBuffer[lineNo + 1][materialNo_0]);
            memory.materialType[1] = (MemoryType)int.Parse(dataBuffer[lineNo + 1][materialNo_1]);
            memory.materialType[2] = (MemoryType)int.Parse(dataBuffer[lineNo + 1][materialNo_2]);
            dictionary[memory.type] = memory;
        }

        //todo:Debug.log消す
        for (int n = 0; n < (int)MemoryType.Count; n++)
        {
            Debug.Log(n);
            Debug.Log(dictionary[(MemoryType)n].type);
            Debug.Log(dictionary[(MemoryType)n].explanation);
            Debug.Log(dictionary[(MemoryType)n].materialType[0]);
            Debug.Log(dictionary[(MemoryType)n].materialType[1]);
            Debug.Log(dictionary[(MemoryType)n].materialType[2]);
        }

    }

    /// <summary>
    /// データを取得
    /// </summary>
    /// <param name="memoryType">種類</param>
    /// <returns>データ</returns>
    public Memory GetData(MemoryType memoryType)
    {
        return dictionary[memoryType];
    }
}
