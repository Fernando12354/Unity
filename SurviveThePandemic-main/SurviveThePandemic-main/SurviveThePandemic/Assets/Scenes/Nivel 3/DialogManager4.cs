using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;      // Para TextMeshProUGUI
using UnityEngine.UI; // Para Image
using UnityEngine.SceneManagement; // Para cargar escenas

public class DialogManager4 : MonoBehaviour
{
    [Header("Configuración de UI")]
    public TextMeshProUGUI dialogText;      // Texto del diálogo
    public Image characterImage;            // Imagen del personaje
    public GameObject dialogBox;            // Caja de diálogo que se muestra en la pantalla
    public GameObject escenario;            // Objeto de escenario
    public GameObject canvasMision;         // Canvas de misión

    [Header("Diálogos")]
    public Sprite[] characterImages = new Sprite[]
    {
        // Añade aquí tus imágenes de personajes
    };

    public string[] dialogTexts = new string[]
    {
        // Añade los diálogos aquí
    };

    [Header("Configuración de Escena")]
    public string escenaSiguiente;         // Nombre de la escena a cargar, editable desde el Inspector

    private Queue<string> sentences;        // Cola de frases
    private int currentDialogIndex = -1;    // Índice del diálogo actual
    private bool isDialogActive = false;    // Indica si el diálogo está activo

    private void Start()
    {
        sentences = new Queue<string>();
        dialogBox.SetActive(false);          // Asegúrate de que la caja de diálogo esté inicialmente oculta
        canvasMision.SetActive(false);       // El Canvas de misión también está oculto al inicio
    }

    public void StartDialog(int dialogIndex)
    {
        if (dialogIndex < 0 || dialogIndex >= dialogTexts.Length || dialogIndex >= characterImages.Length)
        {
            Debug.LogError("Índice de diálogo fuera de rango.");
            return;
        }

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

        // Ocultar el escenario y activar el Canvas de misión
        escenario.SetActive(false);
        canvasMision.SetActive(true);

        // Reanudar el juego
        Time.timeScale = 1f;
    }

    private void Update()
    {
        // Avanzar al siguiente diálogo cuando se presiona Enter
        if (isDialogActive && Input.GetKeyDown(KeyCode.Return))
        {
            DisplayNextSentence();
        }

        // Cargar la siguiente escena al presionar Enter cuando el Canvas de misión esté activo
        if (!isDialogActive && canvasMision.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(escenaSiguiente);
        }
    }
}
