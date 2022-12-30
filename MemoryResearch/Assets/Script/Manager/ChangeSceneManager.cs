using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum SceneType
{
    None,
    Title,
    Game,
    GameOver,
    End,
}

public class ChangeSceneManager : MonoBehaviour
{
    SceneType nowScene;
    SceneType nextScene;

    IReadPlayer player;
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
        FadeManager.Instance.LoadScene("Titel", 1.0f);
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

        try
        {
             player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }
        catch
        {

        }
    }

    void Update()
    {
        CheckGameOver();

        SceneUpdate();
    }

    //�Q�[���I�[�o�[���m�F���āA�Q�[���I�[�o�[�Ȃ玟�̃V�[�����Q�[���I�[�o�[�ɐݒ�
    void CheckGameOver()
    {
        if (!player.IsDead()) return;
        nextScene = SceneType.GameOver;
    }

    //�V�[���̍X�V
    void SceneUpdate()
    {
        if (nowScene == nextScene) return;

        switch (nextScene)
        {
            case SceneType.Title:

                break;
            case SceneType.Game:

                break;
            case SceneType.GameOver:
                break;

            case SceneType.End:
                //UnityEditor.EditorApplication.isPlaying = false;
                break;
        }

        nowScene = nextScene;
    }
}
