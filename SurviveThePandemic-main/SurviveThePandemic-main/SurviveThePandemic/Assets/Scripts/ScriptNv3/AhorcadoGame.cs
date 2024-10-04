using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AhorcadoGame : MonoBehaviour
{
    public Canvas canvasJuego; // Canvas del juego del ahorcado
    public Text palabraText;
    public Text mensajeFinalText;
    public Button botonReiniciar;
    public GameObject panelFelicidades;
    public GameObject panelGameOver;
    public GameObject guionPrefab; // Prefab del guion (Text o cualquier otro UI Element)
    public Transform panelGuiones; // Contenedor de los guiones

    private string palabraOculta;
    private List<GameObject> guiones = new List<GameObject>(); // Lista de guiones creados
    private int intentosFallidos = 0;
    private int letrasCorrectas = 0;

    private string[] listaPalabras = { "gato", "perro", "elefante" }; // Lista de palabras
    private int maxFallos = 6;

    public Image[] partesAhorcado; // Imagenes del ahorcado

    private void Start()
    {
        canvasJuego.gameObject.SetActive(false); // Asegura que el canvas esté inactivo al inicio
        botonReiniciar.onClick.AddListener(ReiniciarJuego);
    }

    public void ReiniciarJuego()
    {
        intentosFallidos = 0;
        letrasCorrectas = 0;

        // Limpiar el panel de guiones
        foreach (GameObject guion in guiones)
        {
            Destroy(guion);
        }
        guiones.Clear();

        // Seleccionar una nueva palabra
        palabraOculta = listaPalabras[Random.Range(0, listaPalabras.Length)];
        CrearGuiones(palabraOculta.Length);

        // Reiniciar las imágenes del ahorcado
        foreach (var parte in partesAhorcado)
        {
            parte.gameObject.SetActive(false);
        }

        mensajeFinalText.gameObject.SetActive(false);
        panelFelicidades.SetActive(false);
        panelGameOver.SetActive(false);
        palabraText.text = ""; // Limpiar el texto de la palabra oculta
    }

   private void CrearGuiones(int cantidad)
{
    for (int i = 0; i < cantidad; i++)
    {
        // Instanciar un nuevo guion en el panel de guiones
        GameObject nuevoGuion = Instantiate(guionPrefab, panelGuiones);
        nuevoGuion.GetComponent<GuionReceptor>().letraCorrecta = palabraOculta[i]; // Asignar la letra correcta al guion
        guiones.Add(nuevoGuion); // Agregar el guion a la lista
    }

    // Forzar el Layout Group a reestructurarse
    LayoutRebuilder.ForceRebuildLayoutImmediate(panelGuiones.GetComponent<RectTransform>());

    // Ajustar el espaciado entre guiones
    HorizontalLayoutGroup layoutGroup = panelGuiones.GetComponent<HorizontalLayoutGroup>();
    if (layoutGroup != null)
    {
        layoutGroup.spacing = 0; // Ajusta esto a un valor más bajo si es necesario
    }
}


    public void IntentarLetra(bool acierto)
    {
        if (acierto)
        {
            letrasCorrectas++;

            // Verificar si se adivinó toda la palabra
            if (letrasCorrectas == palabraOculta.Length)
            {
                // Mostrar el panel de felicitaciones
                panelFelicidades.SetActive(true);
                mensajeFinalText.gameObject.SetActive(true);
                mensajeFinalText.text = "¡Felicidades, ganaste!";
            }
        }
        else
        {
            intentosFallidos++;
            if (intentosFallidos < partesAhorcado.Length)
            {
                partesAhorcado[intentosFallidos].gameObject.SetActive(true);
            }

            // Verificar si se han agotado los intentos
            if (intentosFallidos >= maxFallos)
            {
                // Mostrar el panel de Game Over
                panelGameOver.SetActive(true);
                mensajeFinalText.gameObject.SetActive(true);
                mensajeFinalText.text = "¡Perdiste! La palabra era: " + palabraOculta;
            }
        }
    }

    public void IniciarJuegoAutomáticamente()
    {
        ReiniciarJuego(); // Reinicia y carga una nueva palabra cuando el jugador entra en la zona
    }
}
