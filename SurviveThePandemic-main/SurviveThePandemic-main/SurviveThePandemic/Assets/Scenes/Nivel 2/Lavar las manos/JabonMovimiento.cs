using UnityEngine;

public class JabonMovimiento : MonoBehaviour
{
    public RectTransform imagenJabon; // RectTransform del jabón
    public Canvas canvas; // Asignar el Canvas donde está el jabón
    void Update()
    {
       // Mover la imagen del jabón siguiendo el cursor
        Vector2 posicionCursor;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, 
            Input.mousePosition, 
            canvas.worldCamera, // Si el Canvas está en Screen Space - Camera
            out posicionCursor
        );

        // Asigna la posición del cursor al objeto Jabón
        imagenJabon.anchoredPosition = posicionCursor;
    }
}
