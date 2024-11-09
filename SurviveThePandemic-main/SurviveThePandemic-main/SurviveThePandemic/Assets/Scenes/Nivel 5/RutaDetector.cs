using UnityEngine;

public class RutaDetector : MonoBehaviour
{
    public GameObject canvasVictoria;
    public GameObject canvasDerrota;
    public GameObject escenario;
    public GameObject minijuego;

    private bool enRuta = true;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            enRuta = false;
            TerminarMinijuego(false); // Llama a la función de pérdida
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Meta") && Input.GetMouseButton(0) && enRuta)
        {
            TerminarMinijuego(true); // Llama a la función de victoria
        }
    }

    private void TerminarMinijuego(bool gano)
    {
        minijuego.SetActive(false);

        if (gano)
        {
            canvasVictoria.SetActive(true);
            // Lógica para continuar o reiniciar
        }
        else
        {
            canvasDerrota.SetActive(true);
        }
    }
}

