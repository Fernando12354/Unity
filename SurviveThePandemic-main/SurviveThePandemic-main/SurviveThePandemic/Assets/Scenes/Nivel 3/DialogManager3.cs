using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManager3 : MonoBehaviour
{
    [Header("Configuración de UI")]
    public TextMeshProUGUI dialogText;
    public Image characterImage;
    public GameObject dialogBox;
    public GameObject escenario, ahorcado;

    [Header("Diálogos")]
    public Sprite[] characterImages = new Sprite[] { };
    public string[] dialogTexts = new string[] { };

    private Queue<string> sentences;
    private int currentDialogIndex = -1;
    private bool isDialogActive = false;

    private Vector2 originalResolution = new Vector2(1920, 1080); // Resolución original
    private Vector2 dialogResolution = new Vector2(3840, 2160); // Resolución para los diálogos

    private void Start()
    {
        sentences = new Queue<string>();
        dialogBox.SetActive(false);
    }

    public void StartDialog(int dialogIndex)
    {
        if (dialogIndex < 0 || dialogIndex >= dialogTexts.Length || dialogIndex >= characterImages.Length)
        {
            Debug.LogError("Índice de diálogo fuera de rango.");
            return;
        }

        // Cambia la resolución al iniciar los diálogos
        SetResolution(dialogResolution);

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
            escenario.SetActive(false);
            ahorcado.SetActive(true);
        }
        else
        {
            dialogBox.SetActive(false);
            isDialogActive = false;
            Time.timeScale = 1f;
        }

        // Restaura la resolución original al terminar los diálogos
        SetResolution(originalResolution);
    }

    private void Update()
    {
        if (isDialogActive && Input.GetKeyDown(KeyCode.Return))
        {
            DisplayNextSentence();
        }
    }

    // Función para restaurar el escenario después de terminar el minijuego del ahorcado
    public void ReturnToEscenario()
    {
        ahorcado.SetActive(false);
        escenario.SetActive(true);
        Time.timeScale = 1f;
    }

    // Función para cambiar la resolución
    private void SetResolution(Vector2 resolution)
    {
        Screen.SetResolution((int)resolution.x, (int)resolution.y, FullScreenMode.Windowed);
    }
}
