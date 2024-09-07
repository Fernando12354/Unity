using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CoinCollector : MonoBehaviour
{
    public int totalCoinsRequired = 10; // Número total de monedas requeridas
    public TextMeshProUGUI coinText; // Texto UI para mostrar el contador de monedas usando TextMeshPro
    public string sceneToLoad; // Nombre de la escena que se cargará
    public GameObject specialObject; // Nuevo objeto que se activará al recoger las monedas

    private bool objectActivated = false; // Para evitar activar el objeto más de una vez

    private void Start()
    {
        if (coinText == null)
        {
            Debug.LogError("TextMeshProUGUI para el contador de monedas no asignado.");
        }

        if (specialObject == null)
        {
            Debug.LogError("El objeto especial no está asignado.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckCoins();
    }

    // Método para verificar el número de monedas actuales
    private void CheckCoins()
    {
        if (coinText != null)
        {
            // Obtener el texto del contador y extraer el número de monedas
            string coinTextString = coinText.text;
            int currentCoins;

            // Aquí asumimos que el texto solo contiene el número de monedas
            if (int.TryParse(coinTextString, out currentCoins))
            {
                if (currentCoins >= totalCoinsRequired)
                {
                    if (!objectActivated && specialObject != null)
                    {
                        specialObject.SetActive(true); // Activa el objeto especial
                        objectActivated = true; // Asegura que el objeto solo se active una vez
                    }
                }
            }
            else
            {
                Debug.LogError("El texto del contador de monedas no está en el formato esperado.");
            }
        }
        else
        {
            Debug.LogError("El TextMeshProUGUI no está asignado.");
        }
    }

    // Carga la nueva escena si el jugador activa el trigger con el objeto
    private void OnTriggerEnter(Collider other)
    {
        if (objectActivated && other.CompareTag("Player"))
        {
            LoadNextScene();
        }
    }

    // Carga la nueva escena
    private void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
