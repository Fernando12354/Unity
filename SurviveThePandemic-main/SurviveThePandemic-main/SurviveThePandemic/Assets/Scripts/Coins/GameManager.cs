using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton para acceso global
    public int Coins = 0; // Contador universal de monedas

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantener este objeto entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

