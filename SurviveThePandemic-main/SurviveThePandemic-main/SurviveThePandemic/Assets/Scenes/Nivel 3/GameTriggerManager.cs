using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameTriggerManager : MonoBehaviour
{
    [Header("Objetos de la escena")]
    public GameObject escenario;  // Objeto del escenario inicial
    public GameObject juego;      // Objeto que contiene el juego con los canvas y temporizador
    public ObjectiveController objectiveController; // Referencia al ObjectiveController
    public string objectiveName; // Nombre del objetivo que se debe completar

    private GameTimer gameTimer;  // Referencia al script GameTimer

    [Header("Configuración de diálogos")]
    public TextMeshProUGUI dialogText;
    public Image characterImage;
    public GameObject dialogBox;
    public GameObject decisionCanvas;
    public Button readyButton;
    public Button notReadyButton;

    [Header("Diálogos")]
    public Sprite[] characterImages = new Sprite[] { };
    public string[] dialogTexts = new string[] { };

    private Queue<string> sentences;
    private int currentDialogIndex = -1;
    private bool isDialogActive = false;

    private void Start()
    {
        // Inicializar componentes y referencias
        sentences = new Queue<string>();
        dialogBox.SetActive(false);
        decisionCanvas.SetActive(false);

       

        readyButton.onClick.AddListener(OnReadyButtonClicked);
        notReadyButton.onClick.AddListener(OnNotReadyButtonClicked);

        gameTimer = juego.GetComponent<GameTimer>();
        juego.SetActive(false); // Asegurarse de que el juego esté desactivado al inicio
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verificar que el trigger lo active el jugador
        {
            // Marcar el objetivo como completado
            if (objectiveController != null && !string.IsNullOrEmpty(objectiveName))
            {
                objectiveController.CompleteObjective(objectiveName);
            }

            // Iniciar el flujo de diálogos
            StartDialog(0); // Comienza con el primer diálogo
        }
    }

    public void StartDialog(int dialogIndex)
    {
        if (dialogIndex < 0 || dialogIndex >= dialogTexts.Length || dialogIndex >= characterImages.Length)
        {
            Debug.LogError("Índice de diálogo fuera de rango.");
            return;
        }

       

        dialogBox.SetActive(true);
        dialogText.gameObject.SetActive(true);
        characterImage.gameObject.SetActive(true);

        Time.timeScale = 0f;

        characterImage.sprite = characterImages[dialogIndex];
        dialogText.text = "";

        sentences.Clear();
        sentences.Enqueue(dialogTexts[dialogIndex]);

        DisplayNextSentence();

        isDialogActive = true;
        currentDialogIndex = dialogIndex;
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            MoveToNextDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogText.text = sentence;
    }

    private void MoveToNextDialog()
    {
        if (currentDialogIndex < dialogTexts.Length - 1)
        {
            currentDialogIndex++;
            StartDialog(currentDialogIndex);
        }
        else
        {
            EndDialog();
        }
    }

    public void EndDialog()
    {
        if (currentDialogIndex == dialogTexts.Length - 1)
        {
            dialogBox.SetActive(false);
            decisionCanvas.SetActive(true);
        }
        else
        {
            dialogBox.SetActive(false);
            isDialogActive = false;

            Time.timeScale = 1f;

           
        }
    }

    private void Update()
    {
        if (isDialogActive && Input.GetKeyDown(KeyCode.Return))
        {
            DisplayNextSentence();
        }
    }

    // Métodos para los botones del canvas de decisiones
    private void OnReadyButtonClicked()
    {
        decisionCanvas.SetActive(false); // Oculta el canvas de decisión
        Time.timeScale = 1f;

        // Activar el juego y desactivar el escenario
        ActivarJuego();
    }

    private void OnNotReadyButtonClicked()
    {
        decisionCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    // Activar el juego después de los diálogos
    private void ActivarJuego()
    {
        escenario.SetActive(false); // Desactiva el escenario inicial
        juego.SetActive(true);      // Activa el objeto del juego

        if (gameTimer != null)
        {
            gameTimer.gameEnded = false; // Reinicia el temporizador si es necesario
        }
    }
}
