using UnityEngine;
using UnityEngine.UI;

public class GenerarBurbujas : MonoBehaviour
{
    public RectTransform manos; // Referencia a las manos
    public GameObject burbujaPrefab; // Prefab de la burbuja
    public Canvas canvas; // Asignar el Canvas

    // Método para verificar si el cursor está sobre las manos
    public bool EstaSobreLasManos()
    {
        return RectTransformUtility.RectangleContainsScreenPoint(manos, Input.mousePosition, canvas.worldCamera);
    }

    // Método para crear una burbuja en la posición del cursor
    public void CrearBurbuja()
    {
        Vector2 posicionCursor;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, 
            Input.mousePosition, 
            canvas.worldCamera, 
            out posicionCursor
        );

        // Instanciar la burbuja en la posición del cursor
        GameObject nuevaBurbuja = Instantiate(burbujaPrefab, canvas.transform);
        nuevaBurbuja.GetComponent<RectTransform>().anchoredPosition = posicionCursor;

        Debug.Log("Burbuja creada");
    }
}
