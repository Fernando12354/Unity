using UnityEngine;
using UnityEngine.UI;

public class GenerarBurbujas : MonoBehaviour
{
    public Image manos;
    public Image burbujas;
    public float burbujasIncremento = 0.1f;

    void Update()
    {
        // Detectar clic izquierdo
        if (Input.GetMouseButton(0))
        {
            // Lógica para aumentar las burbujas
            Color burbujasColor = burbujas.color;
            burbujasColor.a += burbujasIncremento * Time.deltaTime;
            burbujas.color = burbujasColor;

            // Asegurarse de que no exceda la opacidad máxima
            burbujasColor.a = Mathf.Clamp(burbujasColor.a, 0, 1);
        }
    }
}

