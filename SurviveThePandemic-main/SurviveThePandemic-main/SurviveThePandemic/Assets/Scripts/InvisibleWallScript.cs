using UnityEngine;
using UnityEngine.SceneManagement;

public class InvisibleWallScript : MonoBehaviour
{
    public ObjectiveController objectiveController; // Asignar en el Inspector
    public string objectiveName; // El nombre del objetivo necesario
    public string sceneToLoad; // Nombre de la escena que se cargará

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (objectiveController != null && objectiveController.IsObjectiveCompleted(objectiveName))
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.Log("El objetivo no está completado. No se puede cambiar de escena.");
            }
        }
    }
}

