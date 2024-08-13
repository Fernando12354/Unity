using UnityEngine;
using UnityEngine.UI;

public class ContinueButtonHandler : MonoBehaviour
{
    public ScriptLibroDatos scriptLibroDatos; // Referencia al script ScriptLibroDatos

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnContinueButtonClicked);
    }

    void OnContinueButtonClicked()
    {
        if (scriptLibroDatos != null)
        {
            scriptLibroDatos.ContinueGame(); // Llamar al método ContinueGame del script ScriptLibroDatos
        }
    }
}

