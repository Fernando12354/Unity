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
        textoLetra.text = letra.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        sopaDeLetrasManager.LetraSeleccionada(this);
    }
}
