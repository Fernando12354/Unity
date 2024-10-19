using UnityEngine;
using System.Collections;

public class SequentialCanvasController : MonoBehaviour
{
    public DisplayMessage displayMessage; // Asigna el script que gestiona la visualización del mensaje
    public DialogManagerSinVida2 dialogManager; // Asigna el manager de diálogos
    public int dialogIndex = 0; // Índice del diálogo actual

    [Header("Canvas")]
    public GameObject canvasNombreMision4; // Canvas de la misión
    public GameObject canvasDialogosNoticieros; // Canvas de diálogos
    public GameObject canvasCall; // Canvas de llamadas

    public Llamadas2 llamadas2; // Asigna el script que gestiona las llamadas

    private bool isMessageComplete = false;

    void Start()
    {
        canvasNombreMision4.SetActive(true);
        canvasDialogosNoticieros.SetActive(false);
        canvasCall.SetActive(true);
    }

    void Update()
    {
        if (!displayMessage.messageText.gameObject.activeSelf && !isMessageComplete)
        {
            Debug.Log("El mensaje ha sido completado, iniciando el siguiente diálogo..."); // Mensaje de depuración
            isMessageComplete = true;
            StartNextDialog();
        }
    }

    private void StartNextDialog()
    {
        canvasNombreMision4.SetActive(false);
        canvasDialogosNoticieros.SetActive(true);

        Debug.Log("Iniciando el siguiente diálogo..."); // Mensaje de depuración
        dialogManager.StartDialog(dialogIndex);
        StartCoroutine(WaitForDialogToEnd());
    }

    private IEnumerator WaitForDialogToEnd()
    {
        yield return new WaitUntil(() => !canvasDialogosNoticieros.activeSelf);

        if (dialogManager.IsLastDialogComplete())
        {
            Debug.Log("Diálogo completo, activando llamada..."); // Mensaje de depuración
            llamadas2.ActivarLlamada();
        }
    }
}
