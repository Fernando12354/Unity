using UnityEngine;
using UnityEngine.UI;

public class ScriptLibroDatos : MonoBehaviour
{
    public GameObject infoPanel; // Panel que cubre la pantalla
    public Text infoText; // Componente de texto para mostrar la información del virus
    public string[] virusFacts; // Array de datos curiosos sobre los virus
    public Button continueButton; // Botón para continuar el juego

    private int currentFactIndex = 0; // Índice del dato actual
    private bool isPaused = false;

    void Start()
    {
        infoPanel.SetActive(false); // Comenzar con el panel oculto
        continueButton.onClick.AddListener(ContinueGame);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowVirusInfo();
        }
    }

    void ShowVirusInfo()
    {
        if (currentFactIndex < virusFacts.Length)
        {
            infoText.text = virusFacts[currentFactIndex]; // Mostrar el dato actual
            infoPanel.SetActive(true); // Mostrar el panel
            Time.timeScale = 0f; // Pausar el juego
            isPaused = true;
            gameObject.SetActive(false); // Ocultar el objeto libro

            // Cambiar la resolución cuando se muestra el panel
            Screen.SetResolution(3840, 2160, FullScreenMode.FullScreenWindow); // Resolución en pantalla completa
        }
    }

    public void ContinueGame() // Cambiar a public
    {
        infoPanel.SetActive(false); // Ocultar el panel
        Time.timeScale = 1f; // Reanudar el juego
        isPaused = false;

        currentFactIndex++; // Pasar al siguiente dato

        // Cambiar la resolución cuando se oculta el panel
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow); // Cambiar a otra resolución
    }

    void Update()
    {
        if (isPaused && Input.GetKeyDown(KeyCode.C))
        {
            ContinueGame();
        }
    }
}

