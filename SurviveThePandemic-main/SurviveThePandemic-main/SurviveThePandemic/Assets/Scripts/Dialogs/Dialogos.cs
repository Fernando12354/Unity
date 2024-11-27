/*using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialogos : MonoBehaviour
{
    public GameObject canvasGuia; // Canvas Guia
    public TextMeshProUGUI textD;
    [TextArea(30, 3)]
    public string[] parrafos;
    public Sprite[] ayudaVisual;
    private int index = 0;
    public float velParrafo;
    public GameObject componenteCompleto;

    public GameObject botonContinue;
    public GameObject botonQuitar;

    public GameObject panelDialogo;
    public GameObject botonLeer;
    public Button buttonChange;

    

    private Coroutine currentCoroutine;

    void Start()
    {
        botonContinue.SetActive(false);
        botonQuitar.SetActive(false);
        botonLeer.SetActive(true);
        panelDialogo.SetActive(true);
        currentCoroutine = StartCoroutine(TextDialogo());
        canvasGuia.SetActive(false);
    }

    void Update()
    {
        if (textD.text == parrafos[index])
        {
            botonContinue.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            siguienteParrafo();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            botonCerrar();
        }
    }

    IEnumerator TextDialogo()
    {
        if (index < parrafos.Length && index < ayudaVisual.Length)
        {
            Debug.Log($"Mostrando párrafo {index}: {parrafos[index]}");

            textD.text = "";  // Reiniciar el texto antes de empezar a mostrarlo
            buttonChange.image.sprite = ayudaVisual[index];

            foreach (char letra in parrafos[index].ToCharArray())
            {
                textD.text += letra;
                yield return new WaitForSeconds(velParrafo);
            }

            Debug.Log("Texto completo mostrado");
        }
        else
        {
            Debug.LogError("Índice fuera de rango");
        }
    }

    public void siguienteParrafo()
    {
        botonContinue.SetActive(false);
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        if (index < parrafos.Length - 1)
        {
            index++;
            currentCoroutine = StartCoroutine(TextDialogo());
        }
        else
        {
            textD.text = "";
            botonContinue.SetActive(false);
            botonQuitar.SetActive(true);
        }
    }

    public void activarBotonLeer()
    {
        panelDialogo.SetActive(true);
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(TextDialogo());
    }

    public void botonCerrar()
    {
        panelDialogo.SetActive(false);
        botonLeer.SetActive(false);
        canvasGuia.SetActive(true);
    }
}
*/
using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialogos : MonoBehaviour
{
    public GameObject canvasGuia; // Canvas Guia
    public TextMeshProUGUI textD;
    [TextArea(30, 3)]
    public string[] parrafos;
    public Sprite[] ayudaVisual;
    private int index = 0;
    public float velParrafo;
    public GameObject componenteCompleto;

    public GameObject botonContinue;
    public GameObject botonQuitar;

    public GameObject panelDialogo;
    public GameObject botonLeer;
    public Button buttonChange;

    // Elementos personalizados de finalización
    [TextArea(3, 3)]
    public string mensajeFinal = "¡Gracias por leer!";
    public Sprite spriteFinal;

    private Coroutine currentCoroutine;

    // Nueva bandera
    private bool dialogosCompletados = false;

    void Start()
    {
        // Congelar la escena desde el inicio
        Time.timeScale = 0;

        botonContinue.SetActive(false);
        botonQuitar.SetActive(false);
        botonLeer.SetActive(true);
        panelDialogo.SetActive(true);
        currentCoroutine = StartCoroutine(TextDialogo());
        canvasGuia.SetActive(false);
    }

    void Update()
    {
        if (textD.text == parrafos[index])
        {
            botonContinue.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            siguienteParrafo();
        }

        // Solo permitir usar S cuando los diálogos hayan terminado
        if (Input.GetKeyDown(KeyCode.S) && dialogosCompletados)
        {
            botonCerrar();
        }
    }

    IEnumerator TextDialogo()
    {
        // Si hemos llegado al final de los diálogos y sprites, mostrar mensaje y sprite personalizados
        if (index >= parrafos.Length || index >= ayudaVisual.Length)
        {
            MostrarFinal();
            yield break;
        }

        textD.text = ""; // Reiniciar el texto antes de empezar a mostrarlo
        buttonChange.image.sprite = ayudaVisual[index];

        foreach (char letra in parrafos[index].ToCharArray())
        {
            textD.text += letra;
            yield return new WaitForSecondsRealtime(velParrafo); // Usar tiempo real
        }
    }

    public void siguienteParrafo()
    {
        botonContinue.SetActive(false);
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        if (index < parrafos.Length - 1)
        {
            index++;
            currentCoroutine = StartCoroutine(TextDialogo());
        }
        else
        {
            MostrarFinal();
        }
    }

    private void MostrarFinal()
    {
        // Mostrar el mensaje y el sprite personalizados al final
        textD.text = mensajeFinal;
        buttonChange.image.sprite = spriteFinal;

        // Desactivar el botón "Continuar" y activar el botón "Quitar" al final
        botonContinue.SetActive(false);
        botonQuitar.SetActive(true);

        // Activar la bandera de finalización
        dialogosCompletados = true;
    }

    public void activarBotonLeer()
    {
        panelDialogo.SetActive(true);
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(TextDialogo());
    }

    public void botonCerrar()
    {
        // Reanudar la escena
        Time.timeScale = 1;

        panelDialogo.SetActive(false);
        botonLeer.SetActive(false);
        canvasGuia.SetActive(true);
    }
}



