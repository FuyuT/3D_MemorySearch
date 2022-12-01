using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneType
{
    None,
    Title,
    Game,
    End,
}

public class ChangeSceneManager : MonoBehaviour
{
    SceneType nowScene;
    SceneType nextScene;

    //////////////////////////////
    /// setter
    //////////////////////////////
    
    public void SetNextScene(SceneType sceneType)
    {
        nextScene = sceneType;
    }

    public void ToTitle()
    {
        nextScene = SceneType.Title;
    }

    public void ToGame()
    {
        nextScene = SceneType.Game;
    }

    public void ToEnd()
    {
        nextScene = SceneType.End;
    }

    //////////////////////////////
    /// main
    //////////////////////////////
    private void Awake()
    {
        nowScene = SceneType.Title;
        nextScene = SceneType.Title;
    }

    void Update()
    {
        if (nowScene == nextScene) return;

        switch(nextScene)
        {
            case SceneType.Title:

                break;
            case SceneType.Game:
                break;
            case SceneType.End:
                UnityEditor.EditorApplication.isPlaying = false;
                break;
        }

        nowScene = nextScene;
    }
}
