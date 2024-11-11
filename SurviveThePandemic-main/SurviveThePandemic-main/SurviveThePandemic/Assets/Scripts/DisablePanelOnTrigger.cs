using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class DisablePanelOnTrigger : MonoBehaviour
{
    // Referencia al panel que deseas desactivar
    public GameObject panelToDisable;

    // Este método se llama automáticamente cuando un objeto entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que colisiona es el personaje (puedes ajustar la condición según sea necesario)
        if (other.CompareTag("Player"))
        {
            // Desactiva el panel especificado
            if (panelToDisable != null)
            {
                panelToDisable.SetActive(false);
            }
        }
    }
}

