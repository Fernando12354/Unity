using UnityEngine;
using UnityEngine.EventSystems;

public class LetraArrastrable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public char letra;  // Letra que representa este objeto
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform parentOriginal;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentOriginal = transform.parent;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvasGroup.transform.localScale.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // Si no se coloca en un guion válido, vuelve a su posición original
        if (transform.parent == parentOriginal || transform.parent == canvasGroup.transform)
        {
            transform.SetParent(parentOriginal);
        }
    }

    // Método para evitar que la letra se arrastre de nuevo si ya está asignada
    public void BloquearArrastre()
    {
        canvasGroup.blocksRaycasts = false;
    }
}

