using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniciarJuegoZonaAhorcado : MonoBehaviour
{
    public GameObject ahorcado;
    public GameObject escenario;

    // Método que se llama cuando otro objeto entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que ha colisionado tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador ha entrado en la zona de activación"); // Imprime en la consola cuando el jugador entra en la zona
            escenario.SetActive(false);
            ahorcado.SetActive(true);

            // Desactiva el objeto donde está adjuntado este script
            gameObject.SetActive(false);
        }
    }
}

