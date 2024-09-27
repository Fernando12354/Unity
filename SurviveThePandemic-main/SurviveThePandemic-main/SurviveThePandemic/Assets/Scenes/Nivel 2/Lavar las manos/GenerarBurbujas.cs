using UnityEngine;
using UnityEngine.UI;

public class GenerarBurbujas : MonoBehaviour
{
     public RectTransform manos; // Referencia a las manos
    public GameObject burbujaPrefab; // Prefab de la burbuja
    public Canvas canvas; // Asignar el Canvas
    public int burbujasNecesarias = 50; // Número total de burbujas para cubrir las manos
    public int burbujasInstanciadas = 0;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && EstaSobreLasManos())
        {
            // Instanciar burbuja
            CrearBurbuja();

            // Aumentar contador de burbujas instanciadas
            burbujasInstanciadas++;

            // Verificar si se alcanzó el número de burbujas para ganar
            if (burbujasInstanciadas >= burbujasNecesarias)
            {
                Ganar();
            }
        }
    }
    bool EstaSobreLasManos()
    {
        // Verifica si el cursor está sobre las manos
        return RectTransformUtility.RectangleContainsScreenPoint(manos, Input.mousePosition, canvas.worldCamera);
    }

    void CrearBurbuja()
    {
        // Obtener la posición del cursor en relación al Canvas
        Vector2 posicionCursor;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, 
            Input.mousePosition, 
            canvas.worldCamera, 
            out posicionCursor
        );

        // Instanciar la burbuja
        GameObject nuevaBurbuja = Instantiate(burbujaPrefab, canvas.transform);
        nuevaBurbuja.GetComponent<RectTransform>().anchoredPosition = posicionCursor;
    }

    void Ganar()
    {
        // Lógica para ganar, como mostrar la pantalla de victoria
        Debug.Log("¡Manos completamente cubiertas de burbujas! ¡Has ganado!");
    }
}


