using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadLevelManager : MonoBehaviour
{
    // Este campo será configurable en el Inspector de Unity
    public string fileName;

    // Método para cargar el nivel guardado con el nombre del archivo JSON dinámico
    public void LoadSavedLevel()
    {
        // Define la ruta de la carpeta GameData en el almacenamiento persistente
        string folderPath = Path.Combine(Application.persistentDataPath, "GameData");

        // Verifica si la carpeta GameData existe
        if (!Directory.Exists(folderPath))
        {
            Debug.LogError("No se encontró la carpeta GameData en: " + folderPath);
            return; // Sale del método si no se encuentra la carpeta
        }

        // Define la ruta completa del archivo JSON usando el nombre del archivo proporcionado
        string filePath = Path.Combine(folderPath, fileName + ".json");

        // Verifica si el archivo existe antes de intentar leerlo
        if (File.Exists(filePath))
        {
            // Lee el contenido del archivo JSON
            string json = File.ReadAllText(filePath);

            // Convierte el JSON en un objeto LevelData
            LevelData levelData = JsonUtility.FromJson<LevelData>(json);

            // Carga la escena que estaba guardada usando el nombre de la escena en el JSON
            SceneManager.LoadScene(levelData.sceneName);
        }
        else
        {
            Debug.LogError("No se encontró el archivo de datos del nivel: " + filePath);
        }
    }
}

