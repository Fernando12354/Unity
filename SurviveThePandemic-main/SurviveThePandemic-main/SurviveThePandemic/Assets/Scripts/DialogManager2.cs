using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManager2 : MonoBehaviour
{
    [Header("Configuración de UI")]
    public TextMeshProUGUI dialogText;
    public Image characterImage;
    public GameObject dialogBox;
    public GameObject healthBarCanvas;
    public GameObject decisionCanvas;
    public Button readyButton;
    public Button notReadyButton;

    [Header("Diálogos")]
    public Sprite[] characterImages = new Sprite[] { };
    public string[] dialogTexts = new string[] { };

    private Queue<string> sentences;
    private int currentDialogIndex = -1;
    private bool isDialogActive = false;

    [Header("Configuración de Escena")]
    public string sceneToLoad; // Nombre de la escena a cargar desde el Inspector

    private void Start()
    {
        sentences = new Queue<string>();
        dialogBox.SetActive(false);
        decisionCanvas.SetActive(false);

        if (healthBarCanvas != null)
        {
            healthBarCanvas.SetActive(true);
        }

        readyButton.onClick.AddListener(OnReadyButtonClicked);
        notReadyButton.onClick.AddListener(OnNotReadyButtonClicked);
    }

    public void StartDialog(int dialogIndex)
    {
        if (dialogIndex < 0 || dialogIndex >= dialogTexts.Length || dialogIndex >= characterImages.Length)
        {
            Debug.LogError("Índice de diálogo fuera de rango.");
            return;
        }

        if (healthBarCanvas != null)
        {
            healthBarCanvas.SetActive(false);
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

            if (healthBarCanvas != null)
            {
                healthBarCanvas.SetActive(true);
            }
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
        Time.timeScale = 1f;

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad); // Carga la escena desde el Inspector
        }
        else
        {
            Debug.LogError("El nombre de la escena no está configurado.");
        }
    }

    private void OnNotReadyButtonClicked()
    {
        decisionCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
}

