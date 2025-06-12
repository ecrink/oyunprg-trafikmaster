using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadQuizScene()
    {
        SceneManager.LoadScene("QuizScene");
    }
}
