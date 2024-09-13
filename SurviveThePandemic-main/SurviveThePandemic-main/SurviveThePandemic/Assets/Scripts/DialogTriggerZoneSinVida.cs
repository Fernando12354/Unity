/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTriggerZoneSinVida : MonoBehaviour
{
    public DialogManagerSinVida dialogManager;
    public ObjectiveController objectiveController; // Referencia al ObjectiveController
    public string objectiveName; // Nombre del objetivo que se debe completar
    public int firstDialogIndex = 0; // Índice del primer diálogo que se mostrará

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            if (dialogManager != null)
            {
                dialogManager.StartDialog(firstDialogIndex);
                hasTriggered = true;

                // Marcar el objetivo como completado
                if (objectiveController != null && !string.IsNullOrEmpty(objectiveName))
                {
                    objectiveController.CompleteObjective(objectiveName);
                }
            }
            else
            {
                Debug.LogError("DialogManager no asignado en DialogTriggerZone.");
            }
        }
    }
}*/

using System.Collections;
using UnityEngine;

public class DialogTriggerZoneSinVida : MonoBehaviour
{
    public DialogManagerSinVida dialogManager;
    public ObjectiveController objectiveController; // Referencia al ObjectiveController
    public string objectiveName; // Nombre del objetivo que se debe completar
    public int firstDialogIndex = 0; // Índice del primer diálogo que se mostrará
    public AudioSource musicAudioSource; // Referencia al AudioSource de la música

    private bool hasTriggered = false;
    public GameObject ZonaDeDialogosPost;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            if (dialogManager != null)
            {
                // Pausar la música
                PauseMusic();

                dialogManager.StartDialog(firstDialogIndex);
                hasTriggered = true;

                // Marcar el objetivo como completado
                if (objectiveController != null && !string.IsNullOrEmpty(objectiveName))
                {
                    objectiveController.CompleteObjective(objectiveName);
                }

                // Esperar a que el diálogo termine y reanudar la música
                StartCoroutine(WaitForDialogEnd());
            }
            else
            {

                if (objectiveController != null && !string.IsNullOrEmpty(objectiveName))
                {
                    objectiveController.CompleteObjective(objectiveName);
                }
                
                Debug.LogError("DialogManager no asignado en DialogTriggerZone.");
            }
        }
    }

    private IEnumerator WaitForDialogEnd()
    {
        // Esperar hasta que el diálogo termine
        while (dialogManager.IsDialogActive())
        {
            yield return null;
        }

        // Reanudar la música
        ResumeMusic();

          if (ZonaDeDialogosPost != null)
        {
            ZonaDeDialogosPost.SetActive(true); // Activa el GameObject con MessageManager
        }
    }

    private void PauseMusic()
    {
        if (musicAudioSource != null)
        {
            musicAudioSource.Pause();
        }
    }

    private void ResumeMusic()
    {
        if (musicAudioSource != null)
        {
            musicAudioSource.UnPause();
        }
    }
}
