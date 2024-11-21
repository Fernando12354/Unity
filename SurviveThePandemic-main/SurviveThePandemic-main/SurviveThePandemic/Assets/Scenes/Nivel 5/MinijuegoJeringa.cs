using UnityEngine;
using UnityEngine.UI;

public class MinijuegoJeringa : MonoBehaviour
{
    public Texture2D jeringaCursor;           // Imagen del cursor con la jeringa
    public Texture2D defaultCursor;            // Cursor predeterminado para cuando gane o pierda
    public GameObject puntoMeta;               // Objeto de la meta
    public GameObject canvasVictoria;          // Canvas de victoria
    public GameObject canvasDerrota;           // Canvas de derrota
    public GameObject canvasJuego;             // Canvas del minijuego
    public GameObject escenario;               // Objeto del escenario principal
    public Text contadorTexto;                 // Objeto de texto para el contador
    public float tiempoLimite = 30f;           // Tiempo límite del minijuego

    private float tiempoRestante;
    private bool enJuego = false;              // Cambiado a false para que no inicie el juego al principio

    private void Start()
    {
        // Inicializar tiempo y ocultar los canvas de victoria y derrota
        tiempoRestante = tiempoLimite;
        canvasVictoria.SetActive(false);
        canvasDerrota.SetActive(false);
        Cursor.visible = true; // Mostrar el cursor del sistema
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto); // Establecer cursor por defecto
    }

    private void Update()
    {
        if (enJuego)
        {
            // Seguir el cursor con la imagen de la jeringa
            Vector3 cursorPos = Input.mousePosition;

            // Actualizar y mostrar el tiempo restante
            tiempoRestante -= Time.deltaTime;
            contadorTexto.text = "Tiempo: " + Mathf.CeilToInt(tiempoRestante).ToString();

            // Si el tiempo se acaba, el jugador pierde
            if (tiempoRestante <= 0)
            {
                TerminarMinijuego(false); // Derrota por tiempo agotado
            }

            // Verificar si el jugador hace clic izquierdo
            if (Input.GetMouseButtonDown(0))
            {
                // Revisar si hace clic en el punto meta
                if (EsClicEnMeta(cursorPos))
                {
                    TerminarMinijuego(true); // Victoria
                }
                else
                {
                    TerminarMinijuego(false); // Derrota por clic fuera de la meta
                }
            }
        }
    }

    // Método para iniciar el minijuego
    public void IniciarMinijuego()
    {
        enJuego = true; // Comenzar el minijuego
        canvasJuego.SetActive(true); // Activar el canvas del minijuego
        Cursor.visible = false; // Ocultar el cursor del sistema
        SetJeringaCursor(); // Establecer el cursor de jeringa
        tiempoRestante = tiempoLimite; // Restablecer el tiempo
        contadorTexto.text = "Tiempo: " + Mathf.CeilToInt(tiempoRestante).ToString();
    }

    private bool EsClicEnMeta(Vector3 cursorPos)
    {
        // Convertir la posición del cursor a espacio de pantalla del canvas
        RectTransform rectTransform = puntoMeta.GetComponent<RectTransform>();
        Vector2 localPoint;

        // Convertir la posición del cursor a local del RectTransform
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, cursorPos, Camera.main, out localPoint))
        {
            // Revisar si la posición está dentro del área del RectTransform
            return rectTransform.rect.Contains(localPoint);
        }
        return false;
    }

    // Método para terminar el minijuego
    private void TerminarMinijuego(bool gano)
    {
        enJuego = false; // Detener el juego
        Cursor.visible = true; // Mostrar el cursor del sistema
        canvasJuego.SetActive(false); // Ocultar el canvas del minijuego

        // Cambiar el cursor al predeterminado
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);

        if (gano)
        {
            canvasVictoria.SetActive(true); // Mostrar canvas de victoria
        }
        else
        {
            canvasDerrota.SetActive(true); // Mostrar canvas de derrota
        }
    }

    // Método para el botón de Continuar (en el canvas de victoria)
    public void Continuar()
    {
        canvasVictoria.SetActive(false);
        escenario.SetActive(true); // Reactivar el escenario principal
        gameObject.SetActive(false); // Desactivar el minijuego
    }

    // Método para el botón de Reiniciar (en ambos canvas)
    public void Reiniciar()
    {
        // Restablecer el tiempo
        tiempoRestante = tiempoLimite;
        contadorTexto.text = "Tiempo: " + Mathf.CeilToInt(tiempoRestante).ToString();

        // Ocultar los canvas de victoria y derrota
        canvasVictoria.SetActive(false);
        canvasDerrota.SetActive(false);

        // Reactivar el canvas del minijuego
        canvasJuego.SetActive(true);

        // Configurar el cursor de jeringa
        SetJeringaCursor();

        enJuego = true; // Reiniciar el estado del juego
    }

    // Función para establecer el cursor como la jeringa
    private void SetJeringaCursor()
    {
        if (jeringaCursor != null)
        {
            Cursor.SetCursor(jeringaCursor, Vector2.zero, CursorMode.Auto);
            Debug.Log("Cursor de jeringa establecido.");
        }
        else
        {
            Debug.LogWarning("Cursor de jeringa no asignado!");
        }
    }
}
