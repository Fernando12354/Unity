using UnityEngine;

public class CleanableObject : MonoBehaviour
{
    public GameTimer gameTimer; // Referencia al GameTimer

    void Start()
    {
        if (gameTimer == null)
        {
            // Intenta encontrar el GameTimer en la escena si no está asignado
            gameTimer = FindObjectOfType<GameTimer>();
        }
    }

    void OnMouseDown()
    {
        // Llama al método ObjectCleaned en GameTimer
        if (gameTimer != null)
        {
            gameTimer.ObjectCleaned(); // Notificar que se ha limpiado un objeto
            Destroy(gameObject); // Eliminar el objeto de suciedad
        }
    }
}


