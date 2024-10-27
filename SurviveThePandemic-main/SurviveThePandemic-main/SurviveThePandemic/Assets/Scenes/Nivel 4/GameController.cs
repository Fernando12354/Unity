using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public List<GameObject> objetosJeringas;
    public List<GameObject> objetosCubrebocas;
    public List<GameObject> objetosGuantes;
    public List<GameObject> objetosAlcohol;
    public List<GameObject> objetosGel;
    public GameObject contenedorClasificacion;
    public Text mensaje;
    public GameObject canvasVictoria;
    public GameObject canvasDerrota;
    public GameObject canvasMisionCompletada; // Nuevo canvas para la misión completada
    public Camera mainCamera;
    public Text textoTiempo; // Objeto de texto para mostrar el tiempo
    public float tiempoLimite = 120f;  // Tiempo límite total
    public string escenaSiguiente;     // Nombre de la escena siguiente, modificable en el Inspector

    private int faseActual = 0;
    private float tiempoRestante;
    private List<GameObject>[] fasesObjetos;

    void Start()
    {
        tiempoRestante = tiempoLimite;

        // Inicializar fases
        fasesObjetos = new List<GameObject>[] { objetosJeringas, objetosCubrebocas, objetosGuantes, objetosAlcohol, objetosGel };

        // Mostrar todos los objetos al inicio
        MostrarTodosLosObjetos();

        // Iniciar primera fase
        IniciarFase();
    }

    void Update()
    {
        // Control de tiempo
        tiempoRestante -= Time.deltaTime;

        // Mostrar el tiempo restante
        textoTiempo.text = "Tiempo: " + Mathf.Max(0, Mathf.RoundToInt(tiempoRestante)).ToString();

        if (tiempoRestante <= 0)
        {
            MostrarDerrota();
            return;
        }

        // Solo comprobar fases si no estamos en la misión completada
        if (!canvasMisionCompletada.activeSelf)
        {
            // Comprobar si todos los objetos de la fase actual están clasificados
            if (faseActual < fasesObjetos.Length && TodosClasificados(fasesObjetos[faseActual]))
            {
                DesactivarObjetosDeFase(faseActual);
                faseActual++;
                if (faseActual >= fasesObjetos.Length)
                {
                    MostrarVictoria();
                }
                else
                {
                    IniciarFase();
                }
            }
        }
        else
        {
            // Actualizar el estado del canvas de misión completada
            UpdateMisionCompletada();
        }
    }

    void MostrarTodosLosObjetos()
    {
        foreach (var fase in fasesObjetos)
        {
            foreach (GameObject objeto in fase)
            {
                objeto.SetActive(true); // Mostrar todos los objetos al inicio
            }
        }
    }

    void IniciarFase()
    {
        mensaje.text = "Separa primero las " + ObtenerNombreDeFase(faseActual);
        mensaje.gameObject.SetActive(true);
        ActivarObjetosDeFase(faseActual);
    }

    void ActivarObjetosDeFase(int fase)
    {
        // Activar solo los objetos de la fase actual
        foreach (GameObject objeto in fasesObjetos[fase])
        {
            objeto.SetActive(true);
        }
    }

    void DesactivarObjetosDeFase(int fase)
    {
        // Desactivar los objetos de la fase que se ha completado
        foreach (GameObject objeto in fasesObjetos[fase])
        {
            objeto.SetActive(false);
        }
    }

    string ObtenerNombreDeFase(int fase)
    {
        string[] nombresFases = { "jeringas", "cubrebocas", "guantes", "alcohol", "gel antibacterial" };
        return nombresFases[fase];
    }

    bool TodosClasificados(List<GameObject> objetos)
    {
        foreach (GameObject objeto in objetos)
        {
            if (!EnContenedor(objeto))
                return false;
        }
        return true;
    }

    public bool EnContenedor(GameObject objeto)
    {
        RectTransform contenedorRect = contenedorClasificacion.GetComponent<RectTransform>();
        RectTransform objetoRect = objeto.GetComponent<RectTransform>();

        return RectTransformUtility.RectangleContainsScreenPoint(contenedorRect, objetoRect.position, mainCamera);
    }

    public bool EsObjetoDeFaseActual(GameObject objeto)
    {
        foreach (GameObject obj in fasesObjetos[faseActual])
        {
            if (obj == objeto) // Comparar la referencia del objeto
                return true;
        }
        return false;
    }

    void MostrarVictoria()
    {
        mensaje.text = "¡Has ganado!";
        mensaje.gameObject.SetActive(false);
        textoTiempo.gameObject.SetActive(false);
        canvasVictoria.SetActive(true);
        PausarJuego();
    }

    void MostrarDerrota()
    {
        mensaje.text = "Se acabó el tiempo. Reintentar.";
        mensaje.gameObject.SetActive(false);
        textoTiempo.gameObject.SetActive(false);
        canvasDerrota.SetActive(true);
        PausarJuego();
    }

    void PausarJuego()
    {
        Time.timeScale = 0f; // Pausar el juego
    }

    public void Reintentar()
    {
        Time.timeScale = 1f; // Reanudar el juego antes de reiniciar
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Continuar()
    {
        canvasVictoria.SetActive(false); // Desactivar el canvas de victoria
        canvasMisionCompletada.SetActive(true); // Activar el canvas de misión completada
        PausarJuego();
    }

    void UpdateMisionCompletada()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1f; // Reanudar el juego antes de cargar la siguiente escena
            SceneManager.LoadScene(escenaSiguiente);
        }
    }
}
