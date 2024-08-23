using UnityEngine;

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
}
