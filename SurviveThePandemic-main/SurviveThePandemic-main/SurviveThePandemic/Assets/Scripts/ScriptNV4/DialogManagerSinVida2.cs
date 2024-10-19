using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Para TextMeshProUGUI
using UnityEngine.UI; // Para Image

public class DialogManagerSinVida2 : MonoBehaviour
{
    [Header("Configuración de UI")]
    public TextMeshProUGUI dialogText; // Texto del diálogo
    public Image characterImage; // Imagen del personaje
    public GameObject dialogBox; // Caja de diálogo que se muestra en la pantalla

    [Header("Diálogos")]
    public Sprite[] characterImages; // Añade aquí tus imágenes de personajes
    public string[] dialogTexts; // Añade los diálogos aquí

    [Header("Configuración de Audio")]
    public AudioClip dialogAudioClip; // Clip de audio que deseas reproducir

    private Queue<string> sentences; // Cola de frases
    private int currentDialogIndex = -1; // Índice del diálogo actual
    private bool isDialogActive = false; // Indica si el diálogo está activo

    private void Start()
    {
        sentences = new Queue<string>();
        dialogBox.SetActive(false); // Asegúrate de que la caja de diálogo esté inicialmente oculta
    }

    public void StartDialog(int dialogIndex)
    {
        // Verificar el índice del diálogo
        if (dialogIndex < 0 || dialogIndex >= dialogTexts.Length || dialogIndex >= characterImages.Length)
        {
            Debug.LogError("Índice de diálogo fuera de rango.");
            return;
        }

        // Activar la caja de diálogo y pausar el juego
        dialogBox.SetActive(true);
        Time.timeScale = 0f;

        characterImage.sprite = characterImages[dialogIndex];
        dialogText.text = ""; // Limpiar el texto al inicio

        // Preparar las frases del diálogo
        sentences.Clear();
        sentences.Enqueue(dialogTexts[dialogIndex]);

        // Mostrar la primera frase del diálogo
        DisplayNextSentence();

        // Marcar el diálogo como activo
        isDialogActive = true;
        currentDialogIndex = dialogIndex; // Guardar el índice del diálogo actual

        // Reproducir el audio
        AudioSource.PlayClipAtPoint(dialogAudioClip, Camera.main.transform.position);
        
        // Iniciar la espera para la activación de la llamada
        StartCoroutine(WaitForDialogToEnd());
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

        // Reanudar el juego
        Time.timeScale = 1f;
        Debug.Log("Diálogo terminado."); // Mensaje de depuración
    }

    public bool IsDialogActive()
    {
        return isDialogActive;
    }

    public bool IsLastDialogComplete()
    {
        return currentDialogIndex == dialogTexts.Length - 1 && !isDialogActive;
    }

    private IEnumerator WaitForDialogToEnd()
    {
        yield return new WaitUntil(() => !dialogBox.activeSelf);

        Debug.Log("El canvas de diálogos se ha desactivado, verificando el estado del diálogo...");

        if (IsLastDialogComplete())
        {
            Debug.Log("Diálogo completo, activando llamada...");
            // Asegúrate de tener una referencia a Llamadas2
            FindObjectOfType<Llamadas2>().ActivarLlamada();
        }
        else
        {
            Debug.Log("El diálogo no está completo.");
        }
    }

    private void Update()
    {
        // Avanzar al siguiente diálogo cuando se presiona Enter
        if (isDialogActive && Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Tecla 'Enter' presionada."); // Mensaje de depuración
            DisplayNextSentence();
        }
    }
}
