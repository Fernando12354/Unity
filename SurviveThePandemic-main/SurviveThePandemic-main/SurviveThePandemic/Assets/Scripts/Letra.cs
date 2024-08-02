using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Letra : MonoBehaviour, IPointerClickHandler
{
    public Text textoLetra;
    private SopaDeLetrasManager sopaDeLetrasManager;

    void Start()
    {
        sopaDeLetrasManager = FindObjectOfType<SopaDeLetrasManager>();
    }

    public void ConfigurarLetra(char letra)
    {
        if (textoLetra != null)
        {
            textoLetra.text = letra.ToString();
        }
        else
        {
            Debug.LogError("TextoLetra no está asignado en el prefab Letra.");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        sopaDeLetrasManager.LetraSeleccionada(this);
    }
}
