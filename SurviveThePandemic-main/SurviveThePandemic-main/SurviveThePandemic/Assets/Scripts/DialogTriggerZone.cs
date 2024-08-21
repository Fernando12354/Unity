using UnityEngine;

public class DialogTriggerZone : MonoBehaviour
{
    public Dialog dialog; // Asigna el diálogo en el inspector
    private DialogManager dialogManager;

    void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tenga la etiqueta "Player"
        {
            if (dialogManager != null)
            {
                dialogManager.StartDialog(dialog);
            }
        }
    }
}
