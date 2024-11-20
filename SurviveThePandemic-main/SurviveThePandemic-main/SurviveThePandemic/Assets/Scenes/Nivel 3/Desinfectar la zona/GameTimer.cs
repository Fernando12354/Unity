using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float gameTime; // Tiempo total en segundos, configurable en el inspector
    public TextMeshProUGUI timerText; // Referencia a TextMeshPro para mostrar el tiempo
    public GameObject winCanvas; // Canvas de victoria
    public GameObject loseCanvas; // Canvas de derrota
    public GameObject juego; // Objeto del minijuego
    public GameObject escenario; // Objeto del escenario
    public Texture2D spongeCursor; // Cursor de la esponja para el juego
    public Texture2D defaultCursor; // Cursor predeterminado para cuando gane o pierda
    public GameObject[] dirtObjects; // Array de objetos de suciedad
    public int sortingOrderOnWin = 10; // Orden de capas para el canvas de victoria
    public int sortingOrderOnLose = 10; // Orden de capas para el canvas de derrota
    public GameObject escenarioTrigger; // Referencia pública al objeto con el trigger

    private int objectsToClean; // Cantidad de objetos a limpiar
    public bool gameEnded = false; // Estado del juego
    private float initialGameTime; // Almacena el tiempo inicial definido en el inspector

    void Start()
    {
        initialGameTime = gameTime; // Guardar el tiempo configurado en el inspector
        InitializeMinigame(); // Configurar el minijuego al inicio
    }

    void Update()
    {
        if (!gameEnded)
        {
            gameTime -= Time.deltaTime;
            timerText.text = "Tiempo: " + Mathf.Round(gameTime).ToString(); // Mostrar tiempo con TextMeshPro

            if (gameTime <= 0)
            {
                EndGame(false); // Finalizar juego al agotarse el tiempo
            }
        }
    }

     // Función de continuar para el botón de victoria
    public void Continue()
{
    Debug.Log("Continue button clicked.");
    Time.timeScale = 1f;

    // Desactivar el objeto de juego (minijuego) y volver a activar el escenario
    if (juego != null)
    {
        juego.SetActive(false);
    }
    if (escenario != null)
    {
        escenario.SetActive(true);

        // Desactivar el collider del objeto que tiene el trigger
        if (escenarioTrigger != null)
        {
            Collider triggerCollider = escenarioTrigger.GetComponent<Collider>();
            if (triggerCollider != null)
            {
                triggerCollider.enabled = false; // Desactivar el collider para evitar que active el trigger nuevamente
                Debug.Log("Collider desactivado en el objeto escenarioTrigger.");
            }
        }
    }

    // Detener el temporizador
    gameEnded = true;
    Debug.Log("Game ended and returned to scenario.");
}

    // Configurar el minijuego al inicio o al reiniciar
    private void InitializeMinigame()
    {
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);
        gameEnded = false;

        // Restablecer el tiempo inicial
        gameTime = initialGameTime;

        // Reactivar todos los objetos de suciedad
        objectsToClean = 0;
        foreach (GameObject dirt in dirtObjects)
        {
            if (dirt != null)
            {
                dirt.SetActive(true); // Reactivar los objetos
                objectsToClean++;
            }
        }

        SetSpongeCursor(); // Restablecer el cursor de la esponja
        Debug.Log("Minigame initialized. Objects to clean: " + objectsToClean);
    }

    // Llamado cuando un objeto de suciedad es limpiado
    public void ObjectCleaned(GameObject dirtObject)
    {
        if (dirtObject != null)
        {
            dirtObject.SetActive(false); // Desactivar el objeto en lugar de destruirlo
            objectsToClean--;
            Debug.Log("Cleaned an object. Remaining: " + objectsToClean);
        }

        if (objectsToClean <= 0)
        {
            Debug.Log("All objects cleaned. Winning the game.");
            EndGame(true); // Finalizar juego al limpiar todos los objetos
        }
    }

    void EndGame(bool win)
    {
        if (gameEnded) return; // Evitar múltiples llamadas a EndGame
        gameEnded = true;

        // Cambiar el cursor al predeterminado
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;

        if (win)
        {
            Debug.Log("Game won! Showing win canvas.");
            SetCanvasSortingOrder(winCanvas, sortingOrderOnWin);
            winCanvas.SetActive(true);
        }
        else
        {
            Debug.Log("Game lost! Showing lose canvas.");
            SetCanvasSortingOrder(loseCanvas, sortingOrderOnLose);
            loseCanvas.SetActive(true);
        }
    }

    // Función de reintentar para reiniciar el minijuego
    public void Retry()
    {
        Debug.Log("Retry button clicked. Restarting the minigame.");
        InitializeMinigame(); // Reiniciar el minijuego
    }

    // Ajustar el orden de capas del canvas
    private void SetCanvasSortingOrder(GameObject canvas, int sortingOrder)
    {
        Canvas canvasComponent = canvas.GetComponent<Canvas>();
        if (canvasComponent != null)
        {
            canvasComponent.sortingOrder = sortingOrder;
            Debug.Log("Canvas sorting order set to: " + sortingOrder);
        }
        else
        {
            Debug.LogWarning("No Canvas component found on: " + canvas.name);
        }
    }

    // Restablecer el cursor a la esponja
    private void SetSpongeCursor()
    {
        if (spongeCursor != null)
        {
            Cursor.SetCursor(spongeCursor, Vector2.zero, CursorMode.Auto);
            Debug.Log("Sponge cursor set.");
        }
        else
        {
            Debug.LogWarning("Sponge cursor not assigned!");
        }
    }
}
