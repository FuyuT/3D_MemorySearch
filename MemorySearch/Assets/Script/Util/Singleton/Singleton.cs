using UnityEngine;

namespace MyUtil
{
    public abstract class Singleton<T> where T : class, new()
    {
        /*******************************
        * private
        *******************************/
        static T instance;

        //ロックのためのインスタンス
        static object syncObj = new object();

        static void Create()
        {
            instance = new T();
        }
        /*******************************
        * public
        *******************************/
        public static T Instance()
        {
            //デッドロックが起きないようにロック
            lock (syncObj)
            {
                if (instance == null)
                {
                    Create();
                }
            }

            return instance;
        }
    }
}