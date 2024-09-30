using UnityEngine;

public class InicializadorEscena : MonoBehaviour
{
    void Awake()
    {
        // Este método se llama al inicio antes de que se inicialicen otros componentes.
        Inicializar();
    }

    void Start()
    {
        // Este método se llama después de que todos los objetos en la escena están inicializados.
        Debug.Log("La escena ha iniciado correctamente.");
    }

    void Inicializar()
    {
        // Aquí puedes inicializar tus objetos y cámaras.
        // Por ejemplo, asegúrate de que la cámara esté activa:
        Camera camara = Camera.main;
        if (camara != null)
        {
            camara.gameObject.SetActive(true);
            Debug.Log("La cámara principal está activa.");
        }
        else
        {
            Debug.LogWarning("No se encontró una cámara principal.");
        }

        // Inicializa otros objetos de la escena si es necesario
        // Por ejemplo:
        GameObject[] objetos = GameObject.FindGameObjectsWithTag("Objeto");
        foreach (GameObject objeto in objetos)
        {
            objeto.SetActive(true); // Asegúrate de que todos los objetos relevantes estén activos
            Debug.Log($"Objeto activado: {objeto.name}");
        }
    }
}
