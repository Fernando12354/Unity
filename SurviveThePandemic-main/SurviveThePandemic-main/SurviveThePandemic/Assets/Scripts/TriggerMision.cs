using UnityEngine;
using UnityEngine.SceneManagement; // Para cargar la escena
using UnityEngine.UI; // Para los botones

public class TriggerMision : MonoBehaviour
{
    public Canvas canvasDecision; // Canvas con los dos botones de decisión
    public Canvas canvasMisionCompletada; // Canvas que indica que la misión fue completada
    public GameObject canvasguia; // Objeto de canvasguia
    public string sceneToLoad; // Nombre de la escena a cargar (se puede modificar en el inspector)

    private bool canvasDecisionActive = false; // Para verificar si el CanvasDecision está activo

    private void Start()
    {
        // Asegúrate de que canvasDecision tenga un objeto asignado, de lo contrario, mostrar un mensaje de error
        if (canvasDecision == null)
        {
            Debug.LogError("canvasDecision no está asignado en el Inspector. Por favor, asígnalo.");
            return; // Salir de la función si canvasDecision no está asignado
        }

        canvasDecision.gameObject.SetActive(false); // Desactivar el CanvasDecision al iniciar

        // Asegúrate de que canvasMisionCompletada tenga un objeto asignado
        if (canvasMisionCompletada == null)
        {
            Debug.LogError("canvasMisionCompletada no está asignado en el Inspector. Por favor, asígnalo.");
            return; // Salir de la función si canvasMisionCompletada no está asignado
        }

        canvasMisionCompletada.gameObject.SetActive(false); // Desactivar el CanvasMisionCompletada al iniciar
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegurarse de que solo el jugador active el canvas
        {
            // Solo intenta desactivar canvasguia si está asignado
            if (canvasguia != null)
            {
                canvasguia.SetActive(false);
            }

            if (canvasDecision != null)
            {
                canvasDecision.gameObject.SetActive(true); // Mostrar el Canvas de decisión
                canvasDecisionActive = true;
            }
        }
    }

    public void OnContinueButtonClicked()
    {
        if (canvasDecision != null)
        {
            canvasDecision.gameObject.SetActive(false); // Ocultar el Canvas de decisión
        }
        canvasMisionCompletada.gameObject.SetActive(true); // Mostrar el Canvas de misión completada
    }

    public void OnCloseButtonClicked()
    {
        if (canvasDecision != null)
        {
            canvasDecision.gameObject.SetActive(false); // Ocultar solo el Canvas de decisión
            canvasDecisionActive = false;
        }
    }

    private void Update()
    {
        if (canvasMisionCompletada.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Return)) // Si el canvas de misión está activo y se presiona Enter
        {
            // Cargar la siguiente escena utilizando el nombre especificado en el inspector
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
