using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChangerScript : MonoBehaviour {

    /*private AudioSource myAudioSource;
    public AudioClip myAudioClip;*/

	// Use this for initialization
	void Start () {
        //myAudioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoToPlayScene()
    {
        SceneManager.LoadScene(1);
        //StartCoroutine(DelaySceneLoad(2));
    }

    public void GoToCreditsScene()
    {
        SceneManager.LoadScene(3);
        //StartCoroutine(DelaySceneLoad(3));
    }

    public void GoToHowToPlayScene()
    {
        SceneManager.LoadScene(1);
        //StartCoroutine(DelaySceneLoad(1));
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //si le damos al botón de Quit en Unity, parará de jugar
#else
        Application.Quit();
#endif
    }

    public void GoBackToMenu()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        SceneManager.LoadScene(0);
        //StartCoroutine(DelaySceneLoad(0));
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    /*public void PlaySoundOnClick()
    {
        myAudioSource.PlayOneShot(myAudioClip);
    }*/

    IEnumerator DelaySceneLoad(int sceneNum)
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(sceneNum);
    }
}
