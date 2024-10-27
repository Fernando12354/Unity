using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private GameController gameController;
    private Canvas canvas;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        gameController = FindObjectOfType<GameController>();
        canvas = FindObjectOfType<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Cambiar la opacidad al arrastrar
        canvasGroup.alpha = 0.6f; 
        // Mover el objeto
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Restaurar la opacidad
        canvasGroup.alpha = 1f; 

        // Verifica si está en el contenedor y desactiva el objeto si es de la fase actual
        if (gameController != null && gameController.EnContenedor(gameObject) && gameController.EsObjetoDeFaseActual(gameObject)) 
        {
            Debug.Log("Desactivando objeto: " + gameObject.name); // Mensaje de depuración
            gameObject.SetActive(false); // Desactivar el objeto si está en el contenedor y es de la fase actual
        }
        else
        {
            Debug.Log("No se desactiva: " + gameObject.name); // Mensaje si no se desactiva
        }
    }
}
