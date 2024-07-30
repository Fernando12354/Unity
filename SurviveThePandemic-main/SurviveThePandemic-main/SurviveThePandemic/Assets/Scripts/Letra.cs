using UnityEngine;
using UnityEngine.UI;

public class Letra : MonoBehaviour
{
    public Text textoLetra; // Campo para el componente de texto que mostrará la letra

    void Awake()
    {
        textoLetra = GetComponent<Text>();
    }

    // Método para configurar la letra que se muestra
    public void ConfigurarLetra(char letra)
    {
        if (textoLetra != null)
        {
            textoLetra.text = letra.ToString();
        }
    }
}
