using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskPickup : MonoBehaviour
{
    public GameObject playerOriginal;  // El modelo original del personaje
    public GameObject playerWithMask;  // El modelo del personaje con cubrebocas
    public float maskDuration = 15f;   // Duración en segundos para que el personaje use el cubrebocas

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisionó es el jugador
        if (other.CompareTag("Player"))
        {
            // Desactiva el cubrebocas (lo desaparece de la escena)
            gameObject.SetActive(false);

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
            // Copia la posición y rotación del personaje original
            playerWithMask.transform.position = playerOriginal.transform.position;
            playerWithMask.transform.rotation = playerOriginal.transform.rotation;

            // Activa el personaje con cubrebocas y desactiva el original
            playerWithMask.SetActive(true);
            playerOriginal.SetActive(false);
        }
        else
        {
            // Copia la posición y rotación del personaje con cubrebocas al original
            playerOriginal.transform.position = playerWithMask.transform.position;
            playerOriginal.transform.rotation = playerWithMask.transform.rotation;

            // Activa el personaje original y desactiva el de cubrebocas
            playerWithMask.SetActive(false);
            playerOriginal.SetActive(true);
        }
    }

    // Corrutina que espera 15 segundos y luego cambia de nuevo al modelo original
    System.Collections.IEnumerator RevertToOriginalAfterDelay()
    {
        // Espera el tiempo especificado
        yield return new WaitForSeconds(maskDuration);

        // Cambia de nuevo al modelo original
        ChangePlayerModel(false);
    }
}