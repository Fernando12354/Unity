using UnityEngine;

public class CleanableObject : MonoBehaviour
{
    public GameTimer gameTimer; // Referencia al script GameTimer

    private void OnMouseDown()
    {
        // Llamar al método ObjectCleaned en GameTimer pasando este objeto (el objeto de suciedad)
        if (gameTimer != null)
        {
            gameTimer.ObjectCleaned(gameObject); // Pasa el objeto de suciedad a la función
            gameObject.SetActive(false); // Desactiva el objeto de suciedad después de limpiarlo
        }
    }
}
