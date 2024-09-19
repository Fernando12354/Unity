using UnityEngine;

public class MissionCompletedCanvas : MonoBehaviour
{
    public LevelManager levelManager;  // Referencia al LevelManager
    public string sceneToSave ;  // Nombre de la escena a guardar (cambiar por el nombre deseado)
    public string fileName;     // Nombre del archivo a guardar (cambiar por el nombre deseado)

    // Método que se llama cuando el Canvas de Misión Completada está activo
    public void OnEnable()
    {   
        Debug.Log("ENTROO");

        // Verifica que el LevelManager esté asignado
        if (levelManager != null)
        {
            // Verifica que los nombres de la escena y el archivo no estén vacíos
            if (!string.IsNullOrEmpty(sceneToSave) && !string.IsNullOrEmpty(fileName))
            {
                // Llama al método SaveLevelData con el nombre de la escena y el nombre del archivo
                levelManager.SaveLevelData(sceneToSave, fileName);
                Debug.Log("Misión completada y datos guardados.");
            }
            else
            {
                Debug.LogError("El nombre de la escena o del archivo está vacío.");
            }
        }
        else
        {
            Debug.LogError("LevelManager no está asignado en el Canvas Misión Completada.");
        }
    }
}
