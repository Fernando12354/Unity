using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class ContadorTiempo : MonoBehaviour
{
    public float tiempoLimite = 60f;
    public TMP_Text textoTiempo;
    public GameObject pantallaVictoria;
    public GameObject pantallaDerrota;
    public GameObject juego;
    public int cantidadMaximaBurbujas = 10;
    public string nombreSiguienteEscena;

    private int burbujasInstanciadas = 0;
    private GenerarBurbujas generadorBurbujas;

    void Start()
    {
        generadorBurbujas = FindObjectOfType<GenerarBurbujas>();
        pantallaVictoria.SetActive(false);
        pantallaDerrota.SetActive(false);
        juego.SetActive(true);
    }

    void Update()
    {
        tiempoLimite -= Time.deltaTime;
        textoTiempo.text = "Tiempo: " + Mathf.Round(tiempoLimite).ToString();

        if (tiempoLimite <= 0)
        {
            if (burbujasInstanciadas < cantidadMaximaBurbujas)
            {
                Perder();
            }
        }
        else if (burbujasInstanciadas >= cantidadMaximaBurbujas)
        {
            Ganar();
        }

        // Lógica de creación de burbuja con clic izquierdo
        if (Input.GetMouseButtonDown(0) && generadorBurbujas.EstaSobreLasManos())
        {
            generadorBurbujas.CrearBurbuja();
            burbujasInstanciadas++;
            Debug.Log("Burbujas instanciadas: " + burbujasInstanciadas);
        }
    }

    void Ganar()
    {
        Time.timeScale = 0f;
        pantallaVictoria.SetActive(true);
        juego.SetActive(false);
      
    }

    void Perder()
    {
        pantallaDerrota.SetActive(true);
        juego.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Continuar()
    {
        Time.timeScale = 1f;
        if (!string.IsNullOrEmpty(nombreSiguienteEscena))
        {
            
            SceneManager.LoadScene(nombreSiguienteEscena);
        }
        else
        {
            Debug.LogWarning("El nombre de la siguiente escena no ha sido asignado.");
        }
    }
}
