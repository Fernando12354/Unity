using UnityEngine;
using TMPro;

public class CountCoins : MonoBehaviour
{
    public int Coins =0; // Contador de monedas
    public TextMeshProUGUI nCoins; // Componente TextMeshProUGUI para mostrar las monedas

    // Update is called once per frame
    void Update()
    {
        nCoins.text = Coins.ToString(); // Solo muestra el número de monedas
    }
}

