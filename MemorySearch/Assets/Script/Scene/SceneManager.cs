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

public class SceneManager : MonoBehaviour
{
    /*******************************
    * private
    *******************************/
    SceneType nowScene;
    SceneType nextScene;

    //ゲームオーバーパネル
    [SerializeField]
    GameObject GameOverImage;

    //SE関連///////////////
    [SerializeField] AudioClip GameOverSE;
    [SerializeField] AudioClip BGM;
    //////////////////////////////
    /// main
    //////////////////////////////
    ///

    bool IsGameOver;

    private void Awake()
    {
        ToClear();
        IsGameOver = false;
    }

    void Update()
    {
        if (nowScene == SceneType.Title) return;
        
        CheckGameOver();
    }
      

    //ゲームオーバーか確認して、ゲームオーバーなら次のシーンをゲームオーバーに設定
    void CheckGameOver()
    {
        //プレイヤーの実体がなければ終了
        if (Player.readPlayer == null) return;

        if (Player.readPlayer.IsEndDeadMotion())
        {
            
            ToGameOver();
        }
    }

    private void ToGameOver()
    {
        if (!IsGameOver)
        {
            GameOverImage.SetActive(true);
            SoundManager.instance.StopBgm();
            SoundManager.instance.PlaySe(GameOverSE, Player.readPlayer.GetPos());
            Cursor.visible = true;
            IsGameOver = true;
            Time.timeScale = 0;
        }
    }


    /*******************************
    * public
    *******************************/

    public static void ToTitle()
    {
        Time.timeScale = 1.0f;
        FadeManager.Instance.LoadScene("Titel", 1.0f);
        SoundManager.instance.StopBgm();
    }

    public static void ToGame()
    {
        Time.timeScale = 1.0f;
        FadeManager.Instance.LoadScene("Game", 1.0f);
    }

    public static void ToClear()
    {
        FadeManager.Instance.LoadScene("Gameclear", 1.0f);
    }

    public static void ToEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();//ゲームプレイ終了
#endif               
    }
}
