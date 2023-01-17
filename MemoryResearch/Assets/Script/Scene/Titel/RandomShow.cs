using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomShow : MonoBehaviour
{
    [SerializeField]
    GameObject img;

    Image[] ShowImg;
    // Start is called before the first frame update
    void Start()
    {
        ShowImg = new Image[img.transform.childCount];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < img.transform.childCount; i++)
        {
            // プレハブの位置をランダムで設定
            float y = Random.Range(-276, 564);
        
            Vector3 pos = new Vector3(0, y, 0);

            //ShowImg[i].transform.position = pos;
        }
    }
}
