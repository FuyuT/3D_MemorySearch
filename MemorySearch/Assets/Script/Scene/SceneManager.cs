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

    //�Q�[���I�[�o�[�p�l��
    [SerializeField]
    GameObject GameOverImage;

    //SE�֘A///////////////
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
      

    //�Q�[���I�[�o�[���m�F���āA�Q�[���I�[�o�[�Ȃ玟�̃V�[�����Q�[���I�[�o�[�ɐݒ�
    void CheckGameOver()
    {
        //�v���C���[�̎��̂��Ȃ���ΏI��
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
        Application.Quit();//�Q�[���v���C�I��
#endif               
    }
}
