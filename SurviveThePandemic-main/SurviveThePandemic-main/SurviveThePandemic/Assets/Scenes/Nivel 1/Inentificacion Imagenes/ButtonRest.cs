using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonRest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button myButton;
    private bool isClicked = false;

    void Start()
    {
        myButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        isClicked = true;
        StartCoroutine(ResetButtonColor());
    }

    IEnumerator ResetButtonColor()
{
    yield return new WaitForEndOfFrame();  // Espera un fotograma

    // Forzar que el color normal tenga opacidad completa (255).
    Color normalColor = myButton.colors.normalColor;
    normalColor.a = 1f;  // Asegura opacidad completa
    myButton.image.color = normalColor;

    isClicked = false;
}
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isClicked)
        {
            // Activa el color resaltado solo si el botón no fue clicado recientemente.
            myButton.image.color = myButton.colors.highlightedColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Vuelve al color normal cuando el cursor sale del botón.
        myButton.image.color = myButton.colors.normalColor;
    }
}