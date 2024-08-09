using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Cambio : MonoBehaviour
{
    public Animator transitionAnimator;  // Asigna esto manualmente en el Inspector
    public int numeroEscena;

    void Start() {
        if (transitionAnimator == null) {
            Debug.LogError("Animator no asignado en el Inspector.");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            StartCoroutine(SceneLoad(numeroEscena));
        }
    }

    public IEnumerator SceneLoad(int sceneIndex)
    {
        if (transitionAnimator != null) {
            transitionAnimator.SetTrigger("StartTransition");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(sceneIndex);
        }
    }
}

