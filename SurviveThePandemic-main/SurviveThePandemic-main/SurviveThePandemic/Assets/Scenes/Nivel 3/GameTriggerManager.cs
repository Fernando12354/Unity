using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTriggerManager : MonoBehaviour
{
    [Header("Objetos de la escena")]
    public GameObject escenario;  // Objeto del escenario inicial
    public GameObject juego;      // Objeto que contiene el juego con los canvas y temporizador

    private GameTimer gameTimer;  // Referencia al script GameTimer

    private void Start()
    {
        // Buscar el componente GameTimer en el objeto de juego
        gameTimer = juego.GetComponent<GameTimer>();

        // Asegurarse de que al inicio de la escena el juego esté desactivado
        juego.SetActive(false);
    }

    // Función para activar el juego y desactivar el escenario
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verificar que el trigger lo active el jugador
        {
            // Desactivar el objeto del escenario y activar el objeto del juego
            escenario.SetActive(false);
            juego.SetActive(true);

            // Iniciar el temporizador cuando el juego se activa
            gameTimer.gameEnded = false; // Resetear el estado del juego si es necesario
        }
    }

    // Función para el botón de "Continuar"
    public void ContinuarJuego()
    {
        // Desactivar el juego y detener el temporizador
        juego.SetActive(false);
        
        // Opcional: Cargar la siguiente escena si se ha ganado el juego
        SceneManager.LoadScene(gameTimer.nextSceneName);
    }

    // Función para el botón de "Reintentar"
    public void ReintentarJuego()
    {
        // Reiniciar el estado del juego (opcional)
        juego.SetActive(false); // Desactivar temporalmente para reiniciar el estado

        // Reiniciar la escena para reintentar el juego desde el inicio
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

