using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class TestPlayer
{
    public MemoryTypeEnum[] possesionMemory;
}

public class DataManager : MonoBehaviour
{
    [SerializeField] InventoryButtonManager inventory;

    Dictionary<int, Memory> memoryData;

    TestPlayer player;

    MemoryTypeEnum[] combineMaterial;

    [SerializeField] Sprite[] ui;

    [SerializeField] Image[] materialImage; 


    public TextAsset textAsset;
    void Start()
    {
        memoryData = new Dictionary<int, Memory>();

        TestPlayer player = new TestPlayer();
        player.possesionMemory = new MemoryTypeEnum[5];

        combineMaterial = new MemoryTypeEnum[2];

        JsonLoad();

        //for (int n = 0; n < memoryData.Count; n++)
        //{
        //    Debug.Log(memoryData[n].memoryName);
        //    Debug.Log(memoryData[n].material);
        //    Debug.Log(memoryData[n].explanation);
        //}
    }

    //json読み込み
    void JsonLoad()
    {
        string jsonText = textAsset.ToString();
        JsonNode json = JsonNode.Parse(jsonText);

        int n = 0;
        foreach (JsonNode note in json["Memories"])
        {
            Memory data = new Memory();
            data.memoryName = note["name"].Get<string>();
            data.material = note["material"].Get<string>();
            data.explanation = note["explanation"].Get<string>();

            memoryData.Add(n, data);
            n++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CombineMemory();
    }

    void CombineMemory()
    {
        for (int n = 0; n < combineMaterial.Length; n++)
        {
            if (combineMaterial[n] == MemoryTypeEnum.None)
            {
                return;
            }
        }

        //素材が揃っていれば、合成後の素材を表示
        var c = materialImage[2].color;
        materialImage[2].color = new Color(c.r, c.g, c.b, 255.0f);
    }

    /// setter
    //マテリアルを設定
    public void SetMaterial(MemoryTypeEnum materialMemory)
    {
        //for(int n = 0; n < combineMaterial.Length; n++)
        //{
        //    if(combineMaterial[n] == MemoryType.None)
        //    {
        //        combineMaterial[n] = materialMemory;
        //        var c = materialImage[n].color;
        //        materialImage[n].color = new Color(c.r, c.g, c.b, 255.0f);
        //        return;
        //    }
        //}
    }
}
