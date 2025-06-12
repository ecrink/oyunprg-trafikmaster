using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader1 : MonoBehaviour
{
    public AudioSource clickSound;

    public void LoadMainMenu()
    {
        if (clickSound != null)
            clickSound.Play();

        StartCoroutine(LoadWithDelay("MainMenuScene"));
    }

    private IEnumerator LoadWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }
}

