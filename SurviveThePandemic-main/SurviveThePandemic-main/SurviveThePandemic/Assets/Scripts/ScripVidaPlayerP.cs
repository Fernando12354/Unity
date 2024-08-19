using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScripVidaPlayerP : MonoBehaviour
{
    public float vida = 0; // Inicializa la vida en 0

    [Header("Configuracion Barra de Vida")]
    public Image barraDeVida;
    public TextMeshProUGUI nContagioText;

    // MENU GAME OVER
    [Header("Configuracion Game Over")]
    public GameObject interfaceGameOver;
    public AudioSource Musica;
    public AudioClip CancionGameOver;

    void Start()
    {
        // Asegúrate de que el Game Over no esté activo al iniciar
        interfaceGameOver.SetActive(false);
    }

    void Update()
    {
        // Limitar la vida entre 0 y 100
        vida = Mathf.Clamp(vida, 0, 100);

        // Actualizar la barra de vida de izquierda a derecha
        barraDeVida.fillAmount = vida / 100;

        // Cambiar el color de la barra de verde a rojo
        barraDeVida.color = Color.Lerp(Color.red, Color.green, vida / 100);

        // Actualizar el texto de contagio
        nContagioText.text = (vida) + "%"; // Muestra la vida en lugar de 100 - vida

        // Verificar si la vida es 100 o más
        if (vida >= 100)
        {
            if (!interfaceGameOver.activeSelf)
            {
                Debug.Log(" MUERTO "); // Mensaje para depuración
                interfaceGameOver.SetActive(true);
                if (Musica && CancionGameOver)
                {
                    Musica.Pause();
                    Musica.clip = CancionGameOver;
                    Musica.Play();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si colisiona con un NPC y aumenta la vida
        if (other.CompareTag("NPC"))
        {
            vida += 10; // Ajusta el valor de incremento según sea necesario
        }
    }
}

