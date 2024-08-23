using UnityEngine;

public class DialogTriggerZone : MonoBehaviour
{
    public DialogManager dialogManager;
    public int dialogIndex; // Índice del diálogo que se mostrará

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {

         Debug.Log("Afuera.");
        if (other.CompareTag("Player") && !hasTriggered)
        {

             Debug.Log("Jugador ha entrado en la zona de activación.");
            hasTriggered = true;
            dialogManager.StartDialog(dialogIndex);
        }
    }
}
