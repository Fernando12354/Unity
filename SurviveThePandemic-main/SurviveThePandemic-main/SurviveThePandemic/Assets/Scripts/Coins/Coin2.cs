using System.Collections;
using UnityEngine;
using TMPro;

public class Coin2 : MonoBehaviour
{
    [Header("Coin")]
    public GameObject ContenedorPadre;
    public AudioSource AudioCoin;
    public AudioClip take_sound;
    public TextMeshProUGUI contenedorTexto;
    public GameObject PlayerNormal;           // Referencia al jugador normal
    public GameObject PlayerCubrebocas;       // Referencia al jugador con cubrebocas

    [Header("Next Coin")]
    public GameObject NextCoin;

    private bool coin_working = false;
    private GameObject currentPlayer;         // Jugador actual activo

    // Contador universal de monedas
    private static int totalCoins = 0;

    void Start()
    {
        // Inicializamos el jugador actual como el normal
        currentPlayer = PlayerNormal;
    }

    void Update()
    {
        // Detectamos si el jugador ha cambiado y actualizamos el jugador actual
        if (PlayerNormal.activeSelf)
        {
            currentPlayer = PlayerNormal;
        }
        else if (PlayerCubrebocas.activeSelf)
        {
            currentPlayer = PlayerCubrebocas;
        }

        // Actualizamos el texto con el contador de monedas
        contenedorTexto.text = " " + totalCoins;
    }

    private IEnumerator OnTriggerStay(Collider other)
    {
        // Detectar si el trigger fue activado por el jugador actual
        if ((other.gameObject == PlayerNormal || other.gameObject == PlayerCubrebocas) && coin_working != true)
        {
            coin_working = true;
            AudioCoin.Pause();
            AudioCoin.loop = false;
            AudioCoin.clip = take_sound;
            AudioCoin.Play(0);

            // Incrementar el contador de monedas universal
            totalCoins++;

            // Actualizar el texto inmediatamente después de recoger una moneda
            contenedorTexto.text = " " + totalCoins;

            yield return new WaitForSeconds(0.8f);
            ContenedorPadre.SetActive(false); // Desactivar la moneda
        }
    }
}

