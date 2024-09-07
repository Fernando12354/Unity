using UnityEngine;
using TMPro;

public class InfoTrigger : MonoBehaviour
{
    public GameObject infoCanvas; // Canvas que contiene el texto y el botón.
    public TextMeshProUGUI infoText; // El componente de texto en el canvas.
    public string[] infoArray = new string[10]; // Arreglo que contiene la información de cada objeto.
    public int objectIndex; // Índice que asignarás manualmente a cada objeto en el Inspector.
    private bool isInTrigger; // Verifica si el jugador está en el área de activación.
    private bool isCanvasActive; // Para verificar si el canvas está activo.

    private void Start()
    {
        infoCanvas.SetActive(false); // Asegúrate de que el canvas esté oculto al inicio.

        // Información para cada objeto, basado en tus datos.
        infoArray[0] = "El bocavirus es un virus que puede causar problemas respiratorios como tos.";
        infoArray[1] = "El adenovirus puede causar fiebre, tos y dolor de garganta.";
        infoArray[2] = "El rinovirus es un virus común que afecta las vías respiratorias.";
        infoArray[3] = "El virus de Epstein puede causar mononucleosis infecciosa.";
        infoArray[4] = "El hantavirus se encuentra en los ratones y puede hacerte enfermar.";
        infoArray[5] = "El virus de la parainfluenza se transmite a través del contacto con secreciones respiratorias.";
        infoArray[6] = "El rinovirus NO causa dolor en el estómago.";
        infoArray[7] = "RSV significa Virus Sincitial Respiratorio.";
        infoArray[8] = "La influenza H1N1 puede causar fiebre, tos, dolor de garganta, dolores musculares, entre otros.";
        infoArray[9] = "El hantavirus es un tipo de virus que puede encontrarse en roedores.";

        // Desactiva el canvas al inicio.
        infoCanvas.SetActive(false);
    }

    private void Update()
    {
        // Si el canvas está activo y se presiona la tecla Enter, se cierra el canvas
        if (isCanvasActive && Input.GetKeyDown(KeyCode.Return))
        {
            CloseInfoCanvas();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador es quien activa el trigger.
        {
            isInTrigger = true;
            // Muestra la información basada en el índice del objeto.
            infoText.text = infoArray[objectIndex];
            infoCanvas.SetActive(true); // Muestra el canvas.
            isCanvasActive = true; // El canvas está activo.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Cuando el jugador salga del trigger, oculta el canvas.
        {
            isInTrigger = false;
            infoCanvas.SetActive(false);
            isCanvasActive = false; // El canvas ya no está activo.
        }
    }

    // Este método se llamará al presionar el botón para cerrar el canvas.
    public void CloseInfoCanvas()
    {
        infoCanvas.SetActive(false);
        isCanvasActive = false; // El canvas ya no está activo.
    }
}
