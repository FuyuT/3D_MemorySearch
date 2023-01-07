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
        GameOverImage.SetActive(true);
    }


    /*******************************
    * public
    *******************************/

    public static void ToTitle()
    {
        FadeManager.Instance.LoadScene("Titel", 1.0f);
    }

    public static void ToGame()
    {
        FadeManager.Instance.LoadScene("f_Game", 1.0f);
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
