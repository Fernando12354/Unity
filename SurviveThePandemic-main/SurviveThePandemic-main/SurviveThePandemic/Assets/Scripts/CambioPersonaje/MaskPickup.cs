using System.Collections;
using UnityEngine;

public class MaskPickup : MonoBehaviour
{
    public GameObject playerOriginal;  // El modelo original del personaje
    public GameObject playerWithMask;  // El modelo del personaje con cubrebocas
    public float maskDuration = 15f;   // Duración en segundos para que el personaje use el cubrebocas
    public float yOffset = 0.1f;       // Offset en Y para ajustar altura si es necesario

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisionó es el jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log("Colisión con cubrebocas detectada. Cambiando el modelo del jugador.");
            
            // Cambia el modelo del jugador al que tiene cubrebocas
            ChangePlayerModel(true);

            // Comienza la corrutina para cambiar el modelo de vuelta después de 15 segundos
            StartCoroutine(RevertToOriginalAfterDelay());
        }
    }

    // Método para cambiar entre los dos modelos de personaje
    void ChangePlayerModel(bool useMask)
    {
        if (useMask)
        {
            // Copia la posición X, Y y Z del personaje original
            Vector3 maskPosition = playerOriginal.transform.position;

            // Asegúrate de que la posición Y no sea negativa, si es menor a 0, ajusta a un mínimo
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
            // Copia la posición X, Y y Z del personaje con cubrebocas al original
            Vector3 originalPosition = playerWithMask.transform.position;

            // Asegúrate de que la posición Y no sea negativa, si es menor a 0, ajusta a un mínimo
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

    // Corrutina que espera 15 segundos y luego cambia de nuevo al modelo original
    IEnumerator RevertToOriginalAfterDelay()
    {
        Debug.Log("Esperando 15 segundos para restaurar el modelo original.");
        
        // Espera el tiempo especificado
        yield return new WaitForSeconds(maskDuration);

        // Cambia de nuevo al modelo original
        ChangePlayerModel(false);

        Debug.Log("Modelo restaurado al original después de 15 segundos.");

        // Desactiva el objeto después de que la corrutina termine su tarea
        gameObject.SetActive(false);
    }
}
/*using System.Collections;
using UnityEngine;

public class MaskPickup : MonoBehaviour
{
    public GameObject playerOriginal;  // El modelo original del personaje
    public GameObject playerWithMask;  // El modelo del personaje con cubrebocas
    public float maskDuration = 15f;   // Duración en segundos para que el personaje use el cubrebocas
    public float yOffset = 0.1f;       // Offset en Y para ajustar altura si es necesario

    private bool maskActive = false;   // Verifica si la máscara está activa

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisionó es el jugador y que el cambio no haya ocurrido ya
        if (other.CompareTag("Player") && !maskActive)
        {
            maskActive = true;  // Marca la máscara como activa para evitar múltiples activaciones

            Debug.Log("Cubrebocas recogido. Iniciando el cambio de personaje.");

            // Cambia el modelo del jugador al que tiene cubrebocas
            ChangePlayerModel(true);

            // Desactiva el cubrebocas inmediatamente
            gameObject.SetActive(false);

            // Inicia la corrutina para cambiar el modelo de vuelta después de 15 segundos
            StartCoroutine(RevertToOriginalAfterDelay());
        }
    }

    // Método para cambiar entre los dos modelos de personaje
    void ChangePlayerModel(bool useMask)
    {
        if (useMask)
        {
            // Copia la posición y rotación del personaje original
            Vector3 maskPosition = playerOriginal.transform.position;
            maskPosition.y = Mathf.Max(maskPosition.y + yOffset, 0.1f);  // Asegura que esté encima del suelo

            playerWithMask.transform.position = maskPosition;
            playerWithMask.transform.rotation = playerOriginal.transform.rotation;

            Debug.Log("Personaje con cubrebocas activado.");

            // Activa el personaje con cubrebocas y desactiva el original
            playerWithMask.SetActive(true);
            playerOriginal.SetActive(false);
        }
        else
        {
            // Copia la posición y rotación del personaje con cubrebocas al original
            Vector3 originalPosition = playerWithMask.transform.position;
            originalPosition.y = Mathf.Max(originalPosition.y + yOffset, 0.1f);  // Asegura que esté encima del suelo

            playerOriginal.transform.position = originalPosition;
            playerOriginal.transform.rotation = playerWithMask.transform.rotation;

            Debug.Log("Personaje original restaurado.");

            // Activa el personaje original y desactiva el de cubrebocas
            playerOriginal.SetActive(true);
            playerWithMask.SetActive(false);
        }
    }

    // Corrutina que espera el tiempo especificado y luego cambia de nuevo al modelo original
    IEnumerator RevertToOriginalAfterDelay()
    {
        Debug.Log("Esperando " + maskDuration + " segundos para restaurar el personaje original.");

        // Espera el tiempo especificado
        yield return new WaitForSeconds(maskDuration);

        // Cambia de nuevo al modelo original
        ChangePlayerModel(false);

        Debug.Log("Personaje original restaurado después de " + maskDuration + " segundos.");

        // Resetea el estado para poder recoger otro cubrebocas en el futuro
        maskActive = false;
    }
}*/
