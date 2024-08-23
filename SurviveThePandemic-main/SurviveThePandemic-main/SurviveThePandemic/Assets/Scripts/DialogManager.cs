using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [Header("Configuración de UI")]
    public TextMeshProUGUI dialogText;      // Texto del diálogo
    public Image characterImage;            // Imagen del personaje
    public GameObject dialogBox;            // Caja de diálogo que se muestra en la pantalla

    [Header("Diálogos")]
    public List<Sprite> characterImages;    // Imágenes de los personajes
    public List<string> characterNames;     // Nombres de los personajes
    public List<List<string>> dialogSentences; // Frases del diálogo, cada lista corresponde a un personaje

    private Queue<string> sentences;        // Cola de frases
    private bool isDialogActive = false;    // Indica si el diálogo está activo

    private void Start()
    {
        sentences = new Queue<string>();
        dialogBox.SetActive(false);          // Asegúrate de que la caja de diálogo esté inicialmente oculta
    }

    public void StartDialog(int dialogIndex)
    {

         Debug.Log("eNTRO AL OTRO SCRIPT.");
        if (dialogIndex >= dialogSentences.Count || dialogIndex >= characterImages.Count)
        {
            Debug.LogError("Índice de diálogo fuera de rango.");
            return;
        }

        // Pausar el juego
        Time.timeScale = 0f;

        // Mostrar el panel de diálogo
        dialogBox.SetActive(true);

        // Asignar la imagen del personaje y el nombre
        characterImage.sprite = characterImages[dialogIndex];
        dialogText.text = "";

        sentences.Clear();
        foreach (string sentence in dialogSentences[dialogIndex])
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
        isDialogActive = true; // Indica que el diálogo está activo
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogText.text = sentence; // Mostrar el texto de la frase
    }

    public void EndDialog()
    {
        dialogBox.SetActive(false);        // Ocultar la caja de diálogo
        isDialogActive = false;            // Indica que el diálogo ha terminado

        // Reanudar el juego
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (isDialogActive && Input.GetKeyDown(KeyCode.Return))
        {
            DisplayNextSentence();
        }
    }
}
   
