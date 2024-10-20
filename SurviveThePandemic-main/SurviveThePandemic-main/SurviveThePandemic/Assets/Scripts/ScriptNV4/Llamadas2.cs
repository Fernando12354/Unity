using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Llamadas2 : MonoBehaviour
{
    public bool callAnswer = false;
    public bool callSound = false;

    [Header("Notificaciones")]
    public GameObject Notificacion;
    public Transform AnimateNotification;
    public TextMeshProUGUI txtNotificacion;
    public string textoNotificacion = "Presiona X para contestar";

    [Header("Llamadas")]
    public AudioSource Phone;
    public AudioClip vozCaller;
    public AudioClip vozAnswer;
    public GameObject interfaceLlamadas;
    public Transform AnimateCall;
    public TextMeshProUGUI contenedorTexto;
    public Llamada2[] llamadas;

    [Header("Configuración de Teclado")]
    public ConfigDialogos configuracion;

    void Start()
    {
        interfaceLlamadas.SetActive(false);
        Notificacion.SetActive(false);
        Phone.Stop(); // Asegurarse de que el teléfono esté apagado al inicio
    }

    public void ActivarLlamada()
    {
        callAnswer = false;
        callSound = false;

        Notificacion.SetActive(false);
        interfaceLlamadas.SetActive(false);

        Debug.Log("Llamada activada");
        StartCoroutine(StartCall());
    }

    private IEnumerator StartCall()
    {
        Debug.Log("Iniciando llamada...");
        yield return new WaitForSeconds(2);

        Phone.Play();
        callSound = true;

        txtNotificacion.text = textoNotificacion;
        Notificacion.SetActive(true);

        AnimateNotification.localPosition = new Vector2(0, -Screen.height);
        AnimateNotification.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;
    }

    void Update()
    {
        if (callSound && !callAnswer && Input.GetKeyDown(configuracion.ContestarLlamada))
        {
            Debug.Log("Contestando llamada...");
            StartCoroutine(AnswerCall());
            callAnswer = true;
        }
    }

    private IEnumerator AnswerCall()
    {
        interfaceLlamadas.SetActive(true);

        AnimateCall.localPosition = new Vector2(0, -Screen.height);
        AnimateCall.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;

        Phone.Pause();
        AnimateNotification.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo();
        yield return new WaitForSeconds(1);

        Notificacion.SetActive(false);
        StartCoroutine(SimularDialogos(llamadas[0].dialogos, llamadas[0].answer, llamadas[0].caller));
    }

    private IEnumerator SimularDialogos(Dialogo2[] dialogos, string answer, string caller)
    {
        foreach (var dialogo in dialogos)
        {
            contenedorTexto.text = "";
            Phone.clip = dialogo.talker == caller ? vozCaller : vozAnswer;
            Phone.Play();

            for (int i = 0; i <= dialogo.text.Length; i++)
            {
                yield return new WaitForSeconds(configuracion.tiempoLetra);

                if (Input.GetKey(configuracion.teclaSkip) || Input.GetKey(configuracion.teclaSkip2))
                {
                    i = dialogo.text.Length;
                }

                contenedorTexto.text = $"{dialogo.talker}: {dialogo.text.Substring(0, i)}";
            }

            yield return new WaitForSeconds(0.5f);
            Phone.Pause();
            yield return new WaitUntil(() => Input.GetKeyUp(configuracion.teclaSiguienteFrase));
        }

        AnimateCall.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo();
        yield return new WaitForSeconds(1);
        interfaceLlamadas.SetActive(false);
    }
}

[System.Serializable]
public class Llamada2
{
    public string caller;
    public string answer;
    public Dialogo2[] dialogos;
}

[System.Serializable]
public class Dialogo2
{
    public string talker;
    [TextArea(minLines: 3, maxLines: 10)]
    public string text;
}
