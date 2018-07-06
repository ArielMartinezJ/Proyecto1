using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour {

    private static SceneManagerScript mySceneInstance;

    public static SceneManagerScript Instance
    {
        get
        {
            return mySceneInstance;
        }
    }

    private void Awake()
    {
        if (mySceneInstance != null || mySceneInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        mySceneInstance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void GoToPlayScene()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToHowToPlayScene()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToCreditsScene()
    {
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //si le damos al botón de Quit en Unity, parará de jugar
#else
        Application.Quit();
#endif
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GoBackToMenu()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        SceneManager.LoadScene(0);
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
