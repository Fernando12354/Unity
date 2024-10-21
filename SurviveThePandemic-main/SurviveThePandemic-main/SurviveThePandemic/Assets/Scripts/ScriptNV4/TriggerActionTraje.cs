using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Para usar TextMeshProUGUI

public class TriggerActionTraje : MonoBehaviour
{
    [Header("Configuración de UI")]
    public GameObject messageUI; // Panel de mensaje que se muestra
    public TextMeshProUGUI messageText; // Texto del mensaje (ejemplo: "Presiona X")
    public string promptMessage = "Presiona X"; // Mensaje a mostrar

    private bool playerInZone = false; // Indica si el jugador está dentro de la zona

    private void Start()
    {
        // Asegúrate de que el mensaje esté oculto al iniciar
        messageUI.SetActive(false);
    }

    // Detectar cuándo el jugador entra en la zona (asegúrate de que el objeto tenga un Collider con IsTrigger activado)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tenga la etiqueta "Player"
        {
            playerInZone = true;
            messageText.text = promptMessage; // Establecer el mensaje
            messageUI.SetActive(true); // Mostrar el mensaje
        }
    }

    // Detectar cuándo el jugador sale de la zona
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            messageUI.SetActive(false); // Ocultar el mensaje
        }
    }

    private void Update()
    {
        // Detectar si el jugador está en la zona y presiona la tecla X
        if (playerInZone && Input.GetKeyDown(KeyCode.X))
        {
            ExecuteAction(); // Llamar a la acción
        }
    }

    // Acción que se ejecuta al presionar X
    private void ExecuteAction()
    {
        Debug.Log("Acción ejecutada al presionar X");
        // Aquí puedes agregar cualquier acción que desees
        // Por ejemplo: Abrir una puerta, recoger un objeto, etc.
    }
}
