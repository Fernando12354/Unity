using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Método que se llama al finalizar un nivel para guardar los datos
    public void SaveLevelData(string sceneName)
    {
        // Crea un nuevo objeto LevelData y asigna los valores
        LevelData levelData = new LevelData();
        levelData.sceneName = sceneName;
        //levelData.score = score;

        // Convierte los datos a formato JSON
        string json = JsonUtility.ToJson(levelData);

        // Define la ruta donde se guardará el archivo JSON
        string folderPath = Path.Combine(Application.persistentDataPath, "GameData");
        string filePath = Path.Combine(folderPath, sceneName + ".json"); // Usamos el nombre de la escena para el archivo

        // Si la carpeta no existe, la crea
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Verifica si el archivo ya existe
        if (File.Exists(filePath))
        {
            Debug.Log("El archivo " + filePath + " ya existe. No se creará de nuevo.");
        }
        else
        {
            // Escribe el archivo JSON en la carpeta si no existe
            File.WriteAllText(filePath, json);
            Debug.Log("Datos del nivel guardados en: " + filePath);
        }
    }
}
