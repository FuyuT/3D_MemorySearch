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
    private void Awake()
    {
        GameOverImage.SetActive(false);
    }

    void Update()
    {
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
        GameOverImage.SetActive(true);
        SoundManager.instance.StopBgm(BGM);
        SoundManager.instance.PlaySe(GameOverSE);
        Cursor.visible = true;

    }


    /*******************************
    * public
    *******************************/

    public static void ToTitle()
    {
        Time.timeScale = 1.0f;
        FadeManager.Instance.LoadScene("Titel", 1.0f);
    }

    public static void ToGame()
    {
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
