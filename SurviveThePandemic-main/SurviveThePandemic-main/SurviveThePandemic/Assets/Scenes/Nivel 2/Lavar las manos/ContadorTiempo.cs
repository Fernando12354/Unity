using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContadorTiempo : MonoBehaviour
{
    public float tiempoLimite = 60f;
    public Text textoTiempo;
    public Image burbujas;
    public GameObject pantallaVictoria;
    public GameObject pantallaDerrota;

    void Update()
    {
        // Actualizar el tiempo
        tiempoLimite -= Time.deltaTime;
        textoTiempo.text = "Tiempo: " + Mathf.Round(tiempoLimite).ToString();

        if (tiempoLimite <= 0)
        {
            // Si el tiempo se acaba y no está lleno de burbujas, perder
            if (burbujas.color.a < 1)
            {
                Perder();
            }
        }
        else if (burbujas.color.a >= 1)
        {
            // Si se llena de burbujas antes de que se acabe el tiempo, ganar
            Ganar();
        }
    }

    void Ganar()
    {
        pantallaVictoria.SetActive(true);
        Time.timeScale = 0f; // Pausa el juego
    }

    void Perder()
    {
        pantallaDerrota.SetActive(true);
        Time.timeScale = 0f; // Pausa el juego
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f; // Restablece el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Continuar()
    {
        Time.timeScale = 1f; // Restablece el tiempo
        SceneManager.LoadScene("NombreDeLaSiguienteEscena"); // Cambia a otra escena
    }
}

