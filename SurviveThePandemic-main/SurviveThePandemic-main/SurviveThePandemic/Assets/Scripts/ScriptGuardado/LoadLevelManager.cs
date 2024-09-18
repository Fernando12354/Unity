using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelManager : MonoBehaviour
{
    public void LoadSavedLevel()
    {
        // Define la ruta del archivo JSON
        string folderPath = Path.Combine(Application.persistentDataPath, "GameData");
        string filePath = Path.Combine(folderPath, "levelData.json");

        // Verifica si el archivo existe antes de intentar leerlo
        if (File.Exists(filePath))
        {
            // Lee el contenido del archivo JSON
            string json = File.ReadAllText(filePath);

            // Convierte el JSON en un objeto LevelData
            LevelData levelData = JsonUtility.FromJson<LevelData>(json);

            // Carga la escena que estaba guardada
            SceneManager.LoadScene(levelData.sceneName);
        }
        else
        {
            Debug.LogError("No se encontró el archivo de datos del nivel.");
        }
    }
}
