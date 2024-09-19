using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Método que se llama al finalizar un nivel para guardar los datos
    public void SaveLevelData(string sceneName, string fileName)
    {
        try
        {
            // Crea un nuevo objeto LevelData y asigna los valores
            LevelData levelData = new LevelData();
            levelData.sceneName = sceneName;

            // Convierte los datos a formato JSON
            string json = JsonUtility.ToJson(levelData);

            // Define la ruta donde se guardará el archivo JSON
            string folderPath = Path.Combine(Application.persistentDataPath, "GameData");
            string filePath = Path.Combine(folderPath, fileName + ".json"); // Usamos el nombre dinámico del archivo

            // Si la carpeta no existe, la crea
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Debug.Log("Directorio creado en: " + folderPath);
            }

            // Escribe o sobrescribe el archivo JSON
            File.WriteAllText(filePath, json);
            Debug.Log("Datos del nivel guardados en: " + filePath);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error al guardar los datos del nivel: " + ex.Message);
        }
    }
}

