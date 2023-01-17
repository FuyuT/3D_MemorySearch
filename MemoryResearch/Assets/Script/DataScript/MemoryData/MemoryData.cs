using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;//情報を読み込むStringReaderを使用するために導入

[System.Serializable]
public class MemoryData : IMemoryData
{
    /*******************************
    * private
    *******************************/
    [SerializeField] TextAsset memoryDataTxt;
    Dictionary<MemoryType, Memory> dictionary;
    Dictionary<MemoryType, bool>   isCombineMemory; //合成用のメモリかどうか判断するための配列
    /*******************************
    * public 
    *******************************/

    public MemoryData()
    {
        dictionary = new Dictionary<MemoryType, Memory>();
        isCombineMemory = new Dictionary<MemoryType, bool>();
    }

    /// <summary>
    /// メモリデータを読み込む
    /// </summary>
    /// <param name="dataText">メモリデータを纏めたテキスト</param>
    public void Load(TextAsset memoryDataTxt)
    {
        //データをバッファに読み込み
        List<string[]> dataBuffer = MyUtil.TextUtility.ReadCSVData(memoryDataTxt);
        const int Type          = 1;
        const int ExplanationNo = 2;
        const int MaterialNo_0  = 3;
        const int MaterialNo_1  = 4;
        const int MaterialNo_2  = 5;
        const int Combine_Cost  = 6;

        //読み込んだデータを設定
        //0行目はタイトルなので飛ばすためにlineNoを1で初期化している
        //0行目をカウントしない分MemoryType.Countに+1してメモリを全て設定している
        for (int lineNo = 1; lineNo < (int)MemoryType.Count + 1; lineNo++)
        {
            Memory memory = new Memory();
            //種類
            memory.type = (MemoryType)int.Parse(dataBuffer[lineNo][Type]);
            //説明
            memory.explanation = dataBuffer[lineNo][ExplanationNo];
            //素材
            memory.materialType[0] = (MemoryType)int.Parse(dataBuffer[lineNo][MaterialNo_0]);
            memory.materialType[1] = (MemoryType)int.Parse(dataBuffer[lineNo][MaterialNo_1]);
            memory.materialType[2] = (MemoryType)int.Parse(dataBuffer[lineNo][MaterialNo_2]);
            memory.combineCost     = float.Parse(dataBuffer[lineNo][Combine_Cost]);
            //合成メモリか判断する
            isCombineMemory[memory.type] = false;
            for (int mat = 0; mat < Global.MemoryMaterialMax; mat++)
            {
                if(memory.materialType[mat] != MemoryType.None)
                {
                    isCombineMemory[memory.type] = true;
                    break;
                }
            }

            //メモリを格納
            dictionary[memory.type] = memory;
        }
    }

    /// <summary>
    /// 素材を元に合成されるメモリを検索し、返す
    /// </summary>
    /// <param name="material">素材配列</param>
    /// <returns>検索結果を返す、存在しなければ MemoryType.None を返す</returns>
    public MemoryType FindCombineMemory(MemoryType[] material)
    {
        bool[] isMatch = new bool[Global.MemoryMaterialMax] { false,false,false };
        //全てのメモリから検索(1から始めるのは、何もないメモリを表すNoneが0番だから）
        for(MemoryType type = (MemoryType)1; type < MemoryType.Count; type++)
        {
            //合成メモリ以外
            try
            {
                if (!isCombineMemory[type]) continue;
            }
            catch
            {
                continue;
            }
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
                return dictionary[type].type;
            }
        }
        //存在しなければMemoryType.Noneを返す
        return MemoryType.None;
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
