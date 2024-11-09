using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CambioNV5 : MonoBehaviour
{
    [Header("Configuración de UI")]
    public TextMeshProUGUI dialogText;         // Texto del diálogo
    public Image characterImage;               // Imagen del personaje
    public GameObject dialogBox;               // Caja de diálogo
    public GameObject healthBarCanvas;         // Referencia al Canvas de la barra de vida
    public GameObject guia;                    // Referencia al waypoint

    [Header("Diálogos")]
    public Sprite[] characterImages = new Sprite[] {
        // Añade aquí tus imágenes de personajes
    };
    public string[] dialogTexts = new string[] {
        // Añade los diálogos aquí
    };

    [Header("Configuración de escena")]
    public string nextSceneName;               // Nombre de la escena a cargar después de los diálogos

    [Header("Temporizador")]
    public TextMeshProUGUI timerText;          // Texto del temporizador
    public float timeLimit = 60f;              // Límite de tiempo en segundos
    public GameObject defeatCanvas;            // Canvas de derrota que se muestra cuando se acaba el tiempo
    public GameObject deathCanvas;             // Canvas de muerte del personaje

    private float timeRemaining;
    private bool isTimerActive = true;
    private Queue<string> sentences;           // Cola de frases
    private int currentDialogIndex = -1;       // Índice del diálogo actual
    private bool isDialogActive = false;       // Indica si el diálogo está activo

    private void Start()
    {
        sentences = new Queue<string>();
        dialogBox.SetActive(false);             // Oculta la caja de diálogo inicialmente
        if (healthBarCanvas != null)
        {
            healthBarCanvas.SetActive(true);    // Activa la barra de vida al inicio
        }

        timeRemaining = timeLimit;              // Inicializa el tiempo restante
        defeatCanvas.SetActive(false);          // Asegura que el canvas de derrota esté oculto al inicio
        deathCanvas.SetActive(false);           // Asegura que el canvas de muerte esté oculto al inicio
    }

    private void Update()
    {
        // Avanzar al siguiente diálogo cuando se presiona Enter
        if (isDialogActive && Input.GetKeyDown(KeyCode.Return))
        {
            DisplayNextSentence();
        }

        // Control del temporizador
        if (isTimerActive && !isDialogActive && !deathCanvas.activeSelf)
        {
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = "" + Mathf.Ceil(timeRemaining).ToString();
        }
        else
        {
            timeRemaining = 0;
            isTimerActive = false;
            ShowDefeatCanvas();
        }
    }

    public void StartDialog(int dialogIndex)
    {
        if (dialogIndex < 0 || dialogIndex >= dialogTexts.Length || dialogIndex >= characterImages.Length)
        {
            Debug.LogError("Índice de diálogo fuera de rango.");
            return;
        }

        guia.SetActive(false);

        // Ocultar el canvas de la barra de vida y detener el temporizador
        if (healthBarCanvas != null) healthBarCanvas.SetActive(false);
        isTimerActive = false;

        // Activar la caja de diálogo y los componentes de UI
        dialogBox.SetActive(true);
        dialogText.gameObject.SetActive(true);
        characterImage.gameObject.SetActive(true);

        // Pausar el juego
        Time.timeScale = 0f;

        // Asignar la imagen del personaje y el texto del diálogo
        characterImage.sprite = characterImages[dialogIndex];
        dialogText.text = ""; // Limpiar el texto al inicio del diálogo

        // Preparar las frases del diálogo
        sentences.Clear();
        sentences.Enqueue(dialogTexts[dialogIndex]);

        // Mostrar la primera frase del diálogo
        DisplayNextSentence();

        // Marcar el diálogo como activo
        isDialogActive = true;
        currentDialogIndex = dialogIndex; // Guardar el índice del diálogo actual
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            MoveToNextDialog();
            return;
        }

        // Mostrar la siguiente frase
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
        // Ocultar la caja de diálogo y marcar el diálogo como inactivo
        dialogBox.SetActive(false);
        isDialogActive = false;

        // Reanudar el juego y el temporizador
        Time.timeScale = 1f;
        isTimerActive = false;  // Reactivar temporizador solo si el canvas de muerte no está activo

        // Cargar la siguiente escena si el nombre está configurado
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("No se ha asignado un nombre de escena para cargar.");
        }
    }

    private void ShowDefeatCanvas()
    {
        // Muestra el canvas de derrota y pausa el juego
        guia.SetActive(false);
        defeatCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowDeathCanvas()
    {
        // Muestra el canvas de muerte, detiene el temporizador y pausa el juego
        guia.SetActive(false);
        deathCanvas.SetActive(true);
        isTimerActive = false;
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        // Recarga la escena actual
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
