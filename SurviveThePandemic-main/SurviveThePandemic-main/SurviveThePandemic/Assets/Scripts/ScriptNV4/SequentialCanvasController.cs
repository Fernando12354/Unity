using UnityEngine;
using System.Collections; // Required for IEnumerator

public class SequentialCanvasController : MonoBehaviour
{
    public GameObject[] canvases;  // Los tres canvases que quieres activar secuencialmente
    private int currentCanvasIndex = 0;

    private void Start()
    {
        // Asegurarte de que todos los Canvas excepto el primero estén desactivados
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].SetActive(i == currentCanvasIndex);
        }

        // Iniciar el primer Canvas
        StartCoroutine(StartCanvasSequence());
    }

    private IEnumerator StartCanvasSequence()
    {
        // Activar y esperar a que cada Canvas termine antes de pasar al siguiente
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].SetActive(true);

            // Espera la finalización del script asociado antes de continuar con el siguiente Canvas
            yield return new WaitUntil(() => IsCanvasScriptComplete(canvases[i]));

            // Desactivar el Canvas actual antes de activar el siguiente
            canvases[i].SetActive(false);
        }
    }

    private bool IsCanvasScriptComplete(GameObject canvas)
    {
        // Aquí implementas la lógica para verificar si el script asociado a cada Canvas ha terminado.
        // Esta función puede verificar que los diálogos hayan terminado o que una acción específica haya ocurrido.
        
        if (canvas.GetComponent<DisplayMessage>() != null)
        {
            return !canvas.GetComponent<DisplayMessage>().messageText.gameObject.activeSelf;
        }
        else if (canvas.GetComponent<DialogManagerSinVida>() != null)
        {
            return !canvas.GetComponent<DialogManagerSinVida>().IsDialogActive();
        }
        else if (canvas.GetComponent<Llamadas>() != null)
        {
            // Verificar si la llamada ha sido contestada y terminada
            return canvas.GetComponent<Llamadas>().callAnswer;
        }

        // En caso de que no haya un script específico, devuelve false
        return false;
    }
}

