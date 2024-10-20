using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public CollectibleManager collectibleManager; // Referencia al CollectibleManager

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisionó es el jugador
        if (other.CompareTag("Player"))
        {
            collectibleManager.RecolectarObjeto(gameObject); // Llama al manager para recolectar el objeto
        }
    }
}
