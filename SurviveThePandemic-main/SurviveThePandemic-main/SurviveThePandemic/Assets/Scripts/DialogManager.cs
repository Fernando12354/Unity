using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public GameObject dialogBox; // Caja de diálogo que se muestra en la pantalla
    public Image characterImage; // Imagen del personaje que habla
    public GameObject player; // Referencia al jugador para pausar el movimiento

    private Queue<DialogSentence> sentences;
    private bool isDialogActive = false;

    void Start()
    {
        sentences = new Queue<DialogSentence>();
        dialogBox.SetActive(false); // Asegúrate de que la caja de diálogo esté inicialmente oculta
    }

    public void StartDialog(Dialog dialog)
    {
        // Pausar el juego
        Time.timeScale = 0f;

        dialogBox.SetActive(true); // Mostrar la caja de diálogo
        nameText.text = dialog.characterName; // Mostrar el nombre del personaje

        sentences.Clear();

        foreach (var sentence in dialog.sentences)
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

        var sentence = sentences.Dequeue();
        dialogText.text = sentence.text;
        characterImage.sprite = sentence.characterSprite; // Mostrar la imagen del personaje
    }

    void Update()
    {
        if (isDialogActive && Input.GetKeyDown(KeyCode.Return))
        {
            DisplayNextSentence();
        }
    }

    void EndDialog()
    {
        dialogBox.SetActive(false); // Ocultar la caja de diálogo
        isDialogActive = false; // Indica que el diálogo ha terminado

        // Reanudar el juego
        Time.timeScale = 1f;
    }
}



