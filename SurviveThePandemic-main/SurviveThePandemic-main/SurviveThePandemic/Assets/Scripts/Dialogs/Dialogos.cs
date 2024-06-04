using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialogos : MonoBehaviour
{
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
    }
}


