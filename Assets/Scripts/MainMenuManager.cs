using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    public InputField nameInput;
    public AudioSource clickSound;
    public Text welcomeText; // Hoş geldin yazısı için eklendi

    public void StartTest()
    {
        if (nameInput.text.Length < 1)
        {
            Debug.Log("Lütfen adınızı girin!");
            return;
        }

        PlayerPrefs.SetString("PlayerName", nameInput.text);
        PlayerPrefs.Save();

        if (welcomeText != null)
        {
            welcomeText.text = "Hoş geldin, " + nameInput.text + "!";
        }

        clickSound.Play();
        StartCoroutine(LoadSceneWithDelay("QuizScene"));
    }

    public void StartSimulation()
    {
        if (nameInput.text.Length < 1)
        {
            Debug.Log("Lütfen adınızı girin!");
            return;
        }

        PlayerPrefs.SetString("PlayerName", nameInput.text);
        PlayerPrefs.Save();

        if (welcomeText != null)
        {
            welcomeText.text = "Hoş geldin, " + nameInput.text + "!";
        }

        clickSound.Play();
        StartCoroutine(LoadSceneWithDelay("MainScene")); // Simülasyon sahne adı burası
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Oyun kapatılıyor...");
    }

    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(0.5f); // Sesin biraz çalmasına izin ver
        SceneManager.LoadScene(sceneName);
    }
}


