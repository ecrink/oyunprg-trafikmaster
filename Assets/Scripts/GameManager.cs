using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 100;
    public int correctCount = 0;
    public int wrongCount = 0;
    public int currentQuestionIndex = 0;

    public string currentQuestion = "Varsay�lan Soru";
    public int correctAnswerIndex = 0;

    public List<QuestionData> testQuestions = new List<QuestionData>();

    [SerializeField] private Text scoreText;
    [SerializeField] private Text correctText;
    [SerializeField] private Text wrongText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeQuestions();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadNextQuestion();
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        correctCount++;
        UpdateUI();
    }

    public void ReduceScore(int amount)
    {
        score = Mathf.Max(0, score - amount);
        wrongCount++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Skor: " + score;

        if (correctText != null)
            correctText.text = "Do�ru: " + correctCount;

        if (wrongText != null)
            wrongText.text = "Yanl��: " + wrongCount;
    }

    public void LoadNextQuestion()
    {
        if (currentQuestionIndex < testQuestions.Count)
        {
            currentQuestion = testQuestions[currentQuestionIndex].questionText;
            correctAnswerIndex = testQuestions[currentQuestionIndex].correctAnswerIndex;
        }
    }

    public void GoToNextQuestionOrScene()
    {
        currentQuestionIndex++;

        if (currentQuestionIndex < testQuestions.Count)
        {
            LoadNextQuestion();
            SceneManager.LoadScene("QuizScene");
        }
        else
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    public void ResetData()
    {
        score = 100;
        correctCount = 0;
        wrongCount = 0;
        currentQuestionIndex = 0;
        LoadNextQuestion();
    }

    private void InitializeQuestions()
    {
        testQuestions = new List<QuestionData>
        {
            new QuestionData("�lk yard�mda ABC, hava yolu, solunum ve dola��m� temsil eder. Do�ru mu?", 0),
            new QuestionData("Bu levha yayalara �ncelik verilmesini mi s�yler? (Yaya ge�idi)", 0),
            new QuestionData("Sar� ���kta durmak zorunludur. Do�ru mu?", 0),
            new QuestionData("Motor so�ukken ya� seviyesi kontrol edilmelidir. Do�ru mu?", 0),
            new QuestionData("Sar� ���k yand���nda h�z art�r�lmal�d�r. Do�ru mu?", 1),
            new QuestionData("Bilinci kapal� bir kazazede yan yat�r�larak g�venli pozisyona al�nabilir. Do�ru mu?", 0),
            new QuestionData("Burun kanamas�nda hastan�n ba�� geriye yaslanmal�d�r. Do�ru mu?", 1),
            new QuestionData("�oka giren ki�iye s�cak tutulacak �ekilde m�dahale edilmelidir. Do�ru mu?", 0),
            new QuestionData("K�r�k tespitinde hareketsiz hale getirme �nemlidir. Do�ru mu?", 0),
            new QuestionData("Motor ya�� seviyesi, motor s�cakken �l��lmelidir. Do�ru mu?", 1),
            new QuestionData("Arac�n radyat�r�nde su eksikse motor hararet yapabilir. Do�ru mu?", 0),
            new QuestionData("Ak� kutuplar� ters ba�lanabilir. Do�ru mu?", 1),
            new QuestionData("Vantilat�r kay��� koparsa �arj sistemi �al��maz. Do�ru mu?", 0)
        };
    }
}

[System.Serializable]
public class QuestionData
{
    public string questionText;
    public int correctAnswerIndex;

    public QuestionData(string questionText, int correctAnswerIndex)
    {
        this.questionText = questionText;
        this.correctAnswerIndex = correctAnswerIndex;
    }
}
