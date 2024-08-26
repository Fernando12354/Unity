/*using UnityEngine;

public class DialogTriggerZone : MonoBehaviour
{
    public DialogManager dialogManager;
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
            }
            else
            {
                Debug.LogError("DialogManager no asignado en DialogTriggerZone.");
            }
        }
    }
}*/

using UnityEngine;

public class DialogTriggerZone : MonoBehaviour
{
    public DialogManager dialogManager;
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
}
