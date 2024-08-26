using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    [Tooltip("Nombres de los objetivos a completar, configurados desde Unity.")]
    public string[] objectiveNames; // Nombres de los objetivos a completar

    private Dictionary<string, bool> objectives = new Dictionary<string, bool>();

    private void Start()
    {
        // Inicializar el diccionario de objetivos basándose en el array de nombres
        foreach (string objectiveName in objectiveNames)
        {
            if (!objectives.ContainsKey(objectiveName))
            {
                objectives.Add(objectiveName, false); // Todos los objetivos empiezan sin completar
            }
            else
            {
                Debug.LogWarning($"El objetivo '{objectiveName}' ya existe en el ObjectiveController. Evita duplicados.");
            }
        }
    }

    // Método para marcar un objetivo como completado
    public void CompleteObjective(string objectiveName)
    {
        if (objectives.ContainsKey(objectiveName))
        {
            objectives[objectiveName] = true;
            Debug.Log($"¡Objetivo '{objectiveName}' completado!");
        }
        else
        {
            Debug.LogError($"El objetivo '{objectiveName}' no existe en el ObjectiveController.");
        }
    }

    // Método para verificar si todos los objetivos están completados
    public bool AreAllObjectivesCompleted()
    {
        foreach (bool isCompleted in objectives.Values)
        {
            if (!isCompleted)
            {
                return false;
            }
        }
        return true;
    }

    // Método para verificar si un objetivo específico está completado
    public bool IsObjectiveCompleted(string objectiveName)
    {
        if (objectives.ContainsKey(objectiveName))
        {
            return objectives[objectiveName];
        }
        else
        {
            Debug.LogError($"El objetivo '{objectiveName}' no existe en el ObjectiveController.");
            return false;
        }
    }
}
