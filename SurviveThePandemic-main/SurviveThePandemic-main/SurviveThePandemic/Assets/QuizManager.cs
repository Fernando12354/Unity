using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    public Image questionImage; // Componente de imagen para mostrar la imagen de la pregunta
    public Text questionText; // Componente de texto para mostrar el texto de la pregunta
    public Button[] optionButtons; // Array de botones para las opciones de respuesta
    public Text scoreText; // Componente de texto para mostrar la puntuación
    public GameObject retryButton; // Botón para reintentar el cuestionario
    public GameObject continueButton; // Botón para continuar después del cuestionario

    private int currentQuestionIndex = 0; // Índice de la pregunta actual
    private int score = 0; // Puntuación del jugador

    [System.Serializable]
    public class Question
    {
        public Sprite image; // Imagen para la pregunta
        public string question; // Texto para la pregunta
        public string[] options; // Array de opciones de respuesta
        public int correctOptionIndex; // Índice de la opción correcta
    }

    public Question[] questions; // Array de preguntas

    void Start()
    {
        retryButton.SetActive(false); // Ocultar el botón de reintento inicialmente
        continueButton.SetActive(false); // Ocultar el botón de continuar inicialmente
        DisplayQuestion(questions[currentQuestionIndex]);
    }

    void DisplayQuestion(Question question)
    {
        questionImage.sprite = question.image; // Establecer la imagen de la pregunta
        questionText.text = question.question; // Establecer el texto de la pregunta
        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].GetComponentInChildren<Text>().text = question.options[i]; // Establecer el texto de la opción
            optionButtons[i].onClick.RemoveAllListeners(); // Eliminar oyentes anteriores
            int index = i; // Capturar el índice para el oyente
            optionButtons[i].onClick.AddListener(() => CheckAnswer(index)); // Añadir nuevo oyente
        }
    }

    void CheckAnswer(int index)
    {
        if (index == questions[currentQuestionIndex].correctOptionIndex)
        {
            score++; // Incrementar puntuación si la respuesta es correcta
        }

        currentQuestionIndex++; // Pasar a la siguiente pregunta

        if (currentQuestionIndex < questions.Length)
        {
            DisplayQuestion(questions[currentQuestionIndex]); // Mostrar la siguiente pregunta
        }
        else
        {
            DisplayScore(); // Mostrar la puntuación final
        }
    }

    void DisplayScore()
    {
        questionText.text = "Obtuviste una Puntuación de: " + score + "/" + questions.Length; // Mostrar la puntuación final

        questionImage.gameObject.SetActive(false); // Ocultar la imagen de la pregunta
        foreach (Button button in optionButtons)
        {
            button.gameObject.SetActive(false); // Ocultar los botones de opciones
        }

        if (score < 6)
        {
            retryButton.SetActive(true); // Mostrar el botón de reintento
            continueButton.SetActive(false); // Ocultar el botón de continuar
        }
        else
        {
            continueButton.SetActive(true); // Mostrar el botón de continuar
            retryButton.SetActive(true); // Ocultar el botón de reintento
        }
    }

    public void RetryQuiz()
    {
        score = 0; // Reiniciar la puntuación
        currentQuestionIndex = 0; // Reiniciar el índice de preguntas
        retryButton.SetActive(false); // Ocultar el botón de reintento
        continueButton.SetActive(false); // Ocultar el botón de continuar
        questionImage.gameObject.SetActive(true); // Mostrar la imagen de la pregunta
        foreach (Button button in optionButtons)
        {
            button.gameObject.SetActive(true); // Mostrar los botones de opciones
        }
        DisplayQuestion(questions[currentQuestionIndex]); // Mostrar la primera pregunta
    }
}
