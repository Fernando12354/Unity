using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniciarJuegoZonaAhorcado : MonoBehaviour
{
    private bool juegoIniciado = false; // Para evitar múltiples inicios del juego

    // Método que se llama cuando otro objeto entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que ha colisionado tiene el tag "Player" y si el juego no ha sido iniciado aún
        if (other.CompareTag("Player") && !juegoIniciado)
        {
            Debug.Log("Jugador ha entrado en la zona de activación"); // Imprime en la consola cuando el jugador entra en la zona
            
            juegoIniciado = true; // Marca que el juego ha comenzado para evitar reinicios
            AhorcadoGame ahorcadoGame = FindObjectOfType<AhorcadoGame>(); // Busca el script AhorcadoGame en la escena
            
            // Si se encuentra el script AhorcadoGame, inicia el juego automáticamente
            if (ahorcadoGame != null)
            {
                ahorcadoGame.IniciarJuegoAutomáticamente(); // Llama al método para iniciar el juego
                Debug.Log("El juego de ahorcado ha comenzado."); // Confirma que el juego se ha iniciado
            }
            else
            {
                Debug.LogWarning("No se encontró el script AhorcadoGame en la escena."); // Si no se encuentra el script, lanza una advertencia
            }
        }
    }
}
