using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Para usar TextMeshProUGUI
public class TriggerActionPutTraje : MonoBehaviour
{
    [Header("Configuración de UI")]
    public GameObject messageUI;  // Panel del mensaje
    public TextMeshProUGUI messageText;  // Texto del mensaje
    public string promptMessage = "Presiona X";  // Mensaje a mostrar

    [Header("Configuración del Cambio de Personaje")]
    public GameObject currentPlayer;  // Objeto del jugador actual
    public GameObject newPlayer;  // Objeto del nuevo personaje
    public GameObject objectToActive;  // Objeto a desactivar

    [Header("Sonido")]
    public AudioClip changeSound;  // Sonido que se reproducirá

    private AudioSource audioSource;  // Fuente de audio
    private bool playerInZone = false;  // Estado del jugador en la zona
    private bool actionExecuted = false;  // Verificar si ya se ejecutó la acción

    public MessageManager messageManager; 

     public GameObject wallFanthom;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // Agregar AudioSource al objeto
        messageUI.SetActive(false);  // Asegurar que el mensaje esté oculto al inicio
        newPlayer.SetActive(false);  // Asegurar que el nuevo personaje esté oculto inicialmente
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !actionExecuted)
        {
            playerInZone = true;
            messageText.text = promptMessage;
            messageUI.SetActive(true);  // Mostrar mensaje
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            messageUI.SetActive(false);  // Ocultar mensaje
        }
    }

    private void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.X) && !actionExecuted)
        {
            ExecuteAction();  // Ejecutar acción al presionar X
        }
    }

    private void ExecuteAction()
{
    actionExecuted = true;  // Marcar la acción como ejecutada
    messageUI.SetActive(false);  // Ocultar mensaje definitivamente

    // Desactivar el objeto
    if (objectToActive != null)
    {
        objectToActive.SetActive(true);
    }

    // Reproducir el sonido
    if (changeSound != null)
    {
        audioSource.PlayOneShot(changeSound);
    }

    // Cambiar de personaje en la misma posición y rotación
    Vector3 playerPosition = currentPlayer.transform.position;
    Quaternion playerRotation = currentPlayer.transform.rotation;

    currentPlayer.SetActive(false);  // Desactivar personaje actual
    newPlayer.transform.position = playerPosition;  // Mover el nuevo personaje
    newPlayer.transform.rotation = playerRotation;
    newPlayer.SetActive(true);  // Activar el nuevo personaje
    wallFanthom.SetActive(false);

    Debug.Log("Acción ejecutada: Cambio de personaje, sonido reproducido y objeto desactivado.");

    // Llamar a MessageManager para mostrar mensajes
    if (messageManager != null)
    {
        messageManager.gameObject.SetActive(true); // Activar el MessageManager
        messageManager.StartCoroutine("ShowMessages"); // Comenzar a mostrar mensajes
    }

   
}

}
