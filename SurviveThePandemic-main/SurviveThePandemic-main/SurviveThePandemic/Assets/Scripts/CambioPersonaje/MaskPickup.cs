using System.Collections;
using UnityEngine;

public class MaskPickup : MonoBehaviour
{
    public GameObject playerOriginal;  // El modelo original del personaje
    public GameObject playerWithMask;  // El modelo del personaje con cubrebocas
    public float maskDuration = 15f;   // Duración en segundos para que el personaje use el cubrebocas
    public float yOffset = 0.1f;       // Offset en Y para ajustar altura si es necesario
    public CoroutineHandler coroutineHandler; // Referencia al manejador de corrutinas

    private void Start()
    {
        // Asegúrate de que el modelo original esté activo y el modelo con cubrebocas esté desactivado al inicio
        playerOriginal.SetActive(true);
        playerWithMask.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisionó es el jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log("Colisión con cubrebocas detectada. Cambiando el modelo del jugador.");
            
            // Cambia el modelo del jugador al que tiene cubrebocas
            ChangePlayerModel(true);

            // Desactiva el objeto del cubrebocas inmediatamente
            gameObject.SetActive(false);

            // Llama al manejador para iniciar la corrutina
            coroutineHandler.StartRevertCoroutine(this);
        }
    }

    // Método para cambiar entre los dos modelos de personaje
    public void ChangePlayerModel(bool useMask)
    {
        if (useMask)
        {
            // Copia la posición X, Y y Z del personaje original
            Vector3 maskPosition = playerOriginal.transform.position;

            // Asegúrate de que la posición Y no sea negativa
            if (maskPosition.y < 0)
            {
                maskPosition.y = 0.1f;  // Valor mínimo para mantener el personaje por encima del suelo
            }

            // Añadir un pequeño ajuste en Y si es necesario (yOffset)
            maskPosition.y += yOffset;

            // Aplicar la posición y rotación
            playerWithMask.transform.position = maskPosition;
            playerWithMask.transform.rotation = playerOriginal.transform.rotation;

            Debug.Log($"Activando el personaje con máscara en posición: {playerWithMask.transform.position}");

            // Activa el personaje con cubrebocas y desactiva el original
            playerWithMask.SetActive(true);
            playerOriginal.SetActive(false);
        }
        else
        {
            // Restaura el modelo original
            Vector3 originalPosition = playerWithMask.transform.position;

            // Asegúrate de que la posición Y no sea negativa
            if (originalPosition.y < 0)
            {
                originalPosition.y = 0.1f;  // Valor mínimo para mantener el personaje por encima del suelo
            }

            // Añadir un pequeño ajuste en Y si es necesario (yOffset)
            originalPosition.y += yOffset;

            // Aplicar la posición y rotación
            playerOriginal.transform.position = originalPosition;
            playerOriginal.transform.rotation = playerWithMask.transform.rotation;

            Debug.Log($"Restaurando el personaje original en posición: {playerOriginal.transform.position}");

            // Activa el personaje original y desactiva el de cubrebocas
            playerWithMask.SetActive(false);
            playerOriginal.SetActive(true);
        }
    }
}
