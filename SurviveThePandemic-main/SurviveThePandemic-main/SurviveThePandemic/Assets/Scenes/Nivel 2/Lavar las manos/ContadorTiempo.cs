using UnityEngine;
using TMPro; // Importar TextMesh Pro
using UnityEngine.SceneManagement;
using System.Collections;


public class ContadorTiempo : MonoBehaviour
{
    public float tiempoLimite = 60f;
    public TMP_Text textoTiempo; // Cambiar a TMP_Text para usar TextMesh Pro
    public GameObject pantallaVictoria;
    public GameObject pantallaDerrota;
    public GameObject juego;
    public int cantidadMaximaBurbujas = 10; // Número de burbujas necesarias para ganar
    public string nombreSiguienteEscena; // Escena que se cambiará al ganar, ajustable desde el inspector

    private GenerarBurbujas generadorBurbujas; // Referencia al script de generación de burbujas

    void Start()
    {
        // Obtener la referencia al script GeneradorBurbujas
        generadorBurbujas = FindObjectOfType<GenerarBurbujas>();

        // Mantener las pantallas de victoria y derrota desactivadas al iniciar el juego
        pantallaVictoria.SetActive(false);
        pantallaDerrota.SetActive(false);
        juego.SetActive(true);
    }

    void Update()
    {
        // Actualizar el tiempo
        tiempoLimite -= Time.deltaTime;
        textoTiempo.text = "Tiempo: " + Mathf.Round(tiempoLimite).ToString(); // Actualiza el texto con TextMesh Pro

        // Verificar si se acabó el tiempo
        if (tiempoLimite <= 0)
        {
            // Si el tiempo se acaba y no se ha alcanzado el número necesario de burbujas, perder
            if (generadorBurbujas.burbujasInstanciadas < cantidadMaximaBurbujas) 
            {
                Perder();
            }
        }
        else if (generadorBurbujas.burbujasInstanciadas >= cantidadMaximaBurbujas)
        {
            // Si se alcanzan las burbujas necesarias antes de que se acabe el tiempo, ganar
            Ganar();
        }
    }

    void Ganar()
    {
        pantallaVictoria.SetActive(true);
        juego.SetActive(false);
        Time.timeScale = 0f; // Pausa el juego
    }

    void Perder()
    {
        pantallaDerrota.SetActive(true);
        juego.SetActive(false);
        Time.timeScale = 0f; // Pausa el juego
    }

    // Función para reiniciar el juego, asignable desde el botón en el Inspector
    public void Reiniciar()
    {
        Time.timeScale = 1f; // Restablece el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual
    }

   public void Continuar()
{
    if (!string.IsNullOrEmpty(nombreSiguienteEscena))
    {
        Time.timeScale = 1f; // Restablece el tiempo antes de cargar la nueva escena
        SceneManager.LoadScene(nombreSiguienteEscena); // Cambia a la siguiente escena
    }
    else
    {
        Debug.LogWarning("El nombre de la siguiente escena no ha sido asignado.");
    }
}


}
