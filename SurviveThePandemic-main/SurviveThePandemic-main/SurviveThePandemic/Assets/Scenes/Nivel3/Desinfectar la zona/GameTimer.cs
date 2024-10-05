using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float gameTime = 60f; // Tiempo total en segundos
    public TextMeshProUGUI timerText; // Referencia a TextMeshPro para mostrar el tiempo
    public GameObject winCanvas; // Canvas de victoria
    public GameObject loseCanvas; // Canvas de derrota
    public string nextSceneName; // Nombre de la siguiente escena, configurable desde el inspector
    public Texture2D spongeCursor; // Cursor de la esponja para el juego
    public Texture2D defaultCursor; // Cursor predeterminado para cuando gane o pierda
    public int sortingOrderOnWin = 10; // Valor para el orden de capas cuando se gana
    public int sortingOrderOnLose = 10; // Valor para el orden de capas cuando se pierde

    private int objectsToClean; // Cantidad de objetos a limpiar
    private bool gameEnded = false; // Estado del juego

    void Start()
    {
        // Contar cuántos objetos hay para limpiar
        objectsToClean = FindObjectsOfType<CleanableObject>().Length;
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);

        // Asegurarse de que el cursor es la esponja al comenzar el juego
        SetSpongeCursor();
    }

    void Update()
    {
        if (!gameEnded)
        {
            gameTime -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Round(gameTime).ToString(); // Usando TextMeshPro para mostrar el tiempo

            if (gameTime <= 0)
            {
                EndGame(false); // Si se acaba el tiempo y no limpió todo
            }
        }
    }

    // Llamado cuando un objeto de suciedad es limpiado
    public void ObjectCleaned()
    {
        objectsToClean--;
        Debug.Log("Cleaned an object. Remaining: " + objectsToClean); // Debug
        if (objectsToClean <= 0)
        {
            Debug.Log("All objects cleaned. Winning the game.");
            EndGame(true); // Si limpia todos los objetos antes de que acabe el tiempo
        }
    }

    void EndGame(bool win)
    {
        if (gameEnded) return; // Evitar que EndGame se llame varias veces
        gameEnded = true;

        // Cambiar el cursor al predeterminado
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None; // El cursor vuelve a ser libre

        Time.timeScale = 1f; // Asegurar la interacción de los botones

        if (win)
        {
            Debug.Log("Game won! Showing win canvas.");
            SetCanvasSortingOrder(winCanvas, sortingOrderOnWin); // Ajustar el orden de capas del canvas de victoria
            winCanvas.SetActive(true); // Mostrar pantalla de victoria
        }
        else
        {
            Debug.Log("Game lost! Showing lose canvas.");
            SetCanvasSortingOrder(loseCanvas, sortingOrderOnLose); // Ajustar el orden de capas del canvas de derrota
            loseCanvas.SetActive(true); // Mostrar pantalla de derrota
            DestroyAllDirt(); // Destruir los objetos de suciedad si pierde
        }
    }

    // Función para destruir todos los objetos de suciedad al perder
    void DestroyAllDirt()
    {
        CleanableObject[] dirtObjects = FindObjectsOfType<CleanableObject>();
        foreach (CleanableObject dirt in dirtObjects)
        {
            Destroy(dirt.gameObject);
        }
    }

    // Función de reintentar para los botones de derrota y victoria
    public void Retry()
    {
        Debug.Log("Retry button clicked.");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reiniciar la escena actual
    }

    // Función de continuar para el botón de victoria
    public void Continue()
    {
        Debug.Log("Continue button clicked.");
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextSceneName); // Cambiar a la escena especificada en el inspector
    }

    // Función para ajustar el orden de capas del Canvas
    void SetCanvasSortingOrder(GameObject canvas, int sortingOrder)
    {
        Canvas canvasComponent = canvas.GetComponent<Canvas>();
        if (canvasComponent != null)
        {
            canvasComponent.sortingOrder = sortingOrder; // Establecer el orden de capas
            Debug.Log("Canvas sorting order set to: " + sortingOrder);
        }
        else
        {
            Debug.LogWarning("No Canvas component found on: " + canvas.name);
        }
    }

    // Función para establecer el cursor como la esponja
    void SetSpongeCursor()
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
