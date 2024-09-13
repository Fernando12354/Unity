using UnityEngine;
using UnityEngine.SceneManagement; // Para cargar la escena
using UnityEngine.UI; // Para los botones

public class TriggerMision : MonoBehaviour
{
    public Canvas canvasDecision; // Canvas con los dos botones de decisión
    public Canvas canvasMisionCompletada; // Canvas que indica que la misión fue completada
    private bool canvasDecisionActive = false; // Para verificar si el CanvasDecision está activo

    private void Start()
    {
        canvasDecision.gameObject.SetActive(false); // Desactivar el CanvasDecision al iniciar
        canvasMisionCompletada.gameObject.SetActive(false); // Desactivar el CanvasMisionCompletada al iniciar
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegurarse de que solo el jugador active el canvas
        {
            canvasDecision.gameObject.SetActive(true); // Mostrar el Canvas de decisión
            canvasDecisionActive = true;
        }
    }

    public void OnContinueButtonClicked()
    {
        canvasDecision.gameObject.SetActive(false); // Ocultar el Canvas de decisión
        canvasMisionCompletada.gameObject.SetActive(true); // Mostrar el Canvas de misión completada
    }

    public void OnCloseButtonClicked()
    {
        canvasDecision.gameObject.SetActive(false); // Ocultar solo el Canvas de decisión
        canvasDecisionActive = false;
    }

    private void Update()
    {
        if (canvasMisionCompletada.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Return)) // Si el canvas de misión está activo y se presiona Enter
        {
            // Cargar la siguiente escena (asegúrate de agregar la escena en la configuración de Build Settings)
            SceneManager.LoadScene("Sala de Alex");
        }
    }
}
