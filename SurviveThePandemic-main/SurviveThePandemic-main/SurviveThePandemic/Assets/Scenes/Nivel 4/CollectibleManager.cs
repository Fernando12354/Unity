using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public GameObject playerOriginal;      // El modelo original del personaje
    public GameObject nuevoPersonajePrefab; // Prefab del nuevo personaje a cargar
    public int objetosRequeridos = 3;      // Número de objetos requeridos para cambiar de personaje
    public float yOffset = 0.1f;            // Offset en Y para ajustar altura si es necesario

    private int objetosRecogidos = 0;       // Contador de objetos recogidos
    private GameObject nuevoPersonaje;      // Referencia al nuevo personaje instanciado

    private List<GameObject> objetosRecolectables; // Lista de objetos recolectables

    private void Start()
    {
        // Asegúrate de que el modelo original esté activo al inicio
        playerOriginal.SetActive(true);

        // Inicializa la lista
        objetosRecolectables = new List<GameObject>();

        // Encuentra todos los objetos recolectables en la escena
        CollectiblesSetup();
    }

    private void CollectiblesSetup()
    {
        // Encuentra todos los objetos con la etiqueta "Collectible"
        GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Collectible");

        foreach (GameObject collectible in collectibles)
        {
            // Añadir el objeto a la lista
            objetosRecolectables.Add(collectible);
            // Asegúrate de que cada objeto recolectable tenga el script de recolección
            CollectibleItem item = collectible.AddComponent<CollectibleItem>();
            item.collectibleManager = this; // Asigna este manager al objeto
        }
    }

    // Método para manejar la recolección de objetos
    public void RecolectarObjeto(GameObject objeto)
    {
        Debug.Log("Objeto recogido detectado. Cambiando el modelo del jugador.");
        objetosRecogidos++;

        // Desactiva el objeto que fue recogido
        objeto.SetActive(false);

        // Verifica si se han recogido suficientes objetos
        if (objetosRecogidos >= objetosRequeridos)
        {
            CambiarPersonaje();
        }
    }

    // Método para cambiar entre el modelo original y el nuevo personaje
    void CambiarPersonaje()
    {
        if (nuevoPersonajePrefab != null)
        {
            // Instanciar el nuevo personaje en la posición del original
            nuevoPersonaje = Instantiate(nuevoPersonajePrefab, playerOriginal.transform.position, playerOriginal.transform.rotation);
            
            // Ajustar la posición y rotación
            Vector3 nuevoPosicion = nuevoPersonaje.transform.position;
            nuevoPosicion.y += yOffset; // Ajuste en Y si es necesario
            nuevoPersonaje.transform.position = nuevoPosicion;

            Debug.Log($"Activando el nuevo personaje en posición: {nuevoPersonaje.transform.position}");

            // Desactivar el personaje original
            playerOriginal.SetActive(false);
        }
        else
        {
            Debug.LogError("No se ha asignado un prefab de nuevo personaje.");
        }
    }
}
