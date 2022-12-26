using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;//情報を読み込むStringReaderを使用するために導入

namespace Util
{
    public class TextUtility
    {
        /// <summary>
        /// TextAssetをJsonNode形式に変換して返す
        /// </summary>
        /// <param name="data">テキストデータ</param>
        /// <returns>JsonNode形式のデータ</returns>
        static public T ParseToJson<T>(TextAsset data)
        {
            return JsonUtility.FromJson<T>(data.ToString());
        }


        /// <summary>
        /// cscデータの読み込みを行い、バッファを返す
        /// エンコード形式は、shift_jis
        /// </summary>
        /// <param name="dataPath">ファイルのパス</param>
        /// <returns>読み込んだデータのバッファ</returns>
        static public List<string[]> ReadCSVData(string dataPath)
        {
            //指定したアドレスに保管されているCSVファイルから情報を読み取り、enemyDataに情報を文字列として格納するメソッド。
            //enemyData[i][j]はCSVファイルのi行、j列目のデータを表す。但し先頭行（タイトル部分）は0行目と考えるものとする。
            List<string[]> dataBuffer = new List<string[]>();

            StreamReader reader = new StreamReader(dataPath,
                System.Text.Encoding.GetEncoding("shift_jis"));
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                dataBuffer.Add(line.Split(','));
            }

            return dataBuffer;
        }

    }
}