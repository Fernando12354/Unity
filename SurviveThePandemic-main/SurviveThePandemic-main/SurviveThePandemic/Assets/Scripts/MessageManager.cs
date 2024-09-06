using UnityEngine;
using TMPro; // Para usar TextMeshPro
using UnityEngine.UI; // Para usar imágenes
using System.Collections;
using System.Collections.Generic; // Para usar listas

public class MessageManager : MonoBehaviour
{
    // Clase para almacenar cada diálogo con su imagen, duración y tiempo de espera
    [System.Serializable]
    public class MessageData
    {
        public string message; // El texto del diálogo
        public Sprite image; // La imagen asociada al diálogo
        public float displayDuration = 5f; // Cuánto tiempo estará en pantalla
        public float delayBeforeShowing = 6f; // Cuánto tiempo esperar antes de mostrarlo
    }

    public TextMeshProUGUI messageText; // Referencia al componente TextMeshPro en el UI
    public Image messageImage; // Referencia al componente Image en el UI

    public List<MessageData> messages = new List<MessageData>(); // Lista de diálogos y sus imágenes

    private void Start()
    {
        // Comienza a mostrar los mensajes en secuencia
        StartCoroutine(ShowMessages());
    }

    private IEnumerator ShowMessages()
    {
        foreach (MessageData messageData in messages)
        {
            // Esperar el tiempo especificado antes de mostrar el mensaje
            yield return new WaitForSeconds(messageData.delayBeforeShowing);

            // Asignar el texto y la imagen correspondientes
            messageText.text = messageData.message;
            messageImage.sprite = messageData.image;

            // Asegurarse de que los elementos de UI estén activos
            messageText.gameObject.SetActive(true);
            messageImage.gameObject.SetActive(true);

            // Esperar el tiempo que debe mostrarse el mensaje
            yield return new WaitForSeconds(messageData.displayDuration);

            // Ocultar el texto y la imagen
            messageText.gameObject.SetActive(false);
            messageImage.gameObject.SetActive(false);
        }
    }
}
