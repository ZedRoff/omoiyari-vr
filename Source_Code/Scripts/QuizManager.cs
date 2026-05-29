using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class QuizQuestion
    {
        public string question;
        public string[] answers; // 4 réponses
        public int correctIndex; // index de la bonne réponse
    }

    public QuizQuestion[] questions;
    private int currentQuestionIndex;

    public GameObject quizPanel;
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;

    void Start()
    {
        quizPanel.SetActive(false);
    }
    public string GetCode()
    {
        return "ab26";
    }
    public void StartQuiz()
    {
        currentQuestionIndex = 0;
        quizPanel.SetActive(true);
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        QuizQuestion q = questions[currentQuestionIndex];
        questionText.text = q.question;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i; // capture locale pour le listener
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = q.answers[i];
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => Answer(index));
        }
    }

    void Answer(int index)
    {
        if (index == questions[currentQuestionIndex].correctIndex)
        {
            currentQuestionIndex++;
            if (currentQuestionIndex < questions.Length)
                DisplayQuestion();
            else
                EndQuiz();
        }
        else
        {
            // TODO : afficher "mauvaise réponse", rejouer, etc.
            Debug.Log("Mauvaise réponse !");
        }
    }

    void EndQuiz()
    {
        quizPanel.SetActive(false);
        Debug.Log("Quiz terminé ! Le virus est éliminé.");
    }
}
