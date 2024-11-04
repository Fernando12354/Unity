using UnityEngine;

public class ToggleGameObjects: MonoBehaviour
{
    // Arrastra y suelta tus GameObjects en el Inspector
    public GameObject objectToDeactivate;
    public GameObject objectToActivate;

    // Este método se llama automáticamente cuando otro Collider entra en el Trigger
    private void OnTriggerEnter(Collider other)
    {
        // Asegúrate de que el objeto que colisiona tenga la etiqueta "Player"
        if (other.CompareTag("Player")) 
        {
            // Asegúrate de que los objetos están asignados en el Inspector
            if (objectToDeactivate != null && objectToActivate != null)
            {
                // Desactiva el primer objeto y activa el segundo
                objectToDeactivate.SetActive(false);
                objectToActivate.SetActive(true);
                Debug.Log("Objectos cambiados correctamente");
            }
            else
            {
                Debug.LogWarning("Uno o ambos GameObjects no están asignados.");
            }
        }
    }
}


