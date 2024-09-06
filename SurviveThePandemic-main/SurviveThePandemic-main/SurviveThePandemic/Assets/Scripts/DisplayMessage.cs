using UnityEngine;
using TMPro; // Necesario para usar TextMeshPro

public class DisplayMessage : MonoBehaviour
{
    public TextMeshProUGUI messageText; // Arrastra el componente TextMeshPro desde el Inspector.
    public float displayDuration = 3f; // Tiempo que el mensaje permanecerá en pantalla.
    
    [TextArea] // Esto te permitirá escribir el mensaje directamente en el Inspector con un área de texto más grande.
    public string messageToShow; // Mensaje que se mostrará, asignado desde el Inspector.

    void Start()
    {
        // Asigna el mensaje recibido desde el Inspector.
        messageText.text = messageToShow;

        // Asegúrate de que el mensaje sea visible.
        messageText.gameObject.SetActive(true);

        // Llama al método para ocultar el texto después de 'displayDuration' segundos.
        Invoke("HideMessage", displayDuration);
    }

    void HideMessage()
    {
        // Oculta el mensaje después de que pase el tiempo especificado.
        messageText.gameObject.SetActive(false);
    }
}

