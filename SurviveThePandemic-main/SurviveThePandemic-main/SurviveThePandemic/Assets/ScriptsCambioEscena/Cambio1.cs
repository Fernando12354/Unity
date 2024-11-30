using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cambio1 : MonoBehaviour
{
    public Animator transitionAnimator;  // Asigna esto manualmente en el Inspector
    public int numeroEscena;

    // Referencias externas
    private Llamadas llamadasScript;

    void Start()
    {
        if (transitionAnimator == null)
        {
            Debug.LogError("Animator no asignado en el Inspector.");
        }

        // Buscar automáticamente el script de llamadas en la escena
        llamadasScript = FindObjectOfType<Llamadas>();
        if (llamadasScript == null)
        {
            Debug.LogError("No se encontró el script 'Llamadas' en la escena.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Verificar que el tutorial y los diálogos hayan terminado
            if (Objetivos.singleton != null && Objetivos.singleton.finishTutorial &&
                llamadasScript != null && llamadasScript.callAnswer)
            {
                StartCoroutine(SceneLoad(numeroEscena));
            }
            else
            {
                if (Objetivos.singleton != null && !Objetivos.singleton.finishTutorial)
                {
                    Debug.Log("No puedes pasar hasta completar los objetivos del tutorial.");
                }
                if (llamadasScript != null && !llamadasScript.callAnswer)
                {
                    Debug.Log("No puedes pasar hasta contestar y terminar la llamada.");
                }
            }
        }
    }

    public IEnumerator SceneLoad(int sceneIndex)
    {
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("StartTransition");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
