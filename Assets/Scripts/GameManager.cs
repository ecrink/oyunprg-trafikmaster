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

    public string currentQuestion = "Varsayýlan Soru";
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
            correctText.text = "Doðru: " + correctCount;

        if (wrongText != null)
            wrongText.text = "Yanlýþ: " + wrongCount;
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
            new QuestionData("Ýlk yardýmda ABC, hava yolu, solunum ve dolaþýmý temsil eder. Doðru mu?", 0),
            new QuestionData("Bu levha yayalara öncelik verilmesini mi söyler? (Yaya geçidi)", 0),
            new QuestionData("Sarý ýþýkta durmak zorunludur. Doðru mu?", 0),
            new QuestionData("Motor soðukken yað seviyesi kontrol edilmelidir. Doðru mu?", 0),
            new QuestionData("Sarý ýþýk yandýðýnda hýz artýrýlmalýdýr. Doðru mu?", 1),
            new QuestionData("Bilinci kapalý bir kazazede yan yatýrýlarak güvenli pozisyona alýnabilir. Doðru mu?", 0),
            new QuestionData("Burun kanamasýnda hastanýn baþý geriye yaslanmalýdýr. Doðru mu?", 1),
            new QuestionData("Þoka giren kiþiye sýcak tutulacak þekilde müdahale edilmelidir. Doðru mu?", 0),
            new QuestionData("Kýrýk tespitinde hareketsiz hale getirme önemlidir. Doðru mu?", 0),
            new QuestionData("Motor yaðý seviyesi, motor sýcakken ölçülmelidir. Doðru mu?", 1),
            new QuestionData("Aracýn radyatöründe su eksikse motor hararet yapabilir. Doðru mu?", 0),
            new QuestionData("Akü kutuplarý ters baðlanabilir. Doðru mu?", 1),
            new QuestionData("Vantilatör kayýþý koparsa þarj sistemi çalýþmaz. Doðru mu?", 0)
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
