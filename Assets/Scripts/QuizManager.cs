using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    [Header("Soru Alanı")]
    [SerializeField] private Text questionText;
    [SerializeField] private Text correctText;
    [SerializeField] private Text wrongText;
    [SerializeField] private GameObject questionUI;

    [Header("Sonuç Paneli")]
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private Text resultSkorText;
    [SerializeField] private Text resultCorrectText;
    [SerializeField] private Text resultWrongText;
    [SerializeField] private Text resultNameText;

    private bool questionAnswered = false;

    private void Start()
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager instance bulunamadı!");
            return;
        }

        // ❗ Verileri baştan sıfırla (sadece test sahnesinde çalıştığı için güvenlidir)
        GameManager.instance.ResetData();

        GameManager.instance.LoadNextQuestion(); // İlk soruyu yükle
        UpdateQuestionUI();
        resultPanel.SetActive(false);
        questionAnswered = false;
    }

    public void OnAnswerSelected(int selectedIndex)
    {
        if (questionAnswered) return;
        questionAnswered = true;

        if (GameManager.instance == null) return;

        if (selectedIndex == GameManager.instance.correctAnswerIndex)
            GameManager.instance.AddScore(20);
        else
            GameManager.instance.ReduceScore(10);

        UpdateScoreUI();
        StartCoroutine(WaitAndCheckNext(1.2f));
    }

    private IEnumerator WaitAndCheckNext(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameManager.instance.currentQuestionIndex++;

        if (GameManager.instance.currentQuestionIndex < GameManager.instance.testQuestions.Count)
        {
            GameManager.instance.LoadNextQuestion();
            UpdateQuestionUI();
            questionAnswered = false;
        }
        else
        {
            ShowResultScreen();
        }
    }

    private void UpdateQuestionUI()
    {
        questionText.text = GameManager.instance.currentQuestion;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        correctText.text = "Doğru: " + GameManager.instance.correctCount;
        wrongText.text = "Yanlış: " + GameManager.instance.wrongCount;
    }

    private void ShowResultScreen()
    {
        questionUI.SetActive(false);
        resultPanel.SetActive(true);

        resultSkorText.text = "Skor: " + GameManager.instance.score;
        resultCorrectText.text = "Doğru: " + GameManager.instance.correctCount;
        resultWrongText.text = "Yanlış: " + GameManager.instance.wrongCount;

        string name = PlayerPrefs.GetString("PlayerName", "Sürücü");
        resultNameText.text = "Ad Soyad: " + name;
    }

    public void OnRetry()
    {
        SceneManager.LoadScene("QuizScene");
    }

    public void OnSimulate()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
