using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class PanelActivator : MonoBehaviour
{
    public GameObject panel; // Referencia al panel que quieres controlar
    public float displayDuration = 5f; // Tiempo en segundos que el panel estará activo

    private void Start()
    {
        if (panel != null)
        {
            // Asegurarse de que el panel esté activo al iniciar
            panel.SetActive(true);

            // Programar la desactivación del panel después de displayDuration
            Invoke(nameof(DeactivatePanel), displayDuration);
        }
        else
        {
            Debug.LogWarning("No se asignó ningún panel al script.");
        }
    }

    private void DeactivatePanel()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }
}

