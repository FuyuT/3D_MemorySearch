using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;//情報を読み込むStringReaderを使用するために導入

namespace MyUtil
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
            using (StreamReader reader = new StreamReader(dataPath))
            {
                while (reader.Peek() != -1)
                {
                    string line = reader.ReadLine();
                    dataBuffer.Add(line.Split(','));
                }
            }

            return dataBuffer;
        }

        /// <summary>
        /// cscデータの読み込みを行い、バッファを返す
        /// エンコードはしない
        /// </summary>
        /// <param name="csvFile">csv形式のテキスト</param>
        /// <returns>読み込んだデータのバッファ</returns>
        static public List<string[]> ReadCSVData(TextAsset csvFile)
        {
            //指定したアドレスに保管されているCSVファイルから情報を読み取り、enemyDataに情報を文字列として格納するメソッド。
            //enemyData[i][j]はCSVファイルのi行、j列目のデータを表す。但し先頭行（タイトル部分）は0行目と考えるものとする。
            List<string[]> dataBuffer = new List<string[]>();
            using(StringReader reader = new StringReader(csvFile.text))
            {
                while (reader.Peek() != -1)
                {
                    string line = reader.ReadLine();
                    dataBuffer.Add(line.Split(','));
                }
            }

            return dataBuffer;
        }

        /// <summary>
        /// 指定したパスにテキストデータを書き込む
        /// </summary>
        /// <param name="path">書き込むファイルのパス</param>
        /// <param name="writeData">書き込む内容</param>
        static public void WriteText(string path, List<string> writeData)
        {
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
            {
                foreach (string text in writeData)
                {
                    sw.Write(text);
                }
            }
        }
    }
}