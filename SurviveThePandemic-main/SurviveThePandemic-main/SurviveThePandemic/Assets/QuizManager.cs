using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    public Image questionImage; // Image component for displaying the question image
    public Text questionText; // Text component for displaying the question text
    public Button[] optionButtons; // Array of buttons for the answer options
    public Text scoreText; // Text component for displaying the score

    private int currentQuestionIndex = 0; // Index of the current question
    private int score = 0; // Player's score

    [System.Serializable]
    public class Question
    {
        public Sprite image; // Image for the question
        public string question; // Text for the question
        public string[] options; // Array of answer options
        public int correctOptionIndex; // Index of the correct option
    }

    public Question[] questions; // Array of questions

    void Start()
    {
        DisplayQuestion(questions[currentQuestionIndex]);
    }

    void DisplayQuestion(Question question)
    {
        questionImage.sprite = question.image; // Set the question image
        questionText.text = question.question; // Set the question text
        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].GetComponentInChildren<Text>().text = question.options[i]; // Set the option text
            optionButtons[i].onClick.RemoveAllListeners(); // Remove previous listeners
            int index = i; // Capture the index for the listener
            optionButtons[i].onClick.AddListener(() => CheckAnswer(index)); // Add new listener
        }
    }

    void CheckAnswer(int index)
    {
        if (index == questions[currentQuestionIndex].correctOptionIndex)
        {
            score++; // Increment score if the answer is correct
        }

        currentQuestionIndex++; // Move to the next question

        if (currentQuestionIndex < questions.Length)
        {
            DisplayQuestion(questions[currentQuestionIndex]); // Display the next question
        }
        else
        {
            DisplayScore(); // Display the final score
        }
    }

    void DisplayScore()
    {
        questionText.text = "Quiz Completed! Your Score: " + score + "/" + questions.Length; // Display final score
        questionImage.gameObject.SetActive(false); // Hide the question image
        foreach (Button button in optionButtons)
        {
            button.gameObject.SetActive(false); // Hide the option buttons
        }
    }
}
