using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MessageManagerFinal : MonoBehaviour
{
    [System.Serializable]
    public class MessageData
    {
        public string message;
        public Sprite image;
        public float displayDuration = 5f;
        public float delayBeforeShowing = 6f;
    }

    public TextMeshProUGUI messageText;
    public Image messageImage;
    public List<MessageData> messages = new List<MessageData>();

    public Canvas endCanvas; // Canvas que aparecerá al terminar los diálogos
    public Button continueButton; // Botón para cambiar de escena
    public Button exitButton; // Botón para cerrar la aplicación
    public string nextSceneName; // Nombre de la escena que se cargará

    private void Start()
    {
        endCanvas.gameObject.SetActive(false); // Asegurarse de que el Canvas esté inactivo al iniciar
        StartCoroutine(ShowMessages());

        // Asignar acciones a los botones
        continueButton.onClick.AddListener(OnContinue);
        exitButton.onClick.AddListener(OnExit);
    }

    private IEnumerator ShowMessages()
    {
        foreach (MessageData messageData in messages)
        {
            yield return new WaitForSeconds(messageData.delayBeforeShowing);

            messageText.text = messageData.message;
            messageImage.sprite = messageData.image;
            messageText.gameObject.SetActive(true);
            messageImage.gameObject.SetActive(true);

            yield return new WaitForSeconds(messageData.displayDuration);

            messageText.gameObject.SetActive(false);
            messageImage.gameObject.SetActive(false);
        }

        // Al finalizar todos los diálogos, activar el Canvas final
        endCanvas.gameObject.SetActive(true);
    }

    private void OnContinue()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void OnExit()
    {
        Application.Quit();
    }
}
