using UnityEngine;
using System.Collections;

public class SequentialCanvasController : MonoBehaviour
{
    public DisplayMessage displayMessage;      // Referencia al componente DisplayMessage
    public DialogManagerSinVida dialogManager; // Referencia al componente DialogManagerSinVida
    public int dialogIndex = 0;                // Índice del diálogo a iniciar
    public GameObject canvasNombreMision4;     // Referencia al Canvas NombreMision4
    public GameObject canvasDialogosNoticieros;// Referencia al Canvas CanvasDialogosNoticieros
    public GameObject canvasCall;              // Referencia al Canvas CanvasCall
    public Llamadas llamadas;                  // Referencia al script de llamadas

    private bool isMessageComplete = false;

    void Update()
    {
        // Si el mensaje ya se ha mostrado y el diálogo aún no ha comenzado
        if (!displayMessage.messageText.gameObject.activeSelf && !isMessageComplete)
        {
            isMessageComplete = true; // Marca el mensaje como completo
            StartNextDialog();        // Inicia el diálogo
        }
    }

    void StartNextDialog()
    {
        // Desactiva el canvas NombreMision4 y activa CanvasDialogosNoticieros
        canvasNombreMision4.SetActive(false);      // Desactiva NombreMision4
        canvasDialogosNoticieros.SetActive(true);  // Activa CanvasDialogosNoticieros

        dialogManager.StartDialog(dialogIndex);    // Inicia el diálogo con el índice especificado
    }

    public void EndCurrentDialog()
    {
        // Llamado cuando termina el último diálogo
        Debug.Log("Diálogo finalizado, activando siguiente script...");

        // Desactiva el CanvasDialogosNoticieros y activa el CanvasCall
        canvasDialogosNoticieros.SetActive(false);  // Desactiva CanvasDialogosNoticieros
        canvasCall.SetActive(true);                 // Activa CanvasCall

        // Inicia el siguiente script de llamadas
        StartCoroutine(llamadas.StartCall());
    }
}
