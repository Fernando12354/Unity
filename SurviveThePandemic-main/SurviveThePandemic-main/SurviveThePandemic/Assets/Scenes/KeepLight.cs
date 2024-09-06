using UnityEngine;

public class KeepLight : MonoBehaviour
{
    private void Awake()
    {
        // Revisa si ya existe una luz persistente
        if (FindObjectsOfType<KeepLight>().Length > 1)
        {
            // Si ya hay una luz persistente, destruye esta instancia para evitar duplicados
            Destroy(gameObject);
        }
        else
        {
            // Evita que este objeto sea destruido al cambiar de escena
            DontDestroyOnLoad(gameObject);
        }
    }
}

